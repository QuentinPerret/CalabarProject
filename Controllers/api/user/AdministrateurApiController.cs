using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdministrateurApiController : ControllerBase
{
    private readonly CalabarContext _context;
    public AdministrateurApiController(CalabarContext context)
    {
        _context = context;
    }

    // GET: api/AdministrateurApi
    public async Task<ActionResult<IEnumerable<Administrateur>>> GetAdministrateurs()
    {
        return await _context.Set<Administrateur>()
        .Include(a => a.Associations)
        .ToListAsync();
    }

    // GET: api/AdministrateurApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Administrateur>> GetAdministrateur(int id)
    {
        var admin = await _context.Set<Administrateur>()
        .Where(a => a.Id == id)
        .Include(a => a.Associations)
        .FirstOrDefaultAsync();
        if (admin == null)
            return NotFound();
        return admin;
    }

    // POST: api/AdministrateurApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Administrateur>> PostAdministrateur(Administrateur admin)
    {
        _context.Set<Administrateur>().Add(admin);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAdministrateur), new { id = admin.Id }, admin);
    }

    // PUT: api/AdministrateurApi/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAdministrateur(int id, Administrateur admin)
    {
        if (id != admin.Id)
            return BadRequest();

        _context.Entry(admin).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AdministrateurExists(id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    // Returns true if a Administrateur with specified id already exists
    private bool AdministrateurExists(int id)
    {
        return _context.Set<Administrateur>().Any(m => m.Id == id);
    }

    // DELETE: api/AdministrateurApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAdministrateur(int id)
    {
        var admin = await _context.Set<Administrateur>().FindAsync(id);
        if (admin == null)
            return NotFound();

        _context.Set<Administrateur>().Remove(admin);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}