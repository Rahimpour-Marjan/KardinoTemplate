using Domain.Common;
using Domain.Resources;

namespace Domain
{
    public interface IAccountRepository
    {
        Task<int> Create(Account model);
        Task<bool> IsExistNationCode(string nationalCode);
        Task<bool> IsExistAccountalNumber(string AccountalNumber);
        Task<Tuple<IList<AccountView>, int>> FindAll(QueryFilter? queryFilter);
        Task<Account> FindById(int id);
        Task Update(Account model);
        Task Delete(int id);
        Task AccountDeleteAll(int[] ids);
    }
}
