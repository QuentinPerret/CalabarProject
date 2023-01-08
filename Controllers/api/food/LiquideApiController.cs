using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class LiquideApiController : ControllerBase{
    private readonly CalabarContext _context;
    public LiquideApiController(CalabarContext context){
        _context = context;
    }
    
    // GET: api/LiquideApi
    public async Task<ActionResult<IEnumerable<Liquide>>> GetLiquides()
    {
        return await _context.Set<Liquide>()
        .Include(c => c.Association)
        .ToListAsync();
    
    }

    // GET: api/LiquideApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Liquide>> GetLiquide(int id)
    {
        var liquide = await _context.Set<Liquide>()
        .Include(c => c.Association)
        .Where(c => c.Id == id)
        .FirstOrDefaultAsync();
        if (liquide == null)
            return NotFound();
        return liquide;
    }

    // DELETE: api/LiquideApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLiquide(int id)
    {
        var liquide = await _context.Set<Liquide>().FindAsync(id);
        if (liquide == null)
            return NotFound();

        _context.Set<Liquide>().Remove(liquide);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}