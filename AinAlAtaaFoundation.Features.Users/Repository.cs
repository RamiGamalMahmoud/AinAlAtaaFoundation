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
        public override async Task<User> Create(UserDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                User user = new User()
                {
                    UserName = dataModel.UserName,
                    IsActive = true,
                    IsAdmin = dataModel.IsAdmin,
                    Password = dataModel.Password
                };
                dbContext.Users.Add(user);

                try
                {
                    await dbContext.SaveChangesAsync();
                    _entities.Add(user);
                    return user;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public override async Task<IEnumerable<User>> GetAll(bool reload = false)
        {
            if (!_isLoaded || reload)
            {
                using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
                {
                    IEnumerable<User> users = await dbContext
                        .Users
                        .ToListAsync();
                    SetEntities(users);
                }

            }
            return _entities;
        }

        public async Task<User> GetUser(string userName, string password)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .Users
                    .Where(x => x.UserName == userName && x.Password == password)
                    .Where(x => (bool)x.IsActive)
                    .SingleOrDefaultAsync();
            }
        }

        public override async Task<bool> Update(UserDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!dataModel.IsAdmin)
                {
                    List<User> admins = _entities.Where(x => x.IsAdmin).ToList();
                    User toRemove = admins.Where(x => x.Id == dataModel.Model.Id).FirstOrDefault();
                    admins.Remove(toRemove);
                    if (admins.Count == 0)
                    {
                        throw new Shared.Exceptions.AdminsCountException();
                    }
                }

                User user = await dbContext.Users.Where(x => x.Id == dataModel.Model.Id).FirstOrDefaultAsync();

                user.UserName = dataModel.UserName;
                user.IsActive = dataModel.IsActive;
                user.IsAdmin = dataModel.IsAdmin;
                user.Password = dataModel.Password;

                dbContext.Users.Update(user);

                try
                {
                    await dbContext.SaveChangesAsync();
                    dataModel.UpdateModel(dataModel.Model);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
