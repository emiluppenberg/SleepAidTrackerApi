using Microsoft.EntityFrameworkCore;
using SleepAidTrackerApi.Models;

namespace SleepAidTrackerApi.Data.Repository
{
    public class DoseRepository
    {
        private readonly AppDbContext context;

        public DoseRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Dose dose)
        {
            await context.Doses.AddAsync(dose);
        }

        public async Task<Dose?> GetByIdAsync(int doseId)
        {
            return await context.Doses.FindAsync(doseId);
        }

        public void Update(Dose dose)
        {
            context.Update(dose);
        }

        public async Task<bool> DeleteAsync(int doseId)
        {
            var dose = await context.Doses.FindAsync(doseId);

            if (dose == null)
            {
                return false;
            }

            context.Doses.Remove(dose);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<List<Dose>> GetSupplementDosesAsync(int supplementId)
        {
            return await context.Doses
                .Where(x => x.SupplementId == supplementId)
                .ToListAsync();
        }

        public async Task<List<Dose>> GetAllUserDosesAsync(string userId)
        {
            return await context.Doses
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}
