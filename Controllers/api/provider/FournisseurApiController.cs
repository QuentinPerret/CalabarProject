using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class FournisseurApiController : ControllerBase
{
    private readonly CalabarContext _context;
    public FournisseurApiController(CalabarContext context)
    {
        _context = context;
    }

    // GET: api/FournisseurApi
    public async Task<ActionResult<IEnumerable<Fournisseur>>> GetFournisseurs()
    {
        return await _context.Fournisseurs
        .Include(f => f.Factures)
        .ToListAsync();
    }

    // GET: api/FournisseurApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Fournisseur>> GetFournisseur(int id)
    {
        var fournisseur = await _context.Fournisseurs
        .Where(f => f.Id == id)
        .Include(f => f.Factures)
        .FirstOrDefaultAsync();
        if (fournisseur == null)
            return NotFound();
        return fournisseur;
    }

    // DELETE: api/FournisseurApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFournisseur(int id)
    {
        var fournisseur = await _context.Fournisseurs.FindAsync(id);
        if (fournisseur == null)
            return NotFound();

        _context.Fournisseurs.Remove(fournisseur);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}