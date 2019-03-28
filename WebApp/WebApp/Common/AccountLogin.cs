using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Common
{
    public class AccountLogin
    {
        EcommerceDbContext db = null;
        public AccountLogin()
        {
            db = new EcommerceDbContext();
        }
        public bool Login(string userName, string passWord)
        {
            var user = db.Users.Where(us => us.UserName.Equals(userName) && us.Password.Equals(passWord)).FirstOrDefault();
            if(user!=null)
            {
                
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}