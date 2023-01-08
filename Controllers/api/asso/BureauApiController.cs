using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class BureauApiController : ControllerBase
{
    private readonly CalabarContext _context;
    public BureauApiController(CalabarContext context)
    {
        _context = context;
    }

    // GET: api/BureauApi
    public async Task<ActionResult<IEnumerable<Bureau>>> GetBureaux()
    {
        return await _context.Set<Bureau>()
        .Include(b => b.MembreAssociationClubs)
        .Include(b => b.Factures)
        // .Include(b => b.President)
        .Include(b => b.Clubs)
        .ToListAsync();
    }

    // GET: api/BureauApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Bureau>> GetBureau(int id)
    {
        var asso = await _context.Set<Bureau>()
        .Where(a => a.Id == id)
        .Include(b => b.MembreAssociationClubs)
        .Include(b => b.Factures)
        // .Include(b => b.President)
        .Include(b => b.Clubs)
        .FirstOrDefaultAsync();
        if (asso == null)
            return NotFound();
        return asso;
    }


    // ...
    // POST: api/BureauApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Bureau>> PostBureau(BureauDTO bureauDTO)
    {
        var bureau = new Bureau(bureauDTO);

        _context.Set<Bureau>().Add(bureau);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBureau), new { id = bureau.Id }, bureau);
    }

    // ...
    // PUT: api/BureauApi/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBureau(int id, BureauDTO bureauDTO)
    {
        if (id != bureauDTO.Id)
            return BadRequest();

        Bureau bureau = new Bureau(bureauDTO);

        _context.Entry(bureau).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BureauExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // Returns true if a bureau with specified id already exists
    private bool BureauExists(int id)
    {
        return _context.Set<Bureau>().Any(m => m.Id == id);
    }

    // ...
    // DELETE: api/BureauApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBureau(int id)
    {
        var bureau = await _context.Set<Bureau>().FindAsync(id);
        if (bureau == null)
            return NotFound();

        _context.Set<Bureau>().Remove(bureau);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    // ...
}