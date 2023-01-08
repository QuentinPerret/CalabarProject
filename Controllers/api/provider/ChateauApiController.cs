using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChateauApiController : ControllerBase
{
    private readonly CalabarContext _context;
    public ChateauApiController(CalabarContext context)
    {
        _context = context;
    }

    // GET: api/ChateauApi
    public async Task<ActionResult<IEnumerable<Chateau>>> GetChateaus()
    {
        return await _context.Set<Chateau>()
        .Include(a => a.Factures)
        .ToListAsync();
    }

    // GET: api/ChateauApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Chateau>> GetChateau(int id)
    {
        var chateau = await _context.Set<Chateau>()
        .Where(b => b.Id == id)
        .Include(b => b.Factures)
        .FirstOrDefaultAsync();
        if (chateau == null)
            return NotFound();
        return chateau;
    }


    // ...
    // POST: api/ChateauApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Chateau>> PostChateau(Chateau chateau)
    {
        _context.Set<Chateau>().Add(chateau);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetChateau), new { id = chateau.Id }, chateau);
    }

    // ...
    // PUT: api/ChateauApi/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutChateau(int id, Chateau chateau)
    {
        if (id != chateau.Id)
            return BadRequest();

        _context.Entry(chateau).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ChateauExists(id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    // Returns true if a Chateau with specified id already exists
    private bool ChateauExists(int id)
    {
        return _context.Set<Chateau>().Any(m => m.Id == id);
    }

    // DELETE: api/ChateauApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChateau(int id)
    {
        var chateau = await _context.Set<Chateau>().FindAsync(id);
        if (chateau == null)
            return NotFound();

        _context.Set<Chateau>().Remove(chateau);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}