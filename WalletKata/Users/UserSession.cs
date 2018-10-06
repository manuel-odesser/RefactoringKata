using WalletKata.Exceptions;

namespace WalletKata.Users
{
    public class UserSession
    {
        public UserSession() { }

        public virtual User GetLoggedUser()
        {
            throw new ThisIsAStubException("UserSession.IsUserLoggedIn() should not be called in an unit test");
        }
    }
}