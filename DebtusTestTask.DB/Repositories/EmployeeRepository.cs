using DebtusTestTask.DB.Entities;
using DebtusTestTask.DB.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DebtusTestTask.DB.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployees(string? jobTitle = null)
        {
            if (string.IsNullOrEmpty(jobTitle))
            {
                return await _context.Employees.ToListAsync();
            }
            else
            {
                var employeesWithJobTitle = await _context.Employees
                    .Where(e => e.JobTitle == jobTitle)
                    .ToListAsync();

                if (!employeesWithJobTitle.Any())
                {
                    throw new ArgumentException($"Employees with job title '{jobTitle} not found.");
                }

                return employeesWithJobTitle;
            }
        }
        public async Task<IEnumerable<string>> GetJobTitles()
        {
            return await _context.Employees.Select(x => x.JobTitle).Distinct().ToListAsync();
        }
        public async Task<Employee> Get(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee is null)
            {
                throw new ArgumentException($"Employee with id {id} not found.");
            }

            return employee;
        }
        public async Task<Employee> Add(Employee newEmployee)
        {
            if (newEmployee is null)
            {
                throw new ArgumentNullException(nameof(newEmployee));
            }

            await _context.Employees.AddAsync(newEmployee);
            await _context.SaveChangesAsync();

            return newEmployee;
        }
        public async Task<Employee> Update(Employee employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return employee;
        }
        public async Task<Employee> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee is null)
            {
                throw new ArgumentException($"Employee with id {id} not found.");
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }
    }
}