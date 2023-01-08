using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Data;
namespace Controllers;

public class FactureController : Controller // not ControllerBase!
{

    private readonly CalabarContext context;

    public FactureController(CalabarContext context)
    {
        this.context = context;
    }



    public async Task<IActionResult> Index()
    {

        var Facture = await context.Set<Facture>()
        .Include(f => f.Association)
        .Include(f => f.Fournisseur)
        .ToListAsync();
        return View(Facture);

    }

    public IActionResult Create()
    {
        var model = new FactureViewModel(context);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Numero,PrixHT,PrixTTC,Date,FournisseurId,AssociationId")] FactureViewModel model)
    {
        Facture facture = new Facture(model);
        var fournisseur = context.Fournisseurs.Find(facture.FournisseurId);
        var asso = context.Associations.Find(facture.AssociationId);

        facture.Fournisseur = fournisseur!;
        facture.Association = asso!;

        context.Add(facture);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var facture = await context.Set<Facture>().FindAsync(id);
        var model = new FactureViewModel(context, (Facture)facture!);
        if (facture == null)
        {
            return NotFound();
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,PrixHT,PrixTTC,Date,FournisseurId,AssociationId")] FactureViewModel model)
    {
        Facture facture = new Facture(model);
        var fournisseur = context.Fournisseurs.Find(facture.FournisseurId);
        var asso = context.Associations.Find(facture.AssociationId);

        facture.Fournisseur = fournisseur!;
        facture.Association = asso!;

        if (id != facture.Id)
        {
            return NotFound();
        }

        ModelState.Remove("A");
        ModelState.Remove("F");

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(facture);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FactureExiste(facture.Id))
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

    private bool FactureExiste(int id)
    {
        return context.Set<Facture>().Any(e => e.Id == id);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var facture = await context.Set<Facture>()
            .FirstOrDefaultAsync(m => m.Id == id);
        if (facture == null)
        {
            return NotFound();
        }

        return View(facture);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var Facture = await context.Set<Facture>().FindAsync(id);
        context.Set<Facture>().Remove(Facture!);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }



}





