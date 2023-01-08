using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssociationApiController : ControllerBase
{
    private readonly CalabarContext _context;
    public AssociationApiController(CalabarContext context)
    {
        _context = context;
    }

    // GET: api/AssociationApi
    public async Task<ActionResult<IEnumerable<Association>>> GetAssociations()
    {
        return await _context.Associations
        .Include(a => a.MembreAssociationClubs)
        .Include(a => a.Factures)
        // .Include(a => a.President)
        .ToListAsync();
    }

    // GET: api/AssociationApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Association>> GetAssociation(int id)
    {
        var asso = await _context.Associations
        .Include(a => a.Factures)
        .Include(a => a.MembreAssociationClubs)
        // .Include(a => a.President)
        .Where(a => a.Id == id)
        .FirstOrDefaultAsync();
        if (asso == null)
            return NotFound();
        return asso;
    }

    // DELETE: api/AssociationApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssociation(int id)
    {
        var asso = await _context.Associations.FindAsync(id);
        if (asso == null)
            return NotFound();

        _context.Associations.Remove(asso);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}