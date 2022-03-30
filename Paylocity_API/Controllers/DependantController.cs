using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paylocity_API.Models;
using Paylocity_API.VM;

namespace Paylocity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DependantController : ControllerBase
    {
        private readonly PayrollContext _context;

        public DependantController(PayrollContext context)
        {
            _context = context;
        }

        [Route("GetList/{employeeId}")]
        [HttpGet]
        public async Task<IActionResult> GetList(int employeeId)
        {
            try
            {
                var dependants = await _context.Dependants.Where(d => d.EmployeeId == employeeId).ToListAsync();

                //TODO: Improve this message as this could be abused
                if (dependants.Count == 0)
                    throw new Exception("No Dependant Found with provided Id");

                return Ok(dependants);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var dependant = await _context.Dependants.Where(d => d.Id == id).FirstOrDefaultAsync();

                //TODO: Improve this message as this could be abused
                if (dependant == null)
                    throw new Exception("No Dependant Found with provided Id");

                return Ok(dependant);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST api/<DependantController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DependantViewModel dependant)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Missing or Empty Model Properties");

                var addedDependant = new Dependant(dependant.FirstName);
                addedDependant.LastName = dependant.LastName;
                addedDependant.EmployeeId = dependant.EmployeeId;
                _context.Dependants.Add(addedDependant);

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int Id, [FromBody] DependantViewModel updateDependant)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Missing or Empty Model Properties");

                var dependant = await _context.Dependants.Where(d => d.Id == Id).FirstOrDefaultAsync();

                //TODO: Improve this message as this could be abused
                if (dependant == null)
                    throw new Exception("No Dependant Found with provided Id");

                dependant.FirstName = updateDependant.FirstName;
                dependant.LastName = updateDependant.LastName;
                dependant.Relationship = updateDependant.Relationship;

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // DELETE api/<DependantController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            try
            {
                if (id <= 0) 
                    throw new InvalidDataException("Id provided not valid");

                var deletedDependant = new Dependant("");
                deletedDependant.Id = id;

                _context?.Dependants.Remove(deletedDependant);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

    }
}
