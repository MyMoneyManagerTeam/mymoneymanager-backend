using System;
using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.Jars;
using Application.Services.Jars.Dto;
using Domain.Jars;
using Domain.Users;
using NSubstitute;
using NUnit.Framework;

namespace UnitTest
{
    public class JarServicesTest
    {
        [SetUp]
        public void Init()
        {
            
        }

        [Test]
        public void Query_UserId_ReturnAllJarsOneUser()
        {
            //Arrange
            IJarRepository jarRepository = Substitute.For<IJarRepository>();
            JarService jarService = new JarService(jarRepository);
            Guid myGuid = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}");

            IEnumerable<IJar> jarList = new List<Jar>
            {
                new Jar(),
                new Jar()
            };
            jarRepository.Query(myGuid).Returns(jarList);
            
            //Act
            var outputTest = jarService.Query(myGuid);
            
            //Assert
            Assert.AreEqual(2,jarList.Count());
        }

        [Test]
        public void Get_UserIdAndJarId_ReturnOneJar()
        {
            //Arrange
            IJarRepository jarRepository = Substitute.For<IJarRepository>();
            JarService jarService = new JarService(jarRepository);
            Guid myGuid = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}");
            IJar newJar = new Jar
            {
                Balance = 100,
                Description = "nodescript",
                Id = myGuid,
                Max = 150,
                Name = "test",
                Owner = new User
                {
                    Id = myGuid
                }
            };
            OutputDtoQueryJar outputDtoQueryJar = new OutputDtoQueryJar
            {
                Balance = 100,
                Description = "nodescript",
                Id = myGuid,
                Max = 150,
                Name = "test",
                Owner = myGuid
            };
            jarRepository.Get(myGuid, myGuid).Returns(newJar);
            
            //Act
            OutputDtoQueryJar outputTest = jarService.Get(myGuid, myGuid);

            //Assert
            Assert.AreEqual(outputDtoQueryJar,outputTest);
        }

        [Test]
        public void Create_UserIdAndInputDtoCreateJar_ReturnOutputDtoCreateJar()
        {
            //Arrange
            IJarRepository jarRepository = Substitute.For<IJarRepository>();
            JarService jarService = new JarService(jarRepository);
            Guid myGuid = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}");
            InputDtoCreateJar inputDtoCreateJar = new InputDtoCreateJar
            {
                Balance = 100,
                Description = "test",
                Max = 150,
                Name = "test"
            };
            OutputDtoCreateJar outputDtoCreateJar = new OutputDtoCreateJar
            {
                Id = myGuid
            };
            IJar jarIn = new Jar();
            IJar jarOut = new Jar
            {
                Id = myGuid,
                Owner = new User
                {
                    Id = myGuid
                }
            };
            jarRepository.Create(jarIn).Returns(jarOut);
            
            //Act
            var OutputTest = jarService.Create(myGuid, inputDtoCreateJar);

            //Assert
            
            Assert.AreEqual(outputDtoCreateJar,OutputTest);
        }

        [Test]
        public void Update_UserIdAndInputDtoUpdateJar()
        {
            //Arrange
            IJarRepository jarRepository = Substitute.For<IJarRepository>();
            JarService jarService = new JarService(jarRepository);
            Guid userGuid = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8340}");
            Guid myGuid = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}");
            InputDtoUpdateJar inputDtoUpdateJar = new InputDtoUpdateJar
            {
                Balance = 100,
                Description = "test",
                Id = myGuid,
                Max = 150,
                Name = "test",
                Owner = userGuid
            };
            IJar jar = new Jar
            {
                Balance = inputDtoUpdateJar.Balance,
                Description = inputDtoUpdateJar.Description,
                Id = inputDtoUpdateJar.Id,
                Max = inputDtoUpdateJar.Max,
                Name = inputDtoUpdateJar.Name,
                Owner = new User
                {
                    Id = inputDtoUpdateJar.Owner
                }
            };
            jarRepository.Update(inputDtoUpdateJar.Id, jar).Returns(true);

            //Act
            var myBool = jarService.Update(myGuid, inputDtoUpdateJar);

            //Assert
            Assert.AreEqual(true, myBool);
        }

        [Test]
        public void Delete_UserIdAndJarId_ReturnTrue()
        {
            //Arrange
            IJarRepository jarRepository = Substitute.For<IJarRepository>();
            JarService jarService = new JarService(jarRepository);
            Guid myGuid = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8340}");
            IJar jar = new Jar
            {
                Id = myGuid
            };
            jarRepository.Delete(myGuid, jar.Id).Returns(true);
            
            //Act
            var myBool = jarService.Delete(myGuid, jar.Id);

            //Assert
            Assert.AreEqual(true, myBool);
        }
    }
}