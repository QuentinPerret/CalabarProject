using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Data;
namespace Controllers;

public class AssociationController : Controller // not ControllerBase!
{

    private readonly CalabarContext _context;

    public AssociationController(CalabarContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var associations = await _context.Associations
        .ToListAsync();

        return View(associations);

    }

}





