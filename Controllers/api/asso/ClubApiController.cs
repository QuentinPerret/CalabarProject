using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClubApiController : ControllerBase
{
    private readonly CalabarContext _context;
    public ClubApiController(CalabarContext context)
    {
        _context = context;
    }

    // GET: api/ClubApi
    public async Task<ActionResult<IEnumerable<Club>>> GetClub()
    {
        return await _context.Set<Club>()
        .Include(c => c.MembreAssociationClubs)
        .Include(c => c.Factures)
        // .Include(c => c.President)
        .Include(c => c.Bureau)
        .ToListAsync();
    }

    // GET: api/ClubApi/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Club>> GetClub(int id)
    {
        var club = await _context.Set<Club>()
        .Where(a => a.Id == id)
        .Include(c => c.MembreAssociationClubs)
        .Include(c => c.Factures)
        // .Include(c => c.President)
        .Include(c => c.Bureau)
        .FirstOrDefaultAsync();
        if (club == null)
            return NotFound();
        return club;
    }


    // ...
    // POST: api/ClubApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Club>> PostClub(ClubDTO clubDTO)
    {
        var club = new Club(clubDTO);
        var bureau = _context.Set<Bureau>().Find(clubDTO.BureauId);

        club.Bureau = bureau!;

        _context.Set<Club>().Add(club);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetClub), new { id = club.Id }, club);
    }

    // ...
    // PUT: api/ClubApi/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutClub(int id, ClubDTO clubDTO)
    {
        if (id != clubDTO.Id)
            return BadRequest();

        Club club = new Club(clubDTO);

        _context.Entry(club).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClubExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // Returns true if a Club with specified id already exists
    private bool ClubExists(int id)
    {
        return _context.Set<Club>().Any(m => m is Club && m.Id == id);
    }

    // ...
    // DELETE: api/ClubApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClub(int id)
    {
        var club = await _context.Set<Club>().FindAsync(id);
        if (club == null)
            return NotFound();

        _context.Set<Club>().Remove(club);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    // ...
}