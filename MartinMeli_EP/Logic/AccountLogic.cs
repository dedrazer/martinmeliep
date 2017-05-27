using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class AccountLogic: Connection
    {
        public static bool loggedIn { get; set; }
        public static int currentUserId { get; set; } = -1;

        public bool DoesUsernameExist(string username)
        {
            return db.journalists.SingleOrDefault(x => x.username == username) == null ? false : true;
        }

        public bool Register(journalist newJournalist)
        {
            if (DoesUsernameExist(newJournalist.username))
            {
                return false;
            }

            db.journalists.Add(newJournalist);
            db.SaveChanges();
            return true;
        }

        public bool Login(string username, string password)
        {
            journalist account = GetUser(username);

            if (account == null)
                return false;

            if (account.password == password)
            {
                currentUserId = account.id;
                loggedIn = true;
                return true;   
            }
            return false;
        }

        public void Logout()
        {
            currentUserId = -1;
            loggedIn = false;
        }

        public journalist GetUser(string username)
        {
            return db.journalists.SingleOrDefault(j => j.username == username);
        }

        public journalist GetUser(int id)
        {
            return db.journalists.SingleOrDefault(j => j.id == id);
        }

        public List<journalist> GetAllUsers()
        {
            return db.journalists.OrderByDescending(j => j.id).ToList();
        }

        public void DeleteUser(int id)
        {
            journalist j = db.journalists.FirstOrDefault(jr => jr.id == id);

            db.journalists.Remove(j);
            db.SaveChanges();
        }

        public void UpdateJournalist(int id, journalist journalist)
        {
            journalist original = db.journalists.FirstOrDefault(jr => jr.id == id);

            original.about = journalist.about;
            original.email = journalist.email;
            original.name = journalist.name;
            original.password = journalist.password;
            original.surname = journalist.surname;
            original.username = journalist.username;

            db.Entry(original).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
