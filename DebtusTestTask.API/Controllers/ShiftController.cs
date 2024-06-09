using DebtusTestTask.DB.Entities;
using DebtusTestTask.DB.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DebtusTestTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly ILogger<ShiftController> _logger;
        private readonly IWorkShiftRepository _workShiftRepository;
        private readonly IEmployeeRepository _employeesRepository;

        public ShiftController(ILogger<ShiftController> logger, IWorkShiftRepository workShiftRepository, IEmployeeRepository employeesRepository)
        {
            _logger = logger;
            _workShiftRepository = workShiftRepository;
            _employeesRepository = employeesRepository;
        }

        [HttpPut("startshift/{id}")]
        public async Task<IActionResult> StartShift(int id, DateTime startShiftDate)
        {
            try
            {
                var employee = await _employeesRepository.Get(id);
                var workShift = employee.WorkShifts.LastOrDefault();

                if (workShift != null)
                {
                    if (workShift.StartShift != null && workShift.EndShift != null)
                    {
                        await _workShiftRepository.Add(new WorkShift { EmployeeId = id, Employee = employee, StartShift = startShiftDate });
                        return Ok();
                    }
                    else if (workShift.StartShift == null)
                    {
                        workShift.StartShift = startShiftDate;
                        await _workShiftRepository.Update(workShift);
                        return Ok();
                    }
                }
                else
                {
                    await _workShiftRepository.Add(new WorkShift { EmployeeId = id, Employee = employee, StartShift = startShiftDate });
                    return Ok();
                }

                return BadRequest($"Error added StartShift with id: {id}");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error added StartShift.");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("endshift/{id}")]
        public async Task<IActionResult> EndShift(int id, DateTime endShiftDate)
        {
            try
            {
                var employee = await _employeesRepository.Get(id);
                var workShift = employee.WorkShifts.LastOrDefault();

                if (workShift != null)
                {
                    if (workShift.StartShift == null)
                    {
                        return BadRequest($"Employee with id: {id} need to marked StartShift.");
                    }
                    else if (workShift.EndShift == null)
                    {
                        workShift.EndShift = endShiftDate;
                        workShift.HoursWorked = (int)(workShift.EndShift - workShift.StartShift).Value.TotalHours;
                        await _workShiftRepository.Update(workShift);
                        return Ok();
                    }
                }

                return BadRequest($"Error added EndShift with id: {id}");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error added EndShift.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}