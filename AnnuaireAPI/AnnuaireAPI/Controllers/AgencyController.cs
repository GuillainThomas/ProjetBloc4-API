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
    public async Task<ActionResult<List<AgencyDto>>> GetAllAgencyAsync()
    {
        List<AgencyDto> agencyDto = await _context.Agencies.Select(a => new AgencyDto(a)).ToListAsync();
        return agencyDto;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AgencyDto>> GetAgencyByIdAsync(int id)
    {
        var agency = await _context.Agencies.FirstOrDefaultAsync(a => a.Id == id);
        if (agency == null) return NotFound();
        var agencyDto = new AgencyDto(agency);
        return agencyDto;
    }

    [HttpPost]
    public async Task<ActionResult<AgencyDto>> PostAgencyAsync(AgencyDto agencyDto)
    {
        Agency agency = new(agencyDto);
        _context.Agencies.Add(agency);
        await _context.SaveChangesAsync();
        agencyDto = new(agency);
        return CreatedAtAction(nameof(GetAgencyByIdAsync), new { id = agencyDto.Id }, agencyDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAgencyAsync(int id, AgencyDto agencyDto)
    {
        if (id != agencyDto.Id) return BadRequest();
        Agency agency = new(agencyDto);
        _context.Entry(agency).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAgencyAsync(int id)
    {
        var agency = await _context.Agencies.FindAsync(id);
        if (agency == null) return NotFound();
        _context.Agencies.Remove(agency);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
