using ALEHA_API.Models;

namespace ALEHA_API.Services
{
    public interface IAddAccountService<T>
    {
        public string Add(Account account);
    }
}