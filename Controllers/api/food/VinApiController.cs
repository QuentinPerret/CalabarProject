using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class VinApiController : ControllerBase
{
    private readonly CalabarContext _context;
    public VinApiController(CalabarContext context)
    {
        _context = context;
    }

    // GET: api/VinApi
    public async Task<ActionResult<IEnumerable<Vin>>> GetVins()
    {
        return await _context.Set<Vin>()
        .Include(c => c.Association)
        .Include(c => c.Chateau)
        .ToListAsync();

    }

    // GET: api/VinApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Vin>> GetVin(int id)
    {
        var vin = await _context.Set<Vin>()
        .Include(b => b.Association)
        .Include(c => c.Chateau)
        .Where(b => b.Id == id)
        .FirstOrDefaultAsync();
        if (vin == null)
            return NotFound();
        return vin;
    }


    // ...
    // POST: api/VinApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Vin>> PostVin(VinDTO vinDTO)
    {
        var vin = new Vin(vinDTO);

        var asso = _context.Set<Association>().Find(vin.AssociationId);
        var chateau = _context.Set<Chateau>().Find(vin.ChateauId);

        vin.Association = asso!;
        vin.Chateau = chateau!;

        _context.Set<Vin>().Add(vin);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVin), new { id = vin.Id }, vin);
    }

    // ...
    // PUT: api/VinApi/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVin(int id, VinDTO vinDTO)
    {
        if (id != vinDTO.Id)
            return BadRequest();
        Vin vin = new Vin(vinDTO);

        var asso = _context.Set<Association>().Find(vin.AssociationId);
        var chateau = _context.Set<Chateau>().Find(vin.ChateauId);

        vin.Association = asso!;
        vin.Chateau = chateau!;

        _context.Entry(vin).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VinExists(id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    // Returns true if a wine with specified id already exists
    private bool VinExists(int id)
    {
        return _context.Set<Vin>().Any(m => m.Id == id);
    }

    // DELETE: api/VinApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVin(int id)
    {
        var vin = await _context.Set<Vin>().FindAsync(id);
        if (vin == null)
            return NotFound();

        _context.Set<Vin>().Remove(vin);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}