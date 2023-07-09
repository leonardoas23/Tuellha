using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tuellha.Data;
using Tuellha.Models;

namespace Tuellha.Controllers
{
    public class PublicacaoModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PublicacaoModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index2()
        {
            return _context.Publicacoes != null ?
                        View(await _context.Publicacoes.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Publicacoes'  is null.");
        }
        [Authorize]
        // GET: PublicacaoModels
        public async Task<IActionResult> Index()
        {
              return _context.Publicacoes != null ? 
                          View(await _context.Publicacoes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Publicacoes'  is null.");
        }
        [Authorize]
        // GET: PublicacaoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Publicacoes == null)
            {
                return NotFound();
            }

            var publicacaoModel = await _context.Publicacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicacaoModel == null)
            {
                return NotFound();
            }

            return View(publicacaoModel);
        }
        [Authorize]
        // GET: PublicacaoModels/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: PublicacaoModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Conteudo,Data,Foto")] PublicacaoModel publicacaoModel)
        {
            if (ModelState.IsValid)
            {
                if (publicacaoModel.Foto != null && publicacaoModel.Foto.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        publicacaoModel.Foto.CopyToAsync(memoryStream);
                        publicacaoModel.FotoDB = memoryStream.ToArray();
                    }
                }
                _context.Add(publicacaoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publicacaoModel);
        }
        [Authorize]
        // GET: PublicacaoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Publicacoes == null)
            {
                return NotFound();
            }

            var publicacaoModel = await _context.Publicacoes.FindAsync(id);
            if (publicacaoModel == null)
            {
                return NotFound();
            }
            return View(publicacaoModel);
        }
        [Authorize]
        // POST: PublicacaoModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Conteudo,Data")] PublicacaoModel publicacaoModel)
        {
            if (id != publicacaoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publicacaoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicacaoModelExists(publicacaoModel.Id))
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
            return View(publicacaoModel);
        }
        [Authorize]
        // GET: PublicacaoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Publicacoes == null)
            {
                return NotFound();
            }

            var publicacaoModel = await _context.Publicacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicacaoModel == null)
            {
                return NotFound();
            }

            return View(publicacaoModel);
        }
        [Authorize]
        // POST: PublicacaoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Publicacoes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Publicacoes'  is null.");
            }
            var publicacaoModel = await _context.Publicacoes.FindAsync(id);
            if (publicacaoModel != null)
            {
                _context.Publicacoes.Remove(publicacaoModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        private bool PublicacaoModelExists(int id)
        {
          return (_context.Publicacoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
