using Microsoft.EntityFrameworkCore;
using Portfolio.Models;
using Portfolio.Models.Model;

namespace Portfolio.Services
{
    public class UserServices
    {
        public readonly AppDbContext context;

        public UserServices(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<UserModel>?> GetAll()
        {
            var orders = await context.Users
                .AsNoTracking()
                .OrderBy(a => a.Usercode)
                .ToListAsync();

            return orders;
        }

        public async Task<UserModel?> GetById(int id)
        {
            var order = await context.Users.FindAsync(id);
            return order == null ? null : order;
        }

        public async Task<bool> GetByUsercode(string usercode)
        {
            var user = await context.Users.Where(u => u.Usercode == usercode).FirstOrDefaultAsync();
            if (user is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<UserModel?> ValidateLogin(RegisterModel record)
        {
            var checkuser = await context.Users
                            .Where(u => u.Usercode == record.usercode && u.Password == record.password)
                            .FirstOrDefaultAsync();
            if (checkuser is null)
            {
                return null;
            }
            return checkuser;
        }

        public async Task<bool?> Add(UserModel record)
        {
            try
            {
                context.Users.Add(record);
                var status = await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool?> Update(UserModel record)
        {
            try
            {
                var existing = await context.Orders.FindAsync(record.Id);
                if (existing is null)
                {
                    return false;
                }
                context.Entry(existing).CurrentValues.SetValues(record);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool?> Delete(UserModel record)
        {
            try
            {
                var existing = await context.Orders.FindAsync(record.Id);
                if (existing is null) { return false; }
                context.Orders.Remove(existing);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
