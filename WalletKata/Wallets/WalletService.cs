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
            List<Wallet> walletList = new List<Wallet>();
            UserSession userSession = this.userSessionRepository.Current;
            User loggedUser = userSession.GetLoggedUser();
            bool isFriend = false;

            if (loggedUser != null)
            {
                foreach (User friend in user.GetFriends())
                {
                    if (friend.Equals(loggedUser))
                    {
                        isFriend = true;
                        break;
                    }
                }

                if (isFriend)
                {
                    walletList.AddRange(this.walletDao.FindWalletsByUser(user));
                }

                return walletList;
            }
            else
            {
                throw new UserNotLoggedInException();
            }
        }
    }
}