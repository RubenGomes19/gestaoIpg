using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gestaoIpg.Models;
using Microsoft.AspNetCore.Authorization;

namespace gestaoIpg.Controllers
{
    public class TarefaController : Controller
    {
        private readonly gestaoIpgDbContext _context;

        private const int NUMBER_OF_PRODUCTS_PER_PAGE = 3;
        private const int NUMBER_OF_PAGES_BEFORE_AND_AFTER = 2;

        public TarefaController(gestaoIpgDbContext context)
        {
            _context = context;
        }

        // GET: Tarefa
        public IActionResult Index(int page = 1, string sortOrder = null, string searchString = null)
        {

            decimal numberProducts = _context.Tarefa.Count();
            var _contextTarefa = _context.Tarefa.Include(f => f.Funcionario);

            TarefaViewModel vm = new TarefaViewModel
            {

                Tarefas = _contextTarefa
                .Take((int)numberProducts),
                CurrentPage=page,
                TotalPages = (int)Math.Ceiling(numberProducts / NUMBER_OF_PRODUCTS_PER_PAGE),
                FirstPageShow = Math.Max(1, page - NUMBER_OF_PAGES_BEFORE_AND_AFTER),
            };
            
            if (!String.IsNullOrEmpty(searchString) )
            {
                //vm.CurrentSearchString = searchString;
                vm.Tarefas = vm.Tarefas.Where(p => p.DescricaoTarefa.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));

            }

            //ViewBag.NomeSortParm = sortOrder == "Descricao" ? "Nome_Desc" : "Descricao";

            switch (sortOrder)
            {
                case "Descricao":
                    vm.Tarefas = vm.Tarefas.OrderByDescending(p => p.DescricaoTarefa);
                    vm.CurrentSortOrder = "Descricao";
                    break;

                default:
                    vm.Tarefas = vm.Tarefas.OrderBy(p => p.DescricaoTarefa); // ascending by default
                    vm.CurrentSortOrder = "Descricao";
                    break;

            }
            vm.TotalPages = (int)Math.Ceiling((decimal)vm.Tarefas.Count() / NUMBER_OF_PRODUCTS_PER_PAGE);
            vm.Tarefas = vm.Tarefas.Skip((page - 1) * NUMBER_OF_PRODUCTS_PER_PAGE);
            vm.Tarefas = vm.Tarefas.Take(NUMBER_OF_PRODUCTS_PER_PAGE);
            vm.LastPageShow = Math.Min(vm.TotalPages, page + NUMBER_OF_PAGES_BEFORE_AND_AFTER);
            vm.FirstPage = 1;
            vm.LastPage = vm.TotalPages;
            
            return View(vm);
        }

        // GET: Tarefa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa
                .Include(f => f.Funcionario )
                .FirstOrDefaultAsync(m => m.TarefaId == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // GET: Tarefa/Create
        
        public IActionResult Create()
        {

            ViewData["TarefaId"] = new SelectList(_context.Tarefa, "TarefaId", "DescricaoTarefa");
            return View();
        }

        // POST: Tarefa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TarefaId,DescricaoTarefa,FuncionarioId")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "Nome", tarefa.FuncionarioId);
            return View(tarefa);
        }

        // GET: Tarefa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "Nome", tarefa.FuncionarioId);

            return View(tarefa);
        }

        // POST: Tarefa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TarefaId,DescricaoTarefa")] Tarefa tarefa)
        {
            if (id != tarefa.TarefaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarefaExists(tarefa.TarefaId))
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

            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "Nome", tarefa.FuncionarioId);

            return View(tarefa);
        }

        // GET: Tarefa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa
                .Include(f => f.Funcionario)
                .FirstOrDefaultAsync(m => m.TarefaId == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // POST: Tarefa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarefa = await _context.Tarefa.FindAsync(id);
            _context.Tarefa.Remove(tarefa);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return View("Sucesso");
        }

        private bool TarefaExists(int id)
        {
            return _context.Tarefa.Any(e => e.TarefaId == id);
        }
    }
}
