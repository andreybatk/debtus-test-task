using DebtusTestTask.DB.Entities;

namespace DebtusTestTask.DB.Interfaces
{
    public interface IWorkShiftRepository
    {
        Task<WorkShift?> Get(int employeeId);
        Task Add(WorkShift workShift);
        Task Update(WorkShift workShift);
    }
}