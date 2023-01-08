using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrasserieApiController : ControllerBase
{
    private readonly CalabarContext _context;
    public BrasserieApiController(CalabarContext context)
    {
        _context = context;
    }

    // GET: api/BrasserieApi
    public async Task<ActionResult<IEnumerable<Brasserie>>> GetBrasseries()
    {
        return await _context.Set<Brasserie>()
        .Include(b => b.Factures)
        .ToListAsync();
    }

    // GET: api/BrasserieApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Brasserie>> GetBrasserie(int id)
    {
        var brasserie = await _context.Set<Brasserie>()
        .Where(b => b.Id == id)
        .Include(b => b.Factures)
        .FirstOrDefaultAsync();
        if (brasserie == null)
            return NotFound();
        return brasserie;
    }


    // ...
    // POST: api/BrasserieApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Brasserie>> PostBrasserie(Brasserie brasserie)
    {
        _context.Set<Brasserie>().Add(brasserie);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBrasserie), new { id = brasserie.Id }, brasserie);
    }

    // ...
    // PUT: api/BrasserieApi/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBrasserie(int id, Brasserie brasserie)
    {
        if (id != brasserie.Id)
            return BadRequest();

        _context.Entry(brasserie).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BrasserieExists(id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    // Returns true if a Brasserie with specified id already exists
    private bool BrasserieExists(int id)
    {
        return _context.Set<Brasserie>().Any(m => m.Id == id);
    }

    // DELETE: api/BrasserieApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrasserie(int id)
    {
        var brasserie = await _context.Set<Brasserie>().FindAsync(id);
        if (brasserie == null)
            return NotFound();

        _context.Set<Brasserie>().Remove(brasserie);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}