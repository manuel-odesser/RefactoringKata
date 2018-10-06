using System.Collections.Generic;
using WalletKata.Exceptions;
using WalletKata.Users;

namespace WalletKata.Wallets
{
    public class WalletService
    {
        private readonly UserSessionRepository userSessionRepository;
        private readonly WalletDAO walletDao;

        public WalletService(UserSessionRepository userSessionRepository, WalletDAO walletDao)
        {
            this.userSessionRepository = userSessionRepository;
            this.walletDao = walletDao;
        }

        public List<Wallet> GetWalletsByUser(User user)
        {
            User loggedInUser = GetLoggedInUserOrThrow();

            List<Wallet> walletList = new List<Wallet>();

            if (AreFriends(user, loggedInUser))
            {
                walletList.AddRange(this.walletDao.FindWalletsByUser(user));
            }

            return walletList;
        }

        private User GetLoggedInUserOrThrow()
        {
            UserSession userSession = this.userSessionRepository.Current;
            User loggedInUser = userSession.GetLoggedUser();

            if (loggedInUser == null)
            {
                throw new UserNotLoggedInException();
            }

            return loggedInUser;
        }

        private static bool AreFriends(User user, User loggedInUser)
        {
            foreach (User friend in user.GetFriends())
            {
                if (friend.Equals(loggedInUser))
                {
                    return true;
                }
            }

            return false;
        }
    }
}