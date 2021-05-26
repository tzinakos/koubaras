using Business.IRepository;
using Business.Repository;
using Models;
using Moq;

namespace UnitTests.Features
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using AutoMapper;

    using Business.Mapper;
    using DataAccess;

    public class TestRig
    {
        protected ApplicationDbContext Db;
        protected IMapper Mapper;
        protected Mock<IHandle<UserDTO>> UserRepository { get; set; }


        [SetUp]
        public void BaseSetup()
        {
            ConfigureApplicationDbContext();
            ConfigureAutoMapper();
            UserRepository = new Mock<IHandle<UserDTO>>();
        }

        [TearDown]
        public void BaseTearDown()
        {
            try
            {
                Db.Dispose();
                UserRepository.Reset();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void ConfigureApplicationDbContext()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(ConfigReader.ConnectionString)
                .Options;

            Db = new ApplicationDbContext(contextOptions);
        }

        public void ConfigureAutoMapper()
        {
            Mapper = new Mapper(
                new MapperConfiguration(
                    mp => mp
                        .AddProfile(new MapperProfile())
                )
            );
        }
    }
}
