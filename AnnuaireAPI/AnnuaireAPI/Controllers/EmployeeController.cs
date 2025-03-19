using AnnuaireAPI.Data;
using AnnuaireAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly PhoneBookDbContext _context;

    public EmployeeController(PhoneBookDbContext context) { _context = context; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesAsync()
    {
        return await _context.Employees.Include(x => x.Agency).Include(x => x.Service).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployeeAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();
        return employee;
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> PostEmployeeAsync(EmployeeDto employeeDto)
    {
        Employee employee = new(employeeDto);
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEmployeeAsync), new { id = employee.Id }, employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmployeeAsync(int id, EmployeeDto employeeDto)
    {
        if (id != employeeDto.Id) return BadRequest();
        Employee employee = new(employeeDto);
        _context.Entry(employee).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployeeAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("by-agency/{agencyId}")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByAgencyAsync(int agencyId)
    {
        return await _context.Employees.Where(x => x.AgencyId == agencyId).ToListAsync();
    }

    [HttpGet("by-service/{serviceId}")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByServiceAsync(int serviceId)
    {
        return await _context.Employees.Where(x => x.ServiceId == serviceId).ToListAsync();
    }
}
