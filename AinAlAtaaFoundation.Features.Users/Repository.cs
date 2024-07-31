using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Users
{
    internal class Repository(AppDbContextFactory dbContextFactory) : RepositoryBase<User, UserDataModel>(dbContextFactory)
    {
        public override Task<User> Create(UserDataModel dataModel)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<User>> GetAll(bool reload = false)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .Users
                    .ToListAsync();
            }
        }

        public async Task<User> GetUser(string userName, string password)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .Users
                    .Where(x => x.UserName == userName && EF.Property<string>(x, "Password") == password)
                    .SingleOrDefaultAsync();
            }
        }

        public override Task<bool> Update(UserDataModel dataModel)
        {
            throw new NotImplementedException();
        }
    }
}
