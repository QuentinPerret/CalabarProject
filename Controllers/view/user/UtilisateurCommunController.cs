using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models;
using Data;
namespace Controllers;

public class UtilisateurCommunController : Controller // not ControllerBase!
{

    private readonly CalabarContext _context;

    public UtilisateurCommunController(CalabarContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _context.Set<UtilisateurCommun>().ToListAsync();
        return View(user);
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Email,Password,EstUnEtudiant")] UtilisateurCommun utilisateurCommun)
    {

        _context.Add(utilisateurCommun);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _context.Set<UtilisateurCommun>().FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Email,Password,EstUnEtudiant")] UtilisateurCommun user)
    {
        if (id != user.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurCommunExiste(user.Id))
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
        return View(user);
    }

    private bool UtilisateurCommunExiste(int id)
    {
        return _context.Set<UtilisateurCommun>().Any(e => e.Id == id);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _context.Set<UtilisateurCommun>()
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
        var user = await _context.Set<UtilisateurCommun>().FindAsync(id);
        _context.Set<UtilisateurCommun>().Remove(user!);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}





