using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class FactureApiController : ControllerBase
{
    private readonly CalabarContext _context;
    public FactureApiController(CalabarContext context)
    {
        _context = context;
    }

    // GET: api/Facturepi
    public async Task<ActionResult<IEnumerable<Facture>>> GetFactures()
    {
        return await _context.Factures
        .Include(f => f.Fournisseur)
        .ToListAsync();
    }

    // GET: api/FactureApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Facture>> GetFacture(int id)
    {
        var facture = await _context.Factures
        .Include(f => f.Fournisseur)
        .Where(f => f.Id == id)
        .FirstOrDefaultAsync();
        if (facture == null)
            return NotFound();
        return facture;
    }

    // ...
    // POST: api/FactureApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Facture>> PostFacture(FactureDTO FactureDTO)
    {
        var Facture = new Facture(FactureDTO);

        var asso = _context.Set<Association>().Find(Facture.AssociationId);
        var fournisseur = _context.Set<Fournisseur>().Find(Facture.FournisseurId);

        Facture.Association = asso!;
        Facture.Fournisseur = fournisseur!;

        _context.Set<Facture>().Add(Facture);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFacture), new { id = Facture.Id }, Facture);
    }

    // ...
    // PUT: api/FactureApi/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFacture(int id, FactureDTO FactureDTO)
    {
        if (id != FactureDTO.Id)
            return BadRequest();
        Facture Facture = new Facture(FactureDTO);

        var asso = _context.Set<Association>().Find(Facture.AssociationId);
        var fournisseur = _context.Set<Fournisseur>().Find(Facture.FournisseurId);

        Facture.Association = asso!;
        Facture.Fournisseur = fournisseur!;

        _context.Entry(Facture).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FactureExists(id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    // Returns true if a wine with specified id already exists
    private bool FactureExists(int id)
    {
        return _context.Set<Facture>().Any(m => m.Id == id);
    }

    // DELETE: api/FactureApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFacture(int id)
    {
        var Facture = await _context.Set<Facture>().FindAsync(id);
        if (Facture == null)
            return NotFound();

        _context.Set<Facture>().Remove(Facture);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}