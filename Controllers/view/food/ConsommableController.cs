using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Data;
namespace Controllers;

public class ConsommableController : Controller // not ControllerBase!
{

    private readonly CalabarContext _context;

    public ConsommableController(CalabarContext context)
    {
        _context = context;
    }
    // Consommable
    public async Task<IActionResult> Index()
    {
        var consommable = await _context.Consommables
        .Include(s => s.Association)
        .ToListAsync();

        return View(consommable);

    }



}





