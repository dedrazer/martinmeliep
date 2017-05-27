using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web;
using System.Web.Security;
using Data;

namespace MartinMeli_EP.Models
{
    public class ArticleModel
    {
        [StringLength(50, ErrorMessage = "Heading cannot be longer than 40 characters.")]
        [Required]
        [Display(Name = "Headline")]
        public string Headline { get; set; }

        [Required]
        [Display(Name = "Subheading")]
        public string Subheading { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        
        [Required]
        [Display(Name = "Image")]
        public HttpPostedFileBase Image { get; set; }
        
        [Display(Name = "ImageURL")]
        public string ImageURL { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        [Display(Name = "CategoryName")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "AuthorID")]
        public int AuthorID { get; set; }

        public journalist Journalist { get; set; }
    }
}