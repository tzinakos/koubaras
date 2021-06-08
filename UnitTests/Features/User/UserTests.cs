using Models;

namespace UnitTests.Features.User
{
    using System.Linq;

    using NUnit.Framework;

    public class UserTests : UsersTestRig
    {
        [Category("CreateUser")]
        [Test]
        public void Given_A_New_User_When_The_User_Is_Created_Then_The_Count_Of_The_Users_Table_Is_One()
        {
            UserHandler.Create(CreateUser())
                .ConfigureAwait(false);

            Assert.That(Db.Users.Count(),Is.EqualTo(1));
        }

        [Category("UpdateUser")]
        [Test]
        public void Given_A_User_With_FirstName_Of_Peter_When_The_User_Is_Updated_Its_New_FirstName_Is_Tomas()
        {
            var user = UserHandler.Create(CreateUser(firstName: "Peter"))
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            user.FirstName = "Tomas";

            UserHandler.Update(user).ConfigureAwait(false);

            var firstName = Db.Users.FirstOrDefault(u => u.LastName == "Parker")?.FirstName;

            Assert.That(firstName, Is.EqualTo("Tomas"));
        }

        [Category("DeleteUser")]
        [Test]
        public void Given_A_Delete_Request_for_A_User_When_the_request_Is_Handled_Then_The_User_is_Deleted()
        {
            var userId = UserHandler.Create(CreateUser()).GetAwaiter().GetResult().Id;

            UserHandler.Delete(userId).GetAwaiter().GetResult();

            Assert.That(Db.Users.Count(user=> user.Id == userId), Is.EqualTo(0));
        }

        [Category("GetSingleUser")]
        [Test]
        public void Given_A_Get_Request_for_A_User_When_the_request_Is_Handled_Then_The_User_is_Returned()
        {
            var userId = UserHandler.Create(CreateUser()).ConfigureAwait(false).GetAwaiter().GetResult().Id;

            var returnedUser = UserHandler.Get(userId).ConfigureAwait(false).GetAwaiter().GetResult();

            Assert.That(returnedUser, Is.Not.Null);
        }

        [Category("GetAllUsers")]
        [Test]
        public void Given_A_Get_Request_for_All_Users_When_the_request_Is_Handled_Then_All_The_Users_are_Returned()
        {
            UserHandler.Create(CreateUser()).ConfigureAwait(false);
            UserHandler.Create(CreateUser()).ConfigureAwait(false);
            UserHandler.Create(CreateUser()).ConfigureAwait(false);

            var returnedUsers = UserHandler.GetAll().ConfigureAwait(false).GetAwaiter().GetResult();

            Assert.That(returnedUsers.Count, Is.EqualTo(3));
        }
    }
}