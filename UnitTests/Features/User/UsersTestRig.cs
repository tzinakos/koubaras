using Business.IRepository;

namespace UnitTests.Features.User
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using Business.Repository;
    using Models;
    
    public class UsersTestRig : TestRig
    {
        protected IHandle<UserDTO> UserHandler;

        [SetUp]
        public void Setup()
        {
            UserHandler = new UserHandler(Db, Mapper);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                Db.Users.RemoveRange(Db.Users.Select(x => x));
                Db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        public UserDTO CreateUser(string firstName = "Peter", string lastName = "Parker", string age = "25")
        {
            return new () {FirstName = firstName, LastName = lastName, Age = age};
        }
    }
}
