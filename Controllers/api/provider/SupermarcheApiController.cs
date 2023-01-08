using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupermarcheApiController : ControllerBase
{
    private readonly CalabarContext _context;
    public SupermarcheApiController(CalabarContext context)
    {
        _context = context;
    }

    // GET: api/SupermarcheApi
    public async Task<ActionResult<IEnumerable<Supermarche>>> GetSupermarches()
    {
        return await _context.Set<Supermarche>()
        .Include(s => s.Factures)
        .ToListAsync();
    }

    // GET: api/SupermarcheApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Supermarche>> GetSupermarche(int id)
    {
        var supermarche = await _context.Set<Supermarche>()
        .Where(s => s.Id == id)
        .Include(s => s.Factures)
        .FirstOrDefaultAsync();
        if (supermarche == null)
            return NotFound();
        return supermarche;
    }


    // ...
    // POST: api/SupermarcheApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Supermarche>> PostSupermarche(Supermarche supermarche)
    {
        _context.Set<Supermarche>().Add(supermarche);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSupermarche), new { id = supermarche.Id }, supermarche);
    }

    // ...
    // PUT: api/SupermarcheApi/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSupermarche(int id, Supermarche supermarche)
    {
        if (id != supermarche.Id)
            return BadRequest();

        _context.Entry(supermarche).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SupermarcheExists(id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    // Returns true if a Supermarche with specified id already exists
    private bool SupermarcheExists(int id)
    {
        return _context.Set<Supermarche>().Any(m => m.Id == id);
    }

    // DELETE: api/SupermarcheApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSupermarche(int id)
    {
        var supermarche = await _context.Set<Supermarche>().FindAsync(id);
        if (supermarche == null)
            return NotFound();

        _context.Set<Supermarche>().Remove(supermarche);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}