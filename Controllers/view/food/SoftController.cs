using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Data;
namespace Controllers;

public class SoftController : Controller
{

    private readonly CalabarContext context;

    public SoftController(CalabarContext context)
    {
        this.context = context;
    }

    public async Task<IActionResult> Index()
    {

        var soft = await context.Set<Soft>()
           .Include(s => s.Association)
           .ToListAsync();

        return View(soft);

    }

    public IActionResult Create()
    {
        var model = new SoftViewModel(context);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nom,TailleBouteillelitre,Quantite,PrixAchat,PrixVente,AssociationId")] SoftViewModel model)
    {
        Soft soft = new Soft(model);
        var association = context.Associations.Find(soft.AssociationId);

        soft.Association = association!;

        context.Add(soft);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var soft = await context.Set<Soft>().FindAsync(id);
        var model = new SoftViewModel(context, (Soft)soft!);
        if (soft == null)
        {
            return NotFound();
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,TailleBouteillelitre,Quantite,PrixAchat,PrixVente,AssociationId")] SoftViewModel model)
    {
        Soft soft = new Soft(model);
        if (id != soft.Id)
        {
            return NotFound();
        }

        ModelState.Remove("A");
        if (ModelState.IsValid)
        {
            try
            {
                var association = context.Associations.Find(soft.AssociationId);

                soft.Association = association!;

                context.Update(soft);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SoftExiste(soft.Id))
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
        return View(model);
    }

    private bool SoftExiste(int id)
    {
        return context.Set<Soft>().Any(e => e.Id == id);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var soft = await context.Set<Soft>()
            .FirstOrDefaultAsync(m => m.Id == id);
        if (soft == null)
        {
            return NotFound();
        }

        return View(soft);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var soft = await context.Set<Soft>().FindAsync(id);
        context.Set<Soft>().Remove(soft!);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }



}





