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

        [SetUp]
        public void Before()
        {
            this.userSessionRepositoryMock = new Mock<UserSessionRepository>();
            this.userSessionMock = new Mock<UserSession>();
            this.sut = new WalletService(userSessionRepositoryMock.Object, new WalletDAO());
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
    }
}
