using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Data;
namespace Controllers;

public class BiereController : Controller // not ControllerBase!
{

    private readonly CalabarContext context;

    public BiereController(CalabarContext context)
    {
        this.context = context;
    }



    public async Task<IActionResult> Index()
    {

        var biere = await context.Set<Biere>()
           .Include(s => s.Association)
           .Include(b => b.Brasserie)
           .ToListAsync();

        return View(biere);

    }

    public IActionResult Create()
    {
        var model = new BiereViewModel(context);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nom,TypeDeBiere,Degre,BrasserieId,TailleBouteillelitre,Quantite,PrixAchat,PrixVente,AssociationId")] BiereViewModel model)
    {
        Biere biere = new Biere(model);
        var brasserie = context.Set<Brasserie>().Find(biere.BrasserieId);
        var association = context.Set<Association>().Find(biere.AssociationId);

        biere.Brasserie = brasserie!;
        biere.Association = association!;

        context.Add(biere);
        // Apply model validation rules
        await context.SaveChangesAsync();

        // Redirect to student details
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var biere = await context.Set<Biere>().FindAsync(id);
        var model = new BiereViewModel(context, (Biere)biere!);
        if (biere == null)
        {
            return NotFound();
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,TypeDeBiere,Degre,BrasserieId,TailleBouteillelitre,Quantite,PrixAchat,PrixVente,AssociationId")] BiereViewModel model)
    {
        Biere biere = new Biere(model);
        var brasserie = context.Set<Brasserie>().Find(biere.BrasserieId);
        var association = context.Set<Association>().Find(biere.AssociationId);

        biere.Brasserie = brasserie!;
        biere.Association = association!;

        if (id != biere.Id)
        {
            return NotFound();
        }

        ModelState.Remove("A");
        ModelState.Remove("Brasseries");

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(biere);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BiereExiste(biere.Id))
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

    private bool BiereExiste(int id)
    {
        return context.Set<Biere>().Any(e => e.Id == id);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var biere = await context.Set<Biere>()
            .FirstOrDefaultAsync(m => m.Id == id);
        if (biere == null)
        {
            return NotFound();
        }

        return View(biere);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var biere = await context.Set<Biere>().FindAsync(id);
        context.Set<Biere>().Remove(biere!);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }



}





