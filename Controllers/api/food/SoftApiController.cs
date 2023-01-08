using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class SoftApiController : ControllerBase
{
    private readonly CalabarContext _context;
    public SoftApiController(CalabarContext context)
    {
        _context = context;
    }

    // GET: api/SoftApi
    public async Task<ActionResult<IEnumerable<Soft>>> GetSofts()
    {
        return await _context.Set<Soft>()
        .Include(c => c.Association)
        .ToListAsync();

    }

    // GET: api/SoftApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Soft>> GetSoft(int id)
    {
        var soft = await _context.Set<Soft>()
        .Include(b => b.Association)
        .Where(b => b.Id == id)
        .FirstOrDefaultAsync();
        if (soft == null)
            return NotFound();
        return soft;
    }


    // ...
    // POST: api/SoftApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Soft>> PostSoft(SoftDTO softDTO)
    {
        var soft = new Soft(softDTO);

        var asso = _context.Associations.Find(soft.AssociationId);

        soft.Association = asso!;

        _context.Set<Soft>().Add(soft);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSoft), new { id = soft.Id }, soft);
    }

    // ...
    // PUT: api/SoftApi/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSoft(int id, SoftDTO softDTO)
    {
        if (id != softDTO.Id)
            return BadRequest();
        Soft soft = new Soft(softDTO);

        var asso = _context.Associations.Find(soft.AssociationId);
        soft.Association = asso!;

        _context.Entry(soft).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SoftExists(id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    // Returns true if a beer with specified id already exists
    private bool SoftExists(int id)
    {
        return _context.Set<Soft>().Any(m => m.Id == id);
    }

    // DELETE: api/SoftApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBiere(int id)
    {
        var soft = await _context.Set<Soft>().FindAsync(id);
        if (soft == null)
            return NotFound();

        _context.Set<Soft>().Remove(soft);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}