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
    public async Task<ActionResult<IEnumerable<Service>>> GetAgencys()
    {
        return await _context.Services.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Service>> GetAgency(int id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service == null) return NotFound();
        return service;
    }

    [HttpPost]
    public async Task<ActionResult<Service>> PostAgency(Service service)
    {
        _context.Services.Add(service);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAgency), new { id = service.Id }, service);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAgency(int id, Service service)
    {
        if (id != service.Id) return BadRequest();
        _context.Entry(service).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAgency(int id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service == null) return NotFound();
        _context.Services.Remove(service);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
