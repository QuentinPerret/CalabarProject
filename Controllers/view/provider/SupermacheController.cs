using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models;
using Data;
namespace Controllers;

public class SupermarcheController : Controller // not ControllerBase!
{

    private readonly CalabarContext _context;

    public SupermarcheController(CalabarContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _context.Set<Supermarche>().ToListAsync();
        return View(user);
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nom,Siret,Adresse,Email,Telephone")] Supermarche supermarche)
    {

        _context.Add(supermarche);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _context.Set<Supermarche>().FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Siret,Adresse,Email,Telephone")] Supermarche supermarche)
    {
        if (id != supermarche.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(supermarche);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupermarcheExiste(supermarche.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }
        return View(supermarche);
    }

    private bool SupermarcheExiste(int id)
    {
        return _context.Set<Supermarche>().Any(e => e.Id == id);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _context.Set<Supermarche>()
            .FirstOrDefaultAsync(m => m.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }



    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var user = await _context.Set<Supermarche>().FindAsync(id);
        _context.Set<Supermarche>().Remove(user!);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}





