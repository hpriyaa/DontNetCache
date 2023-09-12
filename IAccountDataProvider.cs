using ALEHA_API.Models;

namespace ALEHA_API.Repository
{
    public interface IAccountDataProvider<Account>
    {
        public bool IsAccountEmpty();
        public int AddAccountDetails(Account account);
    }

}