using Microsoft.AspNetCore.Mvc;
using Paylocity_API.Models;
using Paylocity_API.Models.Employee;
using NLog;
using Paylocity_API.VM;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Paylocity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly PayrollContext _context;

        public EmployeeController(PayrollContext context)
        {
            _context = context;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var employees = await _context.Employees.Include(x => x.Dependants).ToListAsync();

                return Ok(employees);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var employee = await _context.Employees.Where(emp => emp.Id == id).FirstOrDefaultAsync();

                return Ok(employee);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeViewModel employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Missing or Empty Model Properties");

                var addedEmployee = new Employee(employee.FirstName);
                addedEmployee.Id = employee.Id;
                addedEmployee.LastName = employee.LastName;

                _context.Employees.Add(addedEmployee);

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int Id,[FromBody] EmployeeViewModel updateEmployee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Missing or Empty Model Properties");

                var employee = await _context.Employees.Where(emp => emp.Id == Id).FirstOrDefaultAsync();

                //TODO: Improve this message as this could be abused
                if (employee == null)
                    throw new Exception("No Dependant Found with provided Id");

                employee.FirstName = updateEmployee.FirstName;
                employee.LastName = updateEmployee.LastName;
                employee.CompensationType = updateEmployee.CompensationType;

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            try
            {
                var deletedEmployee = new Employee("");
                deletedEmployee.Id = id;
                _context.Employees.Remove(deletedEmployee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
