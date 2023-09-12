using ALEHA_API.Models;

namespace ALEHA_API.Repository
{
    public class AccountDataProvider : IAccountDataProvider<Account>
    {
        private readonly AlehabankDbContext _context;
        public AccountDataProvider(AlehabankDbContext context)
        {
            _context = context;
        }

        public bool IsAccountEmpty()
        {
            if (_context.Accounts == null)
            {
                return true;
            }
            else return false;
        }

        public int AddAccountDetails(Account account)
        {
            if (_context.Accounts != null)
            {
                _context.Accounts.Add(account);
                _context.SaveChanges();
                return account.AccountNumber;
            }
            else return 0;
        }
        //        public Account GetAccountDetail(Account account)
        //       {
        //           return _context.account.SingleOrDefault(x => x.Username == login.Username && x.Password == login.Password);
        //       }
    }
}