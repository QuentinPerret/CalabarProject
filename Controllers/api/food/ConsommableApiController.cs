using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConsommableApiController : ControllerBase{
    private readonly CalabarContext _context;
    public ConsommableApiController(CalabarContext context){
        _context = context;
    }
    
    // GET: api/ConsommableApi
    public async Task<ActionResult<IEnumerable<Consommable>>> GetConsommables()
    {
        return await _context.Consommables
        .Include(c => c.Association)
        .ToListAsync();
    
    }

    // GET: api/ConsommableApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Consommable>> GetConsommable(int id)
    {
        var conso = await _context.Consommables
        .Include(c => c.Association)
        .Where(c => c.Id == id)
        .FirstOrDefaultAsync();
        if (conso == null)
            return NotFound();
        return conso;
    }

    // DELETE: api/ConsommableApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConsommable(int id)
    {
        var conso = await _context.Consommables.FindAsync(id);
        if (conso == null)
            return NotFound();

        _context.Consommables.Remove(conso);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}