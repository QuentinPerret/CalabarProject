using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class BiereApiController : ControllerBase
{
    private readonly CalabarContext _context;
    public BiereApiController(CalabarContext context)
    {
        _context = context;
    }

    // GET: api/BiereApi
    public async Task<ActionResult<IEnumerable<Biere>>> GetBieres()
    {
        return await _context.Set<Biere>()
        .Include(c => c.Association)
        .Include(c => c.Brasserie)
        .ToListAsync();
    }

    // GET: api/BiereApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Biere>> GetBiere(int id)
    {
        var biere = await _context.Set<Biere>()
        .Include(b => b.Association)
        .Include(b => b.Brasserie)
        .Where(b => b.Id == id)
        .FirstOrDefaultAsync();
        if (biere == null)
            return NotFound();
        return biere;
    }


    // ...
    // POST: api/BiereApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Biere>> PostBiere(BiereDTO biereDTO)
    {
        var biere = new Biere(biereDTO);

        var asso = _context.Set<Association>().Find(biere.AssociationId);
        var brasserie = _context.Set<Brasserie>().Find(biere.BrasserieId);

        biere.Association = asso!;
        biere.Brasserie = brasserie!;

        _context.Set<Biere>().Add(biere);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBiere), new { id = biere.Id }, biere);
    }

    // ...
    // PUT: api/BiereApi/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBiere(int id, BiereDTO biereDTO)
    {
        if (id != biereDTO.Id)
            return BadRequest();
        Biere biere = new Biere(biereDTO);

        var asso = _context.Set<Association>().Find(biere.AssociationId);
        var brasserie = _context.Set<Brasserie>().Find(biere.BrasserieId);

        biere.Association = asso!;
        biere.Brasserie = brasserie!;

        _context.Entry(biere).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BiereExists(id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    // Returns true if a beer with specified id already exists
    private bool BiereExists(int id)
    {
        return _context.Set<Biere>().Any(m => m.Id == id);
    }

    // DELETE: api/ConsommableApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBiere(int id)
    {
        var biere = await _context.Set<Biere>().FindAsync(id);
        if (biere == null)
            return NotFound();

        _context.Set<Biere>().Remove(biere);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}