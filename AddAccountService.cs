using ALEHA_API.Models;
using ALEHA_API.Repository;
using System.Drawing.Text;

namespace ALEHA_API.Services
{
    public class AddAccountService : IAddAccountService<IAccountDataProvider<Account>>
    {
        private readonly IAccountDataProvider<Account> _repo; 
        
        public AddAccountService(IAccountDataProvider<Account> repo)
        {
            _repo = repo;
        }

        public string Add(Account account)
        {
            int acc_number = _repo.AddAccountDetails(account);
            if(acc_number == 0)
            {
                return "Failed to add Account";
            }
            else return "Succes" + acc_number.ToString();   
        }
    }
}
