using Application.Account.Models;
using AutoMapper;

namespace Application.Account
{
    internal class AccountMapper : Profile
    {
        public AccountMapper()
        {
            CreateMap<Domain.Account, AccountInfo>();
            CreateMap<Domain.AccountView, AccountView>();
        }
    }
}
