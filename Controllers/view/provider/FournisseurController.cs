using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Data;
namespace Controllers;

public class FournisseurController : Controller // not ControllerBase!
{

    private readonly CalabarContext _context;

    public FournisseurController(CalabarContext context)
    {
        _context = context;
    }
    // GET: /Hello/
    public async Task<IActionResult> Index()
    {
        var user = await _context.Set<Fournisseur>().ToListAsync();
        return View(user);
    }
}





