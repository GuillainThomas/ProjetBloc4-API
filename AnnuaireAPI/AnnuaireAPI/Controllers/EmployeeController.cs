using AnnuaireAPI.Data;
using AnnuaireAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly PhoneBookDbContext _context;

    public EmployeeController(PhoneBookDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<EmployeeDto>>> GetAllEmployeeAsync()
    {
        List<EmployeeDto> employeeDto = await _context.Employees.Select(e => new EmployeeDto(e)).ToListAsync();
        return employeeDto;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetEmployeeByIdAsync(int id)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        if (employee == null) return NotFound();
        var employeeDto = new EmployeeDto(employee);
        return employeeDto;
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetFilteredEmployeeAsyn([FromQuery] string? agencyId, [FromQuery] string? serviceId, [FromQuery] string? name)
    {
        // Convert agencyId and serviceId from string to int? if possible
        int? parsedAgencyId = !string.IsNullOrWhiteSpace(agencyId) && int.TryParse(agencyId, out int aId) ? aId : null;
        int? parsedServiceId = !string.IsNullOrWhiteSpace(serviceId) && int.TryParse(serviceId, out int sId) ? sId : null;

        // Start with the base query
        IQueryable<Employee> query = _context.Employees.Include(e => e.Agency).Include(e => e.Service);

        // Filter by Agency if provided
        if (parsedAgencyId.HasValue)
            query = query.Where(e => e.Agency.Id == parsedAgencyId.Value);

        // Filter by Service if provided
        if (parsedServiceId.HasValue)
            query = query.Where(e => e.Service.Id == parsedServiceId.Value);

        // Filter by Employee name if provided
        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name));

        // Execute query and return results
        var listEmployeeDto = await query.Select(e => new EmployeeDto(e)).ToListAsync();
        return Ok(listEmployeeDto);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> PostEmployeeAsync(EmployeeDto employeeDto)
    {
        Employee employee = new(employeeDto);
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        employeeDto = new(employee);
        return CreatedAtAction(nameof(GetEmployeeByIdAsync), new { id = employeeDto.Id }, employeeDto);
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
}
