using DebtusTestTask.DB.Entities;

namespace DebtusTestTask.DB.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees(string? jobTitle = null);
        Task<IEnumerable<string>> GetJobTitles();
        Task<Employee> Get(int id);
        Task<Employee> Add(Employee newEmployee);
        Task<Employee> Update(Employee employee);
        Task<Employee> Delete(int id);
    }
}