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
        public void Add_User_UnitTest()
        {

            User user = new User();
            user.Id = Guid.NewGuid();
            user.UserName = "quacam";
            user.FirstName = "nguyen qua";
            user.LastName = "cam";
            user.Address = "số 18,xuân thủy";
            user.Age = 21;
            user.Email = "adquang123@gmail.com";
            user.Password = "123";
            db.Users.Add(user);
            db.SaveChanges();
            //test
            Assert.AreEqual(1, user.Id);
        }
    }
}
