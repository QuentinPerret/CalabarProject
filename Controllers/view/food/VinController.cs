using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Data;
namespace Controllers;

public class VinController : Controller
{

    private readonly CalabarContext context;

    public VinController(CalabarContext context)
    {
        this.context = context;
    }



    public async Task<IActionResult> Index()
    {

        var vin = await context.Set<Vin>()
           .Include(s => s.Association)
           .Include(c => c.Chateau)
           .ToListAsync();

        return View(vin);

    }

    public IActionResult Create()
    {
        var model = new VinViewModel(context);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nom,ChateauId,TypeDeVin,Millesime,TailleBouteillelitre,Quantite,PrixAchat,PrixVente,AssociationId,")] VinViewModel model)
    {
        Vin vin = new Vin(model);
        var chateau = context.Set<Chateau>().Find(vin.ChateauId);
        var association = context.Set<Association>().Find(vin.AssociationId);

        vin.Chateau = chateau!;
        vin.Association = association!;

        context.Add(vin);
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

        var vin = await context.Set<Vin>().FindAsync(id);
        var model = new VinViewModel(context, (Vin)vin!);
        if (vin == null)
        {
            return NotFound();
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,ChateauId,TypeDeVin,Millesime,TailleBouteillelitre,Quantite,PrixAchat,PrixVente,AssociationId")] VinViewModel model)
    {
        Vin vin = new Vin(model);

        var chateau = context.Set<Chateau>().Find(vin.ChateauId);
        var association = context.Set<Association>().Find(vin.AssociationId);

        vin.Chateau = chateau!;
        vin.Association = association!;

        if (id != vin.Id)
        {
            return NotFound();
        }

        ModelState.Remove("A");
        ModelState.Remove("C");

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(vin);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vinExiste(vin.Id))
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

    private bool vinExiste(int id)
    {
        return context.Set<Vin>().Any(e => e.Id == id);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vin = await context.Set<Vin>()
            .FirstOrDefaultAsync(m => m.Id == id);
        if (vin == null)
        {
            return NotFound();
        }

        return View(vin);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var vin = await context.Set<Vin>().FindAsync(id);
        context.Set<Vin>().Remove(vin!);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }



}



