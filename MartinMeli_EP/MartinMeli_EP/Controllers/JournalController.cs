using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MartinMeli_EP.Models;
using Logic;
using System.Collections.Specialized;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using System.IO;

namespace MartinMeli_EP.Controllers
{
    public class JournalController : Controller
    {
        ArticleLogic al = new ArticleLogic();
        AccountLogic acl = new AccountLogic();
        //
        // GET: /Journal/

        public ActionResult Index()
        {
            if (Logic.AccountLogic.loggedIn)
                return View();
            return Unauthorized();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ArticleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = Logic.AccountLogic.currentUserId;

                    if (id != -1)
                    {
                        string accountName = "melistorageaccount";
                        string accessKey = "PhhJ2ViOfYM30z01O16uR2u2/yJ1jvZSEM3FiZyvfneVmnx43EfLOjb0sKgBIy57UsEPG5I0MhAzEWAsYHuWGg==";

                        StorageCredentials creden = new StorageCredentials(accountName, accessKey);

                        CloudStorageAccount storageAccount = new CloudStorageAccount(creden, useHttps: true);

                        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                        CloudBlobContainer container = blobClient.GetContainerReference("melinews");

                        container.CreateIfNotExists();

                        container.SetPermissions(new BlobContainerPermissions
                        {
                            PublicAccess = BlobContainerPublicAccessType.Blob
                        });

                        //string path = Server.MapPath("~") + "Images\\ArticleImages\\" + model.Image.FileName;
                        //model.Image.SaveAs(path);

                        CloudBlockBlob cblob = container.GetBlockBlobReference(model.Image.FileName);

                        using (/*Stream file = System.IO.File.OpenRead(@"" + path)*/Stream file = model.Image.InputStream)
                        {
                            cblob.UploadFromStream(file);
                        }

                        Data.article newArticle = new Data.article
                        { headline = model.Headline, subheading = model.Subheading, description = model.Description, imageURL = "https://melistorageaccount.blob.core.windows.net/melinews/" + model.Image.FileName, authorId = id, categoryId = model.CategoryID };

                        if (al.AddArticle(newArticle))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

                // If we got this far, something failed, redisplay form
                return View(model);
            }
            catch {
                return View(model);
            }
        }

        public ActionResult View(int id)
        {
            var a = al.GetArticleByID(id);
            ArticleModel am = new ArticleModel() { Headline = a.headline, Subheading = a.subheading, CategoryID = a.categoryId, Description = a.description, AuthorID = a.authorId, ImageURL = a.imageURL, Category = a.category.category1};
            ViewData["article"] = am;

            var j = acl.GetUser(am.AuthorID);
            JournalistModel jm = new JournalistModel() { Name = j.name, Surname = j.surname};
            ViewData["journalist"] = jm;

            return View();
        }

        public ActionResult Edit(int id)
        {
            try
            {
                int userId = Logic.AccountLogic.currentUserId;

                if (userId != -1)
                {
                    NameValueCollection nvc = Request.Form;
                    string headline, description, subheading;
                    int categoryId;
                    if (!string.IsNullOrEmpty(nvc["Headline"]))
                    {
                        headline = nvc["Headline"];

                        if (!string.IsNullOrEmpty(nvc["Subheading"]))
                        {
                            subheading = nvc["Subheading"];
                            if (!string.IsNullOrEmpty(nvc["Description"]))
                            {
                                description = nvc["Description"];

                                if (!string.IsNullOrEmpty(nvc["CategoryID"]))
                                {
                                    categoryId = Convert.ToInt32(nvc["CategoryID"]);

                                    al.UpdateJournal(id, new Data.article() { headline = headline, subheading = subheading, description = description, categoryId = categoryId });
                                    return RedirectToAction("Index", "Home");
                                }
                            }
                        }
                    }

                    return View();
                }
                return RedirectToAction("Journal", "Unauthorized");
            }
            catch { }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                int userId = Logic.AccountLogic.currentUserId;

                if (userId != -1)
                {
                    var a = al.GetArticleByID(id);
                    ArticleModel am = new ArticleModel() { Headline = a.headline, CategoryID = a.categoryId, Description = a.description, AuthorID = a.authorId };

                    if (am.AuthorID == userId)
                    {
                        al.DeleteJournal(id);

                        return View();
                    }
                }
                return RedirectToAction("Unauthorized", "Journal");
            }
            catch { }
            return RedirectToAction("Home", "Index");
        }

        public ActionResult MyJournals()
        {
            try
            {
                int id = Logic.AccountLogic.currentUserId;

                if (id != -1)
                {
                    ViewBag.articles = al.GetArticlesByJournalist(id);

                    var j = acl.GetUser(id);
                    JournalistModel jm = new JournalistModel() { Name = j.name, Surname = j.surname };
                    ViewData["journalist"] = jm;

                    return View();
                }
            }
            catch
            { }

            return RedirectToAction("Index", "Home");
        }


        public ActionResult Journalist(int id)
        {
            try
            {
                    ViewBag.articles = al.GetArticlesByJournalist(id);

                    var j = acl.GetUser(id);
                    JournalistModel jm = new JournalistModel() { Name = j.name, Surname = j.surname };
                    ViewData["journalist"] = jm;

                    return View();
            }
            catch
            { }

            return RedirectToAction("Index", "Home");
        }


        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}
