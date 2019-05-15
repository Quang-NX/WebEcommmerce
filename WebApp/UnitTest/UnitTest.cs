using System;
using Domain;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        private EcommerceDbContext db = new EcommerceDbContext();
        [TestMethod]
        public void User_UnitTest()
        {

            User user = new User();
            user.Id = Guid.NewGuid();
            user.UserName = "Minh";
            user.Password = "123";
            db.Users.Add(user);
            db.SaveChanges();

        }
    }
}
