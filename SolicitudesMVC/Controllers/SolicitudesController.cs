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
    public class SolicitudesController : Controller
    {
        private readonly BdUpcContext _context;

        public SolicitudesController(BdUpcContext context)
        {
            _context = context;
        }

        // GET: Solicitudes
        public async Task<IActionResult> Index()
        {
            var bdUpcContext = _context.Solicitudes.Include(s => s.IdAlumnoNavigation);
            return View(await bdUpcContext.ToListAsync());
        }

        // GET: Solicitudes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Solicitudes == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitudes.FirstOrDefaultAsync(m => m.IdSolicitud == id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return View(solicitud);
        }

        // GET: Solicitudes/Create
        public IActionResult Create()
        {
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "Nombres");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAlumno,FechaSolicitud,CodRegistrante,Carrera,Periodo")] Solicitud solicitud)
        {
            if (ModelState.IsValid)
            {
                solicitud.IdSolicitud = Guid.NewGuid();
                var periodo = string.Empty;
                var carrera = string.Empty;
                                
                #region DataDummy
                //periodo
                switch (solicitud.Periodo)
                {
                    case "1":
                        periodo = "2021-1";
                        break;
                    case "2":
                        periodo = "2021-2";
                        break;
                    case "3":
                        periodo = "2022-2";
                        break;
                    case "4":
                        periodo = "2022-2";
                        break;
                }
                //carrera
                switch (solicitud.Carrera)
                {
                    case "1":
                        carrera = "ADMINISTRACIÓN Y FINANZAS";
                        break;
                    case "2":
                        carrera = "ADMINISTRACIÓN Y MARKETING";
                        break;
                    case "3":
                        carrera = "ADMINISTRACIÓN Y NEGOCIOS INTERNACIONALES";
                        break;
                }
                #endregion
                //Validaciones
                var sol = await _context.Solicitudes.Where(x => x.IdAlumno == solicitud.IdAlumno).SingleOrDefaultAsync();
                
                if (sol != null && sol.Periodo == periodo)
                {
                    ViewData["Message"] = "No se pueden realizar dos solicitudes de matricula para el mismo periodo";  
                }
                //else if(solicitud.)
                else
                {
                    solicitud.Periodo = periodo;
                    solicitud.Carrera = carrera;
                    _context.Add(solicitud);     
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }              
            }
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "IdAlumno", solicitud.IdAlumno);
            return View(solicitud);
        }

        // GET: Solicitudes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Solicitudes == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitudes.FindAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "IdAlumno", solicitud.IdAlumno);
            return View(solicitud);
        }

        // POST: Solicitudes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdSolicitud,IdAlumno,FechaSolicitud,CodRegistrante,Carrera,Periodo")] Solicitud solicitud)
        {
            if (id != solicitud.IdSolicitud)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitud);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudExists(solicitud.IdSolicitud))
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
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "IdAlumno", solicitud.IdAlumno);
            return View(solicitud);
        }

        // GET: Solicitudes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Solicitudes == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitudes
                .Include(s => s.IdAlumnoNavigation)
                .FirstOrDefaultAsync(m => m.IdSolicitud == id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return View(solicitud);
        }

        // POST: Solicitudes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Solicitudes == null)
            {
                return Problem("Entity set 'BdUpcContext.Solicitudes'  is null.");
            }
            var solicitud = await _context.Solicitudes.FindAsync(id);
            if (solicitud != null)
            {
                _context.Solicitudes.Remove(solicitud);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudExists(Guid id)
        {
          return _context.Solicitudes.Any(e => e.IdSolicitud == id);
        }
    }
}
