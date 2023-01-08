using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class NourritureApiController : ControllerBase
{
    private readonly CalabarContext _context;
    public NourritureApiController(CalabarContext context)
    {
        _context = context;
    }

    // GET: api/NourritureApi
    public async Task<ActionResult<IEnumerable<Nourriture>>> GetNourritures()
    {
        return await _context.Set<Nourriture>()
        .Include(c => c.Association)
        .ToListAsync();

    }

    // GET: api/NourritureApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Nourriture>> GetNourriture(int id)
    {
        var Nourriture = await _context.Set<Nourriture>()
        .Include(b => b.Association)
        .Where(b => b.Id == id)
        .FirstOrDefaultAsync();
        if (Nourriture == null)
            return NotFound();
        return Nourriture;
    }


    // ...
    // POST: api/NourritureApi
    // To protect from overposting attacks, see https://go.microNourriture.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Nourriture>> PostNourriture(NourritureDTO nourritureDTO)
    {
        var nourriture = new Nourriture(nourritureDTO);

        var asso = _context.Associations.Find(nourriture.AssociationId);

        nourriture.Association = asso!;

        _context.Set<Nourriture>().Add(nourriture);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetNourriture), new { id = nourriture.Id }, nourriture);
    }

    // ...
    // PUT: api/NourritureApi/5
    // To protect from overposting attacks, see https://go.microNourriture.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutNourriture(int id, NourritureDTO nourritureDTO)
    {
        if (id != nourritureDTO.Id)
            return BadRequest();
        Nourriture nourriture = new Nourriture(nourritureDTO);

        var asso = _context.Associations.Find(nourriture.AssociationId);
        nourriture.Association = asso!;

        _context.Entry(nourriture).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!NourritureExists(id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    // Returns true if a beer with specified id already exists
    private bool NourritureExists(int id)
    {
        return _context.Set<Nourriture>().Any(m => m.Id == id);
    }

    // DELETE: api/NourritureApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBiere(int id)
    {
        var nourriture = await _context.Set<Nourriture>().FindAsync(id);
        if (nourriture == null)
            return NotFound();

        _context.Set<Nourriture>().Remove(nourriture);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}