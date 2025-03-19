using AnnuaireAPI.Data;
using AnnuaireAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class AgencyController : ControllerBase
{
    private readonly PhoneBookDbContext _context;

    public AgencyController(PhoneBookDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Agency>>> GetAgencys()
    {
        return await _context.Agencies.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Agency>> GetAgency(int id)
    {
        var agency = await _context.Agencies.FindAsync(id);
        if (agency == null) return NotFound();
        return agency;
    }

    [HttpPost]
    public async Task<ActionResult<Agency>> PostAgency(Agency agency)
    {
        _context.Agencies.Add(agency);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAgency), new { id = agency.Id }, agency);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAgency(int id, Agency agency)
    {
        if (id != agency.Id) return BadRequest();
        _context.Entry(agency).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAgency(int id)
    {
        var agency = await _context.Agencies.FindAsync(id);
        if (agency == null) return NotFound();
        _context.Agencies.Remove(agency);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
