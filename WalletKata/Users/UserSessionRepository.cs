using WalletKata.Exceptions;

namespace WalletKata.Users
{
    public class UserSessionRepository
    {
        public virtual UserSession Current
        {
            get
            {
                throw new ThisIsAStubException("UserSessionRepository.Current should not be called in an unit test");
            }
        }
    }
}
