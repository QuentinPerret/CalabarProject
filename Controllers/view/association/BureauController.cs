using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Data;
namespace Controllers;

public class BureauController : Controller // not ControllerBase!
{

    private readonly CalabarContext _context;

    public BureauController(CalabarContext context)
    {
        _context = context;
    }



    public async Task<IActionResult> Index()
    {

        var Bureau = await _context.Set<Bureau>()
           .ToListAsync();
        return View(Bureau);

    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nom,Email")] Bureau bureau)
    {

        _context.Add(bureau);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var Bureau = await _context.Set<Bureau>().FindAsync(id);
        if (Bureau == null)
        {
            return NotFound();
        }
        return View(Bureau);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Email")] Bureau bureau)
    {
        if (id != bureau.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(bureau);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BureauExiste(bureau.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(bureau);
    }

    private bool BureauExiste(int id)
    {
        return _context.Set<Bureau>().Any(e => e.Id == id);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var Bureau = await _context.Set<Bureau>()
            .FirstOrDefaultAsync(m => m.Id == id);
        if (Bureau == null)
        {
            return NotFound();
        }

        return View(Bureau);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var Bureau = await _context.Set<Bureau>().FindAsync(id);
        _context.Set<Bureau>().Remove(Bureau!);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }



}





