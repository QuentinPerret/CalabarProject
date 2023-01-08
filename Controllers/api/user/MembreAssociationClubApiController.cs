using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembreAssociationClubApiController : ControllerBase
{
    private readonly CalabarContext _context;
    public MembreAssociationClubApiController(CalabarContext context)
    {
        _context = context;
    }

    // GET: api/MembreAssociationClubApi
    public async Task<ActionResult<IEnumerable<MembreAssociationClub>>> GetMembreAssociationClubs()
    {
        return await _context.Set<MembreAssociationClub>()
        .Include(m => m.Associations)
        .ToListAsync();
    }

    // GET: api/MembreAssociationClubApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<MembreAssociationClub>> GetMembreAssociationClub(int id)
    {
        var member = await _context.Set<MembreAssociationClub>()
        .Include(m => m.Associations)
        .Where(u => u.Id == id)
        .FirstOrDefaultAsync();
        if (member == null)
            return NotFound();
        return member;
    }

    // DELETE: api/MembreAssociationClubApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMembreAssociationClub(int id)
    {
        var member = await _context.Set<MembreAssociationClub>().FindAsync(id);
        if (member == null)
            return NotFound();

        _context.Set<MembreAssociationClub>().Remove(member);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}