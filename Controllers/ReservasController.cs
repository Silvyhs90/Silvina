using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tp5Fi.Data;
using tpc5FI.Models;

namespace tp5Fi.Models
{
    public class ReservasController : Controller
    {       
        private readonly MyContext _context;

        public ReservasController(MyContext context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reserva.ToListAsync());
        }

        public IActionResult Reservar(int id)
        {
            int idAlojamiento = id;
            int idUsuario = 0;
            if (idAlojamiento > 0)
            {
                List<KeyValuePair<string, string>> cookieslist = ControllerContext.HttpContext.Request.Cookies.ToList();
                foreach (KeyValuePair<string, string> campos in cookieslist)
                {
                    if (campos.Key.Equals("myUsuario"))
                    {
                        idUsuario = Convert.ToInt32(campos.Value);
                    }
                }
                //int idUsuario =Convert.ToInt32( HttpContext.Request.HttpContext.Items["key"]); //agarrar la "cookie"
                /*fecha desde + fecha despues
                 precioxdia * cantidad de personas * dias Hotel
                precioxdia * dias  Cabaña
                  
                 */
                DateTime myFechaDesdePedida = Convert.ToDateTime("01/01/0001 0:00:00");
                DateTime myFechaHastaPedida = Convert.ToDateTime("01/01/0001 0:00:00");
                int cantidadDePersonas = 0;
                if (idUsuario > 0)
                {
                    var alojamiento = _context.Alojamiento.First(a => a.codigo == idAlojamiento);
                    Reserva reserva = null;

                    if (alojamiento.tipo.ToLower() == "hotel")
                    {

                        foreach (KeyValuePair<string, string> campos in cookieslist)
                        {
                            if (campos.Key.Equals("myCantidadDePersonas"))
                            {
                                cantidadDePersonas = Convert.ToInt32(campos.Value);
                            }
                        }
                        foreach (KeyValuePair<string, string> campos in cookieslist)
                        {
                            if (campos.Key.Equals("myfechaDesde"))
                            {
                                myFechaDesdePedida = Convert.ToDateTime(campos.Value);
                            }
                        }
                        foreach (KeyValuePair<string, string> campos in cookieslist)
                        {
                            if (campos.Key.Equals("myfechaHasta"))
                            {
                                myFechaHastaPedida = Convert.ToDateTime(campos.Value);
                            }
                        }
                        int dias = myFechaHastaPedida.Day - myFechaDesdePedida.Day;
                        dias++;
                        double precio = alojamiento.precioxDia * dias * (cantidadDePersonas * alojamiento.precioxPersona) ;

                        System.Diagnostics.Debug.WriteLine("mi nombree ciudad " + alojamiento.ciudad);
                        reserva = new Reserva(myFechaDesdePedida, myFechaHastaPedida, idAlojamiento, idUsuario, Convert.ToSingle(precio), alojamiento.ciudad);
                        return RedirectToAction("DetailsReserva", reserva);
                    }
                    else if (alojamiento.tipo.ToLower() == "cabaña")
                    {
                        System.Diagnostics.Debug.WriteLine("mi cabaña ciudad " + alojamiento.ciudad);
                        foreach (KeyValuePair<string, string> campos in cookieslist)
                        {
                            if (campos.Key.Equals("myfechaDesde"))
                            {
                                myFechaDesdePedida = Convert.ToDateTime(campos.Value);
                            }
                        }
                        foreach (KeyValuePair<string, string> campos in cookieslist)
                        {
                            if (campos.Key.Equals("myfechaHasta"))
                            {
                                myFechaHastaPedida = Convert.ToDateTime(campos.Value);
                            }
                        }

                        int dias = myFechaHastaPedida.Day - myFechaDesdePedida.Day;
                        double precio = alojamiento.precioxDia * dias; 
                        reserva = new Reserva(myFechaDesdePedida, myFechaHastaPedida, idAlojamiento, idUsuario, Convert.ToSingle(precio), alojamiento.ciudad);

                        return RedirectToAction("DetailsReserva", reserva);
                    }

                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }

            var reserva = await _context.Reserva
                .FirstOrDefaultAsync(m => m.id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        public  IActionResult DetailsReserva(Reserva reserva)
        {           
            return View(reserva);
        }

        public IActionResult DetailsReservaUsuario()
        {
            List<KeyValuePair<string, string>> cookieslist = ControllerContext.HttpContext.Request.Cookies.ToList();
            int myUsuario = 0;
            foreach (KeyValuePair<string, string> campos in cookieslist)
            {
                if (campos.Key.Equals("myUsuario"))
                {
                    myUsuario = Convert.ToInt32(campos.Value);
                }
            }

            List<Reserva> reserva = _context.Reserva.Where(m => m.codigoPersona == myUsuario).ToList();
                
            if (reserva == null)
            {
                return View();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FDesde,FHasta,codigoAlojamiento,codigoPersona,precio,nombreAlojamiento")] Reserva reserva)
        {
            System.Diagnostics.Debug.WriteLine("fDesde "+ reserva.FDesde+ "fhasta "+ reserva.FHasta + "codigo alojamiento " + reserva.codigoAlojamiento+ "codigo persona " + reserva.codigoPersona+ "precio "+ reserva.precio+ "nombre alojamiento" + reserva.nombreAlojamiento);
            if (ModelState.IsValid)
            {
                
                
                Alojamiento aloj = await _context.Alojamiento.FirstAsync(a => a.codigo == reserva.codigoAlojamiento);
                if (aloj.tipo.ToLower() == "hotel")
                {
                    List<KeyValuePair<string, string>> cookieslist = ControllerContext.HttpContext.Request.Cookies.ToList();                    
                    int cantidadDePersonas = 0;
                    foreach (KeyValuePair<string, string> campos in cookieslist)
                    {
                        if (campos.Key.Equals("myCantidadDePersonas"))
                        {
                            cantidadDePersonas = Convert.ToInt32(campos.Value);
                        }
                    }

                    if (cantidadDePersonas > 0)
                    {
                        aloj.cantPersonas = aloj.cantPersonas - cantidadDePersonas;
                        _context.Update(aloj);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DetailsReservaUsuario));
            }
            return NotFound();
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,FDesde,FHasta,codigoAlojamiento,codigoPersona,precio")] Reserva reserva)
        {
            if (id != reserva.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.id))
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
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .FirstOrDefaultAsync(m => m.id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            _context.Reserva.Remove(reserva);
            await _context.SaveChangesAsync();
            List<KeyValuePair<string, string>> cookieslist = ControllerContext.HttpContext.Request.Cookies.ToList();
            int idUsuario = 0;
            foreach (KeyValuePair<string, string> campos in cookieslist)
            {
                if (campos.Key.Equals("myUsuario"))
                {
                    idUsuario = Convert.ToInt32(campos.Value);
                }
            }
            Usuario Myusuario = await _context.Usuario.FirstAsync(m => m.usuarioID == idUsuario);
            if(Myusuario.esAdmin == true)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("DetailsUsuario","Usuario", new { id = Myusuario.usuarioID });
            }
            
        }

        private bool ReservaExists(int id)
        {
            return _context.Reserva.Any(e => e.id == id);
        }
    }
}
