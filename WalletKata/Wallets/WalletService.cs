using System.Collections.Generic;
using WalletKata.Exceptions;
using WalletKata.Users;

namespace WalletKata.Wallets
{
    public class WalletService
    {
        private readonly WalletDAO walletDao;

        public WalletService(WalletDAO walletDao)
        {
            this.walletDao = walletDao;
        }

        public List<Wallet> GetWalletsByUser(User user)
        {
            List<Wallet> walletList = new List<Wallet>();
            User loggedUser = UserSession.GetInstance().GetLoggedUser();
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
                    walletList = this.walletDao.FindWalletsByUser(user);
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