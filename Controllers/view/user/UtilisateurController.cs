using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Data;
namespace Controllers;

public class UtilisateurController : Controller // not ControllerBase!
{

    private readonly CalabarContext _context;

    public UtilisateurController(CalabarContext context)
    {
        _context = context;
    }
    // GET: /Hello/
    public async Task<IActionResult> Index()
    {
        var user = await _context.Utilisateurs.ToListAsync();
        return View(user);
    }
}





