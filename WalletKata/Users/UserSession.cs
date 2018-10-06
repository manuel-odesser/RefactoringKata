using WalletKata.Exceptions;

namespace WalletKata.Users
{
    public class UserSession
    {
        public UserSession() { }

        public virtual User GetLoggedInUser()
        {
            throw new ThisIsAStubException("UserSession.GetLoggedInUser() should not be called in an unit test");
        }
    }
}