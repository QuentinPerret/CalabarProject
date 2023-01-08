using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Data;
namespace Controllers;

public class MembreAssociationClubController : Controller // not ControllerBase!
{

    private readonly CalabarContext _context;

    public MembreAssociationClubController(CalabarContext context)
    {
        _context = context;
    }
    // GET: /Hello/
    public async Task<IActionResult> Index()
    {
        var user = await _context.Set<MembreAssociationClub>().ToListAsync();
        return View(user);
    }
}

