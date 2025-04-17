using AnnuaireAPI.Data;
using AnnuaireAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ServiceController : ControllerBase
{
    private readonly PhoneBookDbContext _context;

    public ServiceController(PhoneBookDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<ServiceDto>>> GetAllServiceAsync()
    {
        List<ServiceDto> serviceDto = await _context.Services.Select(s => new ServiceDto(s)).ToListAsync();
        return serviceDto;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceDto>> GetServiceByIdAsync(int id)
    {
        var service = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
        if (service == null) return NotFound();
        var serviceDto = new ServiceDto(service);
        return serviceDto;
    }

    [HttpPost]
    public async Task<ActionResult<ServiceDto>> PostServiceAsync(ServiceDto serviceDto)
    {
        Service service = new(serviceDto);
        _context.Services.Add(service);
        await _context.SaveChangesAsync();
        serviceDto = new(service);
        return CreatedAtAction(nameof(GetServiceByIdAsync), new { id = serviceDto.Id }, serviceDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutServiceAsync(int id, ServiceDto serviceDto)
    {
        if (id != serviceDto.Id) return BadRequest();
        Service service = new(serviceDto);
        _context.Entry(service).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServiceAsync(int id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service == null) return NotFound();
        _context.Services.Remove(service);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
