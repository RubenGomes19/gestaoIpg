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
    public class FuncionarioController : Controller
    {
        private readonly gestaoIpgDbContext _context;

        private const int NUMBER_OF_PRODUCTS_PER_PAGE = 3;
        private const int NUMBER_OF_PAGES_BEFORE_AND_AFTER = 2;

        public FuncionarioController(gestaoIpgDbContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public IActionResult Index(int page = 1, string sortOrder = null, string searchString = null)
        {
            decimal numberProducts = _context.Funcionario.Count();
            var _contextFuncionario = _context.Funcionario.Include(f => f.Cargo).Include(f => f.Departamento);


            FuncionarioViewModel vm = new FuncionarioViewModel
            {
                
                Funcionarios = _contextFuncionario
                //.Skip((page - 1) * NUMBER_OF_PRODUCTS_PER_PAGE)
                .Take((int) numberProducts),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(numberProducts / NUMBER_OF_PRODUCTS_PER_PAGE),
                FirstPageShow = Math.Max(1, page - NUMBER_OF_PAGES_BEFORE_AND_AFTER),
            };

           

            if (!String.IsNullOrEmpty(searchString))
            {
                vm.CurrentSearchString = searchString;
                vm.Funcionarios = vm.Funcionarios.Where(p => p.Nome.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));

            }

            switch (sortOrder)
            {
                

                case "Nome_Desc":
                    vm.Funcionarios = vm.Funcionarios.OrderByDescending(p => p.Nome);
                    vm.CurrentSortOrder = "Nome_Desc";
                    break;

                default:
                    vm.Funcionarios = vm.Funcionarios.OrderBy(p => p.Nome); // ascending by default
                    vm.CurrentSortOrder = "Nome";
                    break;
            }
            ViewBag.NomeSortParm = sortOrder == "Nome" ? "Nome_Desc" : "Nome";
         
            
            vm.TotalPages = (int)Math.Ceiling((decimal)vm.Funcionarios.Count() / NUMBER_OF_PRODUCTS_PER_PAGE);
            vm.Funcionarios = vm.Funcionarios.Skip((page - 1) * NUMBER_OF_PRODUCTS_PER_PAGE);
            vm.Funcionarios = vm.Funcionarios.Take(NUMBER_OF_PRODUCTS_PER_PAGE);
            vm.LastPageShow = Math.Min(vm.TotalPages, page + NUMBER_OF_PAGES_BEFORE_AND_AFTER);
            vm.FirstPage = 1;
            vm.LastPage = vm.TotalPages;
            //return View(await gestaoTarefasIPGDbContext.ToListAsync());
            return View(vm);
        }
        

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .Include(f => f.Cargo)
                .Include(f => f.Departamento)
                .FirstOrDefaultAsync(m => m.FuncionarioId == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        [Authorize(Roles = "admin")]
        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            ViewData["CargoId"] = new SelectList(_context.Cargo, "CargoId", "NomeCargo");
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Tipo");
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FuncionarioId,Nome,Morada,Email,Telemovel, CargoId, DepartamentoId")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                return View("Sucesso");
                //return RedirectToAction(nameof(Index));
            }
            ViewData["CargoId"] = new SelectList(_context.Cargo, "CargoId", "NomeCargo", funcionario.CargoId);
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Tipo", funcionario.DepartamentoId);
            return View(funcionario);
        }

        [Authorize(Roles = "admin")]
        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            ViewData["CargoId"] = new SelectList(_context.Cargo, "CargoId", "NomeCargo", funcionario.CargoId);
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Tipo", funcionario.DepartamentoId);

            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FuncionarioId,Nome,Morada,Email,Telemovel,CargoId,DepartamentoId")] Funcionario funcionario)
        {
            if (id != funcionario.FuncionarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                /*try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.FuncionarioId))
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
            ViewData["CargoId"] = new SelectList(_context.Cargo, "CargoId", "NomeCargo", funcionario.CargoId);
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Tipo", funcionario.DepartamentoId);

            return View(funcionario);*/
                try
                {
                    // se isto for null é porque não encontrou nenhum registo na tabela Escola com o mesmo Nome
                    if (_context.Cargo.FirstOrDefault(m => m.NomeCargo == funcionario.Nome) == null)
                    {
                        _context.Update(funcionario);
                        
                        await _context.SaveChangesAsync();
                    }
                    else if (_context.Departamento.FirstOrDefault(m => m.Tipo == funcionario.Nome) == null)
                    {
                        _context.Update(funcionario);
                        
                        await _context.SaveChangesAsync();
                    }

                    else
                    {
                        ViewData["CargoId"] = new SelectList(_context.Cargo, "CargoId", "NomeCargo", funcionario.CargoId);
                        ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Tipo", funcionario.DepartamentoId);
                        ModelState.AddModelError("Nome", "Não é possível adicionar nomes repetidos.");
                        return View(funcionario);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.FuncionarioId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View("Sucesso");
            }
            else
            {
                return View("Erro");
            }
        }

        [Authorize(Roles = "admin")]
        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .Include(f => f.Cargo)
                .Include(f => f.Departamento)
                .FirstOrDefaultAsync(m => m.FuncionarioId == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionario.FindAsync(id);
            _context.Funcionario.Remove(funcionario);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return View("Sucesso");
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionario.Any(e => e.FuncionarioId == id);
        }
    }
}
