using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InfnetMVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace InfnetMVC.Controllers
{
    [Authorize]
    public class FuncionariosController : Controller
    {
        private readonly InfnetDbContexto _context;

        public FuncionariosController(InfnetDbContexto context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index(string searchString)
        {
            var funcionarios = from f in _context.Funcionario.Include(f => f.Departamento)
                               select f;

            if (!string.IsNullOrEmpty(searchString))
            {
                funcionarios = funcionarios.Where(f => f.Nome.Contains(searchString));
            }

            return View(await funcionarios.ToListAsync());
        }
        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Departamentos = await _context.Departamento.ToListAsync();
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Endereco,Telefone,Email,DataNascimento,DepartamentoId")] Funcionario funcionario)
        {
            if (funcionario != null)
            {
                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Se chegou aqui, significa que houve erros de validação, então recarregamos a lista de departamentos e retornamos para a view de criação com os erros
            ViewBag.Departamentos = await _context.Departamento.ToListAsync();
            return View(funcionario);
        }
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
            ViewBag.Departamentos = await _context.Departamento.ToListAsync();
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Endereco,Telefone,Email,DataNascimento,DepartamentoId")] Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }

            // Remova a verificação ModelState.IsValid
            // if (ModelState.IsValid)
            // {
            try
            {
                _context.Update(funcionario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(funcionario.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            ViewBag.Departamentos = await _context.Departamento.ToListAsync();
            return View(funcionario);
        }
        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .FirstOrDefaultAsync(m => m.Id == id);
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
            if (funcionario != null)
            {
                _context.Funcionario.Remove(funcionario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionario.Any(e => e.Id == id);
        }


    }
}