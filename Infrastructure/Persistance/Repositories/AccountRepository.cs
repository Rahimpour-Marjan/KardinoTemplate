using Dapper;
using Domain;
using Domain.Common;
using Domain.Resources;
using Infrastructure.Persistance.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly KardinoTemplateDbContext _db;
        public AccountRepository(KardinoTemplateDbContext db)
        {
            _db = db;
        }
        public async Task<int> Create(Account account)
        {
            var result = await _db.Accounts.AddAsync(account);
            return result.Entity.Id;
        }
        public async Task<Tuple<IList<AccountView>, int>> FindAll(QueryFilter? queryFilter)
        {
            //Edited later
            using IDbConnection connection = new SqlConnection(_db.Database.GetDbConnection().ConnectionString);

            var sqlQuery = @"select 
                Account.Id,
                CONCAT(Account.FirstName, ' ', Account.LastName) as FullName,
                Account.UserType,
                Account.Gender,
                Account.BirthDate,
                Account.NationalCode,
                Account.Phone,
                Account.ExtraPhone1,
                Account.ExtraPhone2,
                Account.ExtraPhone3,
                Account.Email,
                Account.ExtraEmail,
                Account.Fax,
                Account.Website,
                Account.Instagram,
                Account.Telegram,
                Account.WhatsApp,
                Account.Linkedin,
                Account.Facebook,
                Account.Address,
                Account.LocationLong,
                Account.LocationLat,
                Account.Job,
                Account.Company,
                Account.CompanyNo,
                Account.FatherName,
                Account.AccountalNumber,
                Account.IsActive,
                Account.WorkingHoursRate,
                Account.ReagentName,
                Account.ReagentCode,
                Account.ImageUrl,
                Account.DigitalSignatureUrl,
                Account.ResumeUrl,
                Account.SpacialAccount,
                Account.IsPublic,
                Account.EmployeementDate,
                Account.ModifiedDate,
                Account.CreateDate
                from Accounts Account";


            var resultSet = await connection.QueryAsync<AccountView>(sqlQuery);


            var query = resultSet.GroupBy(x => x.Id, (key, g) => g.Last()).AsQueryable();

            var totalRecords = query.Count();

            query = query.ApplyOrdering(queryFilter, "Query");

            query = query.ApplyPaging(queryFilter);

            return new Tuple<IList<AccountView>, int>(query.ToList(), totalRecords);
        }
        public async Task<Account> FindById(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _db.Accounts
                        .FirstOrDefaultAsync(t => t.Id == id && t.UserType != Domain.Enums.UserType.SystemUser);
        }
        public async Task Update(Account account)
        {
            _db.Entry<Account>(account).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            var account = await FindById(id);
            _db.Entry((Account)account).State = EntityState.Deleted;
        }
        public async Task AccountDeleteAll(int[] ids)
        {
            var juncRest = await _db.Accounts.Where(x => ids.Contains(x.Id) && x.UserType != Domain.Enums.UserType.SystemUser).ToListAsync();
            _db.Accounts.RemoveRange(juncRest);
        }
        public async Task<bool> IsExistNationCode(string nationalCode)
        {
            return await _db.Accounts.AnyAsync(x => x.NationalCode == nationalCode);
        }

        public async Task<bool> IsExistAccountalNumber(string accountalNumber)
        {
            return await _db.Accounts.AnyAsync(x => x.AccountalNumber == accountalNumber);
        }
    }
}
