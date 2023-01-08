using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Models;
using Data;
namespace Controllers;

public class NourritureController : Controller
{

    private readonly CalabarContext context;

    public NourritureController(CalabarContext context)
    {
        this.context = context;
    }

    public async Task<IActionResult> IndexNourriture()
    {

        var nourriture = await context.Set<Nourriture>()
           .Include(s => s.Association)
           .ToListAsync();


        return View(nourriture);
    }

    public IActionResult CreateNourriture()
    {
        var model = new NourritureViewModel(context);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateNourriture([Bind("Id,Nom,Quantite,PrixAchat,PrixVente,AssociationId")] NourritureViewModel model)
    {
        Nourriture nourriture = new Nourriture(model);
        var association = context.Set<Association>().Find(nourriture.AssociationId);

        nourriture.Association = association!;

        context.Add(nourriture);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(IndexNourriture));
    }

    public async Task<IActionResult> EditNourriture(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var nourriture = await context.Set<Nourriture>().FindAsync(id);
        var model = new NourritureViewModel(context, (Nourriture)nourriture!);
        if (nourriture == null)
        {
            return NotFound();
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditNourriture(int id, [Bind("Id,Nom,Quantite,PrixAchat,PrixVente,AssociationId")] NourritureViewModel model)
    {
        Nourriture nourriture = new Nourriture(model);
        if (id != nourriture.Id)
        {
            return NotFound();
        }

        ModelState.Remove("A");
        if (ModelState.IsValid)
        {
            try
            {
                var association = context.Set<Association>().Find(nourriture.AssociationId);

                nourriture.Association = association!;

                context.Update(nourriture);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NourritureExiste(nourriture.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(IndexNourriture));
        }
        return View(model);
    }

    private bool NourritureExiste(int id)
    {
        return context.Set<Nourriture>().Any(e => e.Id == id);
    }

    public async Task<IActionResult> DeleteNourriture(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var nourriture = await context.Set<Nourriture>()
            .FirstOrDefaultAsync(m => m.Id == id);
        if (nourriture == null)
        {
            return NotFound();
        }

        return View(nourriture);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmedNourriture(int id)
    {
        var nourriture = await context.Set<Nourriture>().FindAsync(id);
        context.Set<Nourriture>().Remove(nourriture!);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(IndexNourriture));
    }



}





