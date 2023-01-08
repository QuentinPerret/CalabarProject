using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class UtilisateurCommunApiController : ControllerBase
{
    private readonly CalabarContext _context;
    public UtilisateurCommunApiController(CalabarContext context)
    {
        _context = context;
    }

    // GET: api/UtilisateurCommunApi
    public async Task<ActionResult<IEnumerable<UtilisateurCommun>>> GetUtilisateurCommuns()
    {
        return await _context.Set<UtilisateurCommun>().ToListAsync();
    }

    // GET: api/UtilisateurCommunApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UtilisateurCommun>> GetUtilisateurCommun(int id)
    {
        var user = await _context.Set<UtilisateurCommun>().FindAsync(id);
        if (user == null)
            return NotFound();
        return user;
    }

    // POST: api/UtilisateurCommunApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<UtilisateurCommun>> PostUtilisateurCommun(UtilisateurCommun user)
    {
        _context.Set<UtilisateurCommun>().Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUtilisateurCommun), new { id = user.Id }, user);
    }

    // PUT: api/UtilisateurCommunApi/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUtilisateurCommun(int id, UtilisateurCommun user)
    {
        if (id != user.Id)
            return BadRequest();

        _context.Entry(user).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UtilisateurCommunExists(id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    // Returns true if a UtilisateurCommun with specified id already exists
    private bool UtilisateurCommunExists(int id)
    {
        return _context.Utilisateurs.Any(m => m.Id == id);
    }

    // DELETE: api/UtilisateurCommunApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUtilisateurCommun(int id)
    {
        var user = await _context.Set<UtilisateurCommun>().FindAsync(id);
        if (user == null)
            return NotFound();

        _context.Set<UtilisateurCommun>().Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}