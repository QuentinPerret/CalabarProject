using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class CollaborateurApiController : ControllerBase
{
    private readonly CalabarContext _context;
    public CollaborateurApiController(CalabarContext context)
    {
        _context = context;
    }

    // GET: api/CollaborateurApi
    public async Task<ActionResult<IEnumerable<Collaborateur>>> GetCollaborateurs()
    {
        return await _context.Set<Collaborateur>()
        .Include(c => c.Associations)
        .ToListAsync();
    }

    // GET: api/CollaborateurApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Collaborateur>> GetCollaborateur(int id)
    {
        var collaborator = await _context.Set<Collaborateur>()
        .Where(c => c.Id == id)
        .Include(c => c.Associations)
        .FirstOrDefaultAsync();
        if (collaborator == null)
            return NotFound();
        return collaborator;
    }

    // POST: api/CollaborateurApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Collaborateur>> PostCollaborateur(Collaborateur collaborator)
    {
        _context.Set<Collaborateur>().Add(collaborator);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCollaborateur), new { id = collaborator.Id }, collaborator);
    }

    // PUT: api/CollaborateurApi/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCollaborateur(int id, Collaborateur collaborator)
    {
        if (id != collaborator.Id)
            return BadRequest();

        _context.Entry(collaborator).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CollaborateurExists(id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    // Returns true if a Collaborateur with specified id already exists
    private bool CollaborateurExists(int id)
    {
        return _context.Set<Collaborateur>().Any(m => m.Id == id);
    }

    // DELETE: api/CollaborateurApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCollaborateur(int id)
    {
        var collaborator = await _context.Set<Collaborateur>().FindAsync(id);
        if (collaborator == null)
            return NotFound();

        _context.Set<Collaborateur>().Remove(collaborator);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}