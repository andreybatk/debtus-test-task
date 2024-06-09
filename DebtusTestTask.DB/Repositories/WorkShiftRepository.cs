using DebtusTestTask.DB.Entities;
using DebtusTestTask.DB.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DebtusTestTask.DB.Repositories
{
    public class WorkShiftRepository : IWorkShiftRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkShiftRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WorkShift?> Get(int employeeId)
        {
            return await _context.WorkShifts.LastOrDefaultAsync(x => x.EmployeeId == employeeId);
        }
        public async Task Add(WorkShift workShift)
        {
            await _context.WorkShifts.AddAsync(workShift);
            await _context.SaveChangesAsync();
        }
        public async Task Update(WorkShift workShift)
        {
            if (workShift is null)
            {
                throw new ArgumentNullException(nameof(workShift));
            }

            _context.WorkShifts.Update(workShift);
            await _context.SaveChangesAsync();
        }
    }
}