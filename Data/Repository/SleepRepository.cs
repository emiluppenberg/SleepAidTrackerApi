using Microsoft.EntityFrameworkCore;
using SleepAidTrackerApi.Models;

namespace SleepAidTrackerApi.Data.Repository
{
    public class SleepRepository
    {
        private readonly AppDbContext context;

        public SleepRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Sleep sleep)
        {
            await context.Sleeps.AddAsync(sleep);
        }

        public async Task<Sleep?> GetByIdAsync(int sleepId)
        {
            return await context.Sleeps.FindAsync(sleepId);
        }

        public void Update(Sleep sleep)
        {
            context.Update(sleep);
        }

        public async Task<bool> DeleteAsync(int sleepId)
        {
            var sleep = await context.Sleeps.FindAsync(sleepId);

            if (sleep == null)
            {
                return false;
            }

            context.Sleeps.Remove(sleep);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<List<Sleep>> GetUserSleeps(string userId)
        {
            return await context.Sleeps
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }

}
