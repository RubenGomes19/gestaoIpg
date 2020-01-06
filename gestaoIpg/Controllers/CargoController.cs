using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gestaoIpg.Models;

namespace gestaoIpg.Controllers
{
    public class CargoController : Controller
    {
        private readonly gestaoIpgDbContext _context;

        //private const int NUMBER_OF_PRODUCTS_PER_PAGE = 3;
        
        //private const int NUMBER_OF_PAGES_BEFORE_AND_AFTER = 2;

        public CargoController(gestaoIpgDbContext context)
        {
            _context = context;
        }

        // GET: Cargo
        /*public IActionResult Index(int page = 1)
        {
            decimal numberProducts = _context.Cargo.Count();
            CargoViewModel vm = new CargoViewModel
            {
                Cargo = _context.Cargo
                .Skip((page - 1) * NUMBER_OF_PRODUCTS_PER_PAGE)
                .Take(NUMBER_OF_PRODUCTS_PER_PAGE),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(numberProducts / NUMBER_OF_PRODUCTS_PER_PAGE),
                FirstPageShow = Math.Max(1, page - NUMBER_OF_PAGES_BEFORE_AND_AFTER),
            };
            vm.LastPageShow = Math.Min(vm.TotalPages, page + NUMBER_OF_PAGES_BEFORE_AND_AFTER);
            return View(vm);
        }*/

        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty (sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var cargo = from s in _context.Cargo
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                cargo = cargo.Where(s => s.NomeCargo.Contains(searchString));
                
            }

            switch (sortOrder)
            {
                case "name_desc":
                    cargo = cargo.OrderByDescending(s => s.NomeCargo);    
                    break;
                default:
                    cargo = cargo.OrderBy(s => s.NomeCargo);
                    break;

            }

            int pageSize = 3;
            return View(await CargoViewModel<Cargo>.CreateAsync(cargo.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Cargo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargo
                .FirstOrDefaultAsync(m => m.CargoId == id);
            if (cargo == null)
            {
                return NotFound();
            }

            return View(cargo);
        }

        // GET: Cargo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cargo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CargoId,NomeCargo")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cargo);
                await _context.SaveChangesAsync();
                return View("Sucesso");
                //return RedirectToAction(nameof(Index));
            }
            return View(cargo);
        }

        // GET: Cargo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargo.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }
            return View(cargo);
        }

        // POST: Cargo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CargoId,NomeCargo")] Cargo cargo)
        {
            if (id != cargo.CargoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cargo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CargoExists(cargo.CargoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View("Sucesso");
                //return RedirectToAction(nameof(Index));
            }
            return View(cargo);
        }

        // GET: Cargo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargo
                .FirstOrDefaultAsync(m => m.CargoId == id);
            if (cargo == null)
            {
                return NotFound();
            }

            return View(cargo);
        }

        // POST: Cargo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cargo = await _context.Cargo.FindAsync(id);
            _context.Cargo.Remove(cargo);
            await _context.SaveChangesAsync();
            return View("Sucesso");
            //return RedirectToAction(nameof(Index));
        }

        private bool CargoExists(int id)
        {
            return _context.Cargo.Any(e => e.CargoId == id);
        }
    }
}
