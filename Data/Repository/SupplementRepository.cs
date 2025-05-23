using Microsoft.EntityFrameworkCore;
using SleepAidTrackerApi.Models;

namespace SleepAidTrackerApi.Data.Repository
{
    public class SupplementRepository
    {
        private readonly AppDbContext context;

        public SupplementRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Supplement supplement)
        {
            await context.Supplements.AddAsync(supplement);
        }

        public async Task<Supplement?> GetByIdAsync(int supplementId)
        {
            return await context.Supplements
                .Include(x => x.Doses)
                .FirstOrDefaultAsync(x => x.Id == supplementId);
        }

        public void Update(Supplement supplement)
        {
            context.Update(supplement);
        }

        public async Task<bool> DeleteAsync(int supplementId)
        {
            var supplement = await context.Supplements.FindAsync(supplementId);

            if (supplement == null)
            {
                return false;
            }

            context.Supplements.Remove(supplement);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<List<Supplement>> GetUserSupplements(string userId)
        {
            return await context.Supplements
                .Include(x => x.Doses)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }

}
