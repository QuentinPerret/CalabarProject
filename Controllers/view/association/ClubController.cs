using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Data;
namespace Controllers;

public class ClubController : Controller // not ControllerBase!
{

    private readonly CalabarContext context;

    public ClubController(CalabarContext context)
    {
        this.context = context;
    }



    public async Task<IActionResult> Index()
    {

        var club = await context.Set<Club>()
           .Include(s => s.Bureau)
           .ToListAsync();
        return View(club);

    }

    public IActionResult Create()
    {
        var model = new ClubViewModel(context);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nom,Email,BureauId")] ClubViewModel model)
    {
        Club club = new Club(model);
        var bureau = context.Set<Bureau>().Find(club.BureauId);

        club.Bureau = bureau!;

        context.Add(club);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var club = await context.Set<Club>().FindAsync(id);
        var model = new ClubViewModel(context, (Club)club!);
        if (club == null)
        {
            return NotFound();
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Email,BureauId")] ClubViewModel model)
    {
        Club club = new Club(model);
        if (id != club.Id)
        {
            return NotFound();
        }

        ModelState.Remove("B");
        if (ModelState.IsValid)
        {
            try
            {
                var bureau = context.Set<Bureau>().Find(club.BureauId);

                club.Bureau = bureau!;

                context.Update(club);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClubExiste(club.Id))
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

    private bool ClubExiste(int id)
    {
        return context.Set<Club>().Any(e => e.Id == id);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var club = await context.Set<Club>()
            .FirstOrDefaultAsync(m => m.Id == id);
        if (club == null)
        {
            return NotFound();
        }

        return View(club);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var club = await context.Set<Club>().FindAsync(id);
        context.Set<Club>().Remove(club!);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }



}





