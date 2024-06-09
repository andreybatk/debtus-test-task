using AutoMapper;
using DebtusTestTask.API.Contracts;
using DebtusTestTask.DB.Entities;
using DebtusTestTask.DB.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DebtusTestTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepository _employeesRepository;

        public EmployeeController(IMapper mapper, ILogger<EmployeeController> logger, IEmployeeRepository employeesRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _employeesRepository = employeesRepository;
        }

        [HttpGet("getemployees")]
        public async Task<IActionResult> GetEmployees([FromQuery] string? jobTitle = null)
        {
            try
            {
                var employees = await _employeesRepository.GetEmployees(jobTitle);
                var employeesResponse = _mapper.Map<IEnumerable<EmployeeResponse>>(employees);
                return Ok(employeesResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting employees.");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("getjobtitles")]
        public async Task<IActionResult> GetJobTitles()
        {
            try
            {
                var jobTitles = await _employeesRepository.GetJobTitles();
                return Ok(jobTitles);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting jobtitles.");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            try
            {
                var employee = await _employeesRepository.Get(id);
                var employeeResponse = _mapper.Map<EmployeeResponse>(employee);
                return Ok(employeeResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting employee with id: {id}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddEmployee([FromBody] NewEmployeeRequest newEmployee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    IEnumerable<string> errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                    return BadRequest(errorMessages);
                }

                var employee = _mapper.Map<Employee>(newEmployee);
                employee = await _employeesRepository.Add(employee);
                var employeeResponse = _mapper.Map<EmployeeResponse>(employee);
                return Ok(employeeResponse);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error added employee.");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int id, UpdateEmployeeRequest updateEmployee)
        {
            try
            {
                var employee = await _employeesRepository.Get(id);
                _mapper.Map(updateEmployee, employee);
                employee = await _employeesRepository.Update(employee);

                var employeeResponse = _mapper.Map<EmployeeResponse>(employee);
                return Ok(employeeResponse);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error added employee.");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _employeesRepository.Delete(id);
                var employeeResponse = _mapper.Map<EmployeeResponse>(employee);
                return Ok(employeeResponse);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting employee with id: {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}