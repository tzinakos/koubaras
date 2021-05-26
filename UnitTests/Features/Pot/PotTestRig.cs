namespace UnitTests.Features.Pot
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using Business.Repository;
    using Business.IRepository;
    using Models;

    public class PotTestRig : TestRig
    {
        protected IHandle<PotDTO> PotHandler;

        [SetUp]
        public void Setup()
        {
            PotHandler  = new PotHandler(Db, Mapper);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                Db.Pots.RemoveRange(Db.Pots.Select(x => x));
                Db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        public PotDTO CreatePot(Guid userId, string name = "TestPot")
        { 
            return new() { Name = name, UserId = userId};
        }
    }
}
