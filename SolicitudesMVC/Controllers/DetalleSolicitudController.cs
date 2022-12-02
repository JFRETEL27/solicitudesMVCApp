using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SolicitudesMVC.Models;

namespace SolicitudesMVC.Controllers
{
    public class DetalleSolicitudController : Controller
    {
        private readonly BdUpcContext _context;

        public DetalleSolicitudController(BdUpcContext context)
        {
            _context = context;
        }

        // GET: DetalleSolicitud
        public async Task<IActionResult> Index()
        {
            var bdUpcContext = _context.DetalleSolicitudes.Include(d => d.IdCursoNavigation).Include(d => d.IdSolicitudNavigation);
            return View(await bdUpcContext.ToListAsync());
        }

        // GET: DetalleSolicitud/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.DetalleSolicitudes == null)
            {
                return NotFound();
            }

            var detalleSolicitud = await _context.DetalleSolicitudes
                .Include(d => d.IdCursoNavigation)
                .Include(d => d.IdSolicitudNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleSol == id);
            if (detalleSolicitud == null)
            {
                return NotFound();
            }

            return View(detalleSolicitud);
        }

        // GET: DetalleSolicitud/Create
        public IActionResult Create()
        {
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "IdCurso");
            ViewData["IdSolicitud"] = new SelectList(_context.Solicitudes, "IdSolicitud", "IdSolicitud");
            return View();
        }

        // POST: DetalleSolicitud/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalleSol,IdSolicitud,IdCurso,Profesor,Aula,Sede,Observación")] DetalleSolicitud detalleSolicitud)
        {
            if (ModelState.IsValid)
            {
                detalleSolicitud.IdDetalleSol = Guid.NewGuid();
                _context.Add(detalleSolicitud);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "IdCurso", detalleSolicitud.IdCurso);
            ViewData["IdSolicitud"] = new SelectList(_context.Solicitudes, "IdSolicitud", "IdSolicitud", detalleSolicitud.IdSolicitud);
            return View(detalleSolicitud);
        }

        // GET: DetalleSolicitud/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.DetalleSolicitudes == null)
            {
                return NotFound();
            }

            var detalleSolicitud = await _context.DetalleSolicitudes.FindAsync(id);
            if (detalleSolicitud == null)
            {
                return NotFound();
            }
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "IdCurso", detalleSolicitud.IdCurso);
            ViewData["IdSolicitud"] = new SelectList(_context.Solicitudes, "IdSolicitud", "IdSolicitud", detalleSolicitud.IdSolicitud);
            return View(detalleSolicitud);
        }

        // POST: DetalleSolicitud/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdDetalleSol,IdSolicitud,IdCurso,Profesor,Aula,Sede,Observación")] DetalleSolicitud detalleSolicitud)
        {
            if (id != detalleSolicitud.IdDetalleSol)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleSolicitud);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleSolicitudExists(detalleSolicitud.IdDetalleSol))
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
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "IdCurso", detalleSolicitud.IdCurso);
            ViewData["IdSolicitud"] = new SelectList(_context.Solicitudes, "IdSolicitud", "IdSolicitud", detalleSolicitud.IdSolicitud);
            return View(detalleSolicitud);
        }

        // GET: DetalleSolicitud/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.DetalleSolicitudes == null)
            {
                return NotFound();
            }

            var detalleSolicitud = await _context.DetalleSolicitudes
                .Include(d => d.IdCursoNavigation)
                .Include(d => d.IdSolicitudNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleSol == id);
            if (detalleSolicitud == null)
            {
                return NotFound();
            }

            return View(detalleSolicitud);
        }

        // POST: DetalleSolicitud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.DetalleSolicitudes == null)
            {
                return Problem("Entity set 'BdUpcContext.DetalleSolicitudes'  is null.");
            }
            var detalleSolicitud = await _context.DetalleSolicitudes.FindAsync(id);
            if (detalleSolicitud != null)
            {
                _context.DetalleSolicitudes.Remove(detalleSolicitud);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleSolicitudExists(Guid id)
        {
          return _context.DetalleSolicitudes.Any(e => e.IdDetalleSol == id);
        }
    }
}
