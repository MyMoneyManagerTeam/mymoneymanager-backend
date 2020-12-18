using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.Accounts;
using Application.Services.Accounts.Dto;
using Application.Services.Users;
using Application.Services.Users.Dto;
using Domain.Accounts;
using Domain.Users;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NSubstitute.Exceptions;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace UnitTest
{
    public class UserServicesTest
    {
        [SetUp]
        public void Init()
        {
            
        }

        [Test]
        public void Authenticate_InputDtoAuth_OutputDtoAuth()
        {
            //Arrange
            IUserRepository userRepository = Substitute.For<IUserRepository>();
            UserService userService = new UserService(userRepository);
            Guid myGuid = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}");
            OutputDtoAuth outputDtoAuth = new OutputDtoAuth
            {
                Id = myGuid,
                Mail = "test"
            };

            IUser iuser = new User
            {
                Id = myGuid,
                Mail = "test",
                Password = "test"
            };
            
            InputDtoAuth inputDtoAuth = new InputDtoAuth
            {
                Mail = "test",
                Password = "test"
            };
            
            userRepository.Get(inputDtoAuth.Mail, inputDtoAuth.Password).Returns(iuser);
            
            //Act
            OutputDtoAuth outputTest = userService.Authenticate(inputDtoAuth);

            //Assert
            Assert.AreEqual(outputDtoAuth, outputTest);
        }
        
        [Test]
        public void Authenticate_InputDtoAuth_Null()
        {
            //Arrange
            IUserRepository userRepository = Substitute.For<IUserRepository>();
            UserService userService = new UserService(userRepository);

            InputDtoAuth inputDtoAuth = new InputDtoAuth
            {
                Mail = "test",
                Password = "test"
            };
            
            userRepository.Get(inputDtoAuth.Mail, inputDtoAuth.Password).ReturnsNull();
            
            //Act
            OutputDtoAuth outputTest = userService.Authenticate(inputDtoAuth);

            //Assert
            Assert.AreEqual(null, outputTest);
        }

        [Test]
        public void Signin_InputDtoSignin_OutputDtoSignin()
        {
            //Arrange
            IUserRepository userRepository = Substitute.For<IUserRepository>();
            UserService userService = new UserService(userRepository);
            Guid myGuid = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}");
            IUser iuserIn = new User();
            IUser iuserOut = new User
            {
                Id = myGuid,
                Mail = "test",
                Password = "test",
                Account = new Account(),
                Address = "test",
                Admin = true,
                Area = "test",
                City = "test",
                Confirmed = true,
                Country = "test",
                Picture = null,
                Token = "test",
                Zip = 0,
                FirstName = "test",
                LastName = "test"
            };

            OutputDtoSignin outputDtoSignin = new OutputDtoSignin
            {
                Id = myGuid,
                Mail = "test",
                Address = "test",
                Admin = true,
                Area = "test",
                City = "test",
                Confirmed = true,
                Country = "test",
                Picture = null,
                Token = "test",
                Zip = 0,
                FirstName = "test",
                LastName = "test"
            };

            InputDtoSignin inputDtoSignin = new InputDtoSignin
            {
                Mail = "test",
                Password = "test"
            };
            
            userRepository.Create(iuserIn).Returns(iuserOut);
            //Act

            OutputDtoSignin outputTest = userService.Signin(inputDtoSignin);

            //Assert
            Assert.AreEqual(outputDtoSignin,outputTest);
        }
        
        [Test]
        public void Signin_InputDtoSignin_Null()
        {
            //Arrange
            IUserRepository userRepository = Substitute.For<IUserRepository>();
            UserService userService = new UserService(userRepository);
            IUser iuserIn = new User();
            InputDtoSignin inputDtoSignin = new InputDtoSignin
            {
                Mail = "test",
                Password = "test"
            };
            userRepository.Create(iuserIn).ReturnsNull();
            
            //Act
            OutputDtoSignin outputTest = userService.Signin(inputDtoSignin);

            //Assert
            Assert.AreEqual(null,outputTest);
        }

        [Test]
        public void UploadImage_UserIdAndImage_Bool()
        {
            //Arrange
            IUserRepository userRepository = Substitute.For<IUserRepository>();
            UserService userService = new UserService(userRepository);
            Guid myGuid = new Guid();
            IFormFile myImage = null;
            userRepository.UploadImage(myGuid,myImage).Returns(true);
            
            //Act
            bool uploadTest = userService.UploadImage(myGuid, myImage);

            //Assert
            Assert.AreEqual(true,uploadTest);
        }
        
        [Test]
        public void Query_NoArg_OutputDtoQueryUserList()
        {
            //Arrange
            IUserRepository userRepository = Substitute.For<IUserRepository>();
            UserService userService = new UserService(userRepository);
            IEnumerable<IUser> userList = new List<User>
            {
                new User
                {
                    Id = new Guid(),
                    Mail = "test",
                    Password = "test",
                    Account = new Account(),
                    Address = "test",
                    Admin = true,
                    Area = "test",
                    City = "test",
                    Confirmed = true,
                    Country = "test",
                    Picture = null,
                    Token = "test",
                    Zip = 0,
                    FirstName = "test",
                    LastName = "test"
                }
            };
            userRepository.Query().Returns(userList);

            //Act
            var outputTest = userService.Query();
            
            //Assert
            Assert.AreEqual(userList.Count(),outputTest.Count());
        }
        
        [Test]
        public void Update_User_Bool()
        {
            //Arrange
            IUserRepository userRepository = Substitute.For<IUserRepository>();
            UserService userService = new UserService(userRepository);
            Guid myGuid = new Guid("{fd639119-ce4f-401f-959b-fb8999dc8344}");
            InputDtoUpdatePrivileges inputDtoUpdatePrivileges = new InputDtoUpdatePrivileges
            {
                Id = myGuid
            };
            IUser user = new User
            {
                Id = myGuid
            };
            userRepository.Update(user).Returns(true);
            
            //Act
            var myBool = userService.Update(inputDtoUpdatePrivileges);
            
            //Assert
            Assert.AreEqual(true,myBool);
        }

        
    }
}