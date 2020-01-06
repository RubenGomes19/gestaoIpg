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
    public class DepartamentoController : Controller
    {
        private readonly gestaoIpgDbContext _context;

        private const int NUMBER_OF_PRODUCTS_PER_PAGE = 3;
        private const int NUMBER_OF_PAGES_BEFORE_AND_AFTER = 2;

        public DepartamentoController(gestaoIpgDbContext context)
        {
            _context = context;
        }

        // GET: Departamentos
        public IActionResult Index(int page = 1, string sortOrder = null, string searchString = null)
        {
            decimal numberProducts = _context.Departamento.Count();
            DepartamentoViewModel vm = new DepartamentoViewModel
            {
                Departamento = _context.Departamento
                
                .Take((int)numberProducts),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(numberProducts / NUMBER_OF_PRODUCTS_PER_PAGE),
                FirstPageShow = Math.Max(1, page - NUMBER_OF_PAGES_BEFORE_AND_AFTER),
            };
            


            if (!String.IsNullOrEmpty(searchString))
            {
                vm.Departamento = vm.Departamento.Where(p => p.Tipo.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));
 
            }
            
            switch (sortOrder)
            {
                case "Tipo":
                    vm.Departamento = vm.Departamento.OrderBy(p => p.Tipo); // ascending by default
                    vm.CurrentSortOrder = "Tipo";
                    break;
                default:
                    vm.Departamento = vm.Departamento.OrderByDescending(p => p.Tipo);
                    vm.CurrentSortOrder = "Tipo";
                    break;
            }


            vm.TotalPages = (int)Math.Ceiling((decimal)vm.Departamento.Count() / NUMBER_OF_PRODUCTS_PER_PAGE);
            vm.Departamento = vm.Departamento.Skip((page - 1) * NUMBER_OF_PRODUCTS_PER_PAGE);
            vm.Departamento = vm.Departamento.Take(NUMBER_OF_PRODUCTS_PER_PAGE);
            vm.LastPageShow = Math.Min(vm.TotalPages, page + NUMBER_OF_PAGES_BEFORE_AND_AFTER);
            vm.FirstPageShow = 1;
            vm.LastPageShow = vm.TotalPages;
            return View(vm);
        }

        // GET: Departamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamento
                .FirstOrDefaultAsync(m => m.DepartamentoId == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // GET: Departamentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartamentoId,Tipo")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departamento);
                await _context.SaveChangesAsync();
                return View("Sucesso");
                //return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        // GET: Departamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamento.FindAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        // POST: Departamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartamentoId,Tipo")] Departamento departamento)
        {
            if (id != departamento.DepartamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentoExists(departamento.DepartamentoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return View("Sucesso");
            }
            return View(departamento);
        }

        // GET: Departamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamento
                .FirstOrDefaultAsync(m => m.DepartamentoId == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // POST: Departamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departamento = await _context.Departamento.FindAsync(id);
            _context.Departamento.Remove(departamento);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return View("Sucesso");
        }

        private bool DepartamentoExists(int id)
        {
            return _context.Departamento.Any(e => e.DepartamentoId == id);
        }
    }
}
