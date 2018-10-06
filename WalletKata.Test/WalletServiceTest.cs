﻿using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using WalletKata.Exceptions;
using WalletKata.Users;
using WalletKata.Wallets;

namespace WalletKata.Test
{
    public class WalletServiceTest
    {
        private WalletService sut;
        private Mock<UserSessionRepository> userSessionRepositoryMock;
        private Mock<UserSession> userSessionMock;
        private Mock<WalletDAO> walletDaoMock;

        [SetUp]
        public void Before()
        {
            this.userSessionRepositoryMock = new Mock<UserSessionRepository>();
            this.userSessionMock = new Mock<UserSession>();
            this.walletDaoMock = new Mock<WalletDAO>();
            this.sut = new WalletService(this.userSessionRepositoryMock.Object, this.walletDaoMock.Object);
        }

        [Test]
        public void GetWalletsByUser_userIsNotLogged_throws()
        {
            // Arrange
            userSessionMock.Setup(m => m.GetLoggedUser()).Returns((User)null);

            var userSession = this.userSessionMock.Object;
            userSessionRepositoryMock.Setup(m => m.Current).Returns(userSession);


            // Act
            TestDelegate action = () => { this.sut.GetWalletsByUser(new User()); };

            // Assert
            Assert.That(action, Throws.TypeOf<UserNotLoggedInException>());
        }

        [Test]
        public void GetWalletsByUser_userIsLoggedAndHasNoFriend_returnsEmptyList()
        {
            // Arrange
            User loggedInUser = new User();
            userSessionMock.Setup(m => m.GetLoggedUser()).Returns(loggedInUser);

            var userSession = this.userSessionMock.Object;
            userSessionRepositoryMock.Setup(m => m.Current).Returns(userSession);


            // Act
            var actual = this.sut.GetWalletsByUser(new User());

            // Assert
            Assert.That(actual, Is.Empty);
        }

        [Test]
        public void GetWalletsByUser_userIsLoggedAndHasAFriend_returnsListFromDao()
        {
            // Arrange
            User loggedInUser = new User();
            userSessionMock.Setup(m => m.GetLoggedUser()).Returns(loggedInUser);

            var userSession = this.userSessionMock.Object;
            userSessionRepositoryMock.Setup(m => m.Current).Returns(userSession);

            var user = new User();
            user.AddFriend(loggedInUser);

            var expectedWallets = new List<Wallet>();
            this.walletDaoMock.Setup(m => m.FindWalletsByUser(user)).Returns(expectedWallets);

            // Act
            var actual = this.sut.GetWalletsByUser(user);

            // Assert
            Assert.That(actual, Is.SameAs(expectedWallets));
        }
    }
}
