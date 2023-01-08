using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class UtilisateurApiController : ControllerBase{
    private readonly CalabarContext _context;
    public UtilisateurApiController(CalabarContext context){
        _context = context;
    }
    
    // GET: api/UtilisateurApi
    public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
    {
        return await _context.Utilisateurs.ToListAsync();
    }

    // GET: api/UtilisateurApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Utilisateur>> GetUtilisateur(int id)
    {
        var user = await _context.Utilisateurs.FindAsync(id);
        if (user == null)
            return NotFound();
        return user;
    }

    // DELETE: api/UtilisateurApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUtilisateur(int id)
    {
        var user = await _context.Utilisateurs.FindAsync(id);
        if (user == null)
            return NotFound();

        _context.Utilisateurs.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}