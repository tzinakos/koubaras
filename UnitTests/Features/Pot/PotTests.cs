namespace UnitTests.Features.Pot
{
    using System.Linq;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using Business.Repository;
    using Models;

    public class PotTests : PotTestRig
    {
        [Category("CreatePot")]
        [Test]
        public async Task Given_A_Request_For_Creating_A_New_Pot_When_The_Request_Is_Being_Handled_Then_The_Pot_Is_Created()
        {
            var user = await GetUser();
            if (user != null)
            {
                await PotHandler.Create(CreatePot(user.Id));
            }

            var pot = Db.Pots.Count();

            Assert.That(pot, Is.EqualTo(1));
        }

        private async Task<UserDTO>GetUser(UserDTO userDto = null)
        {
            var user = userDto ?? new UserDTO {FirstName = "Jino", LastName = "Biba", Age = "25"};
            return await new UserHandler(Db, Mapper).Create(user);
        }
    }
}
