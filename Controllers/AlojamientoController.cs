using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tp5Fi.Data;
using Newtonsoft.Json;
using tpc5FI.Models;

namespace tp5Fi.Models
{
    public class AlojamientoController : Controller
    {
        private readonly MyContext _context;

        public AlojamientoController(MyContext context)
        {
            _context = context;
        }

        // GET: Alojamiento
        public async Task<IActionResult> Index()
        {
            return View(await _context.Alojamiento.ToListAsync());
        }


        [HttpPost]
        public IActionResult filtrar([Bind("filtrar")] string filtrar, [Bind("mylista")] string mylista)
        {
            List<Alojamiento> mylistaJ = JsonConvert.DeserializeObject<List<Alojamiento>>(mylista);
            System.Diagnostics.Debug.WriteLine("hola ");
            System.Diagnostics.Debug.WriteLine(filtrar);
            //List<Alojamiento> mylistaJ = JsonSerializer.Deserialize(mylista, returnType, default);

            foreach (Alojamiento aloj in mylistaJ)
            {
                System.Diagnostics.Debug.WriteLine("my cidudad " + aloj.ciudad + "barrio " + aloj.barrio + "estrellas " + aloj.estrellas + "precio d " + aloj.precioxDia + "cant  " + aloj.cantPersonas);
            }

            List<Alojamiento> ListaOrdenada = null;
            if (filtrar == "estrellas")
            {
                System.Diagnostics.Debug.WriteLine("estrellas");
                ListaOrdenada = mylistaJ.OrderBy(a => a.estrellas).ToList();
            }
            else if (filtrar == "cantidadPersonas")
            {
                System.Diagnostics.Debug.WriteLine("cantPersonas");
                ListaOrdenada = mylistaJ.OrderBy(a => a.cantPersonas).ToList();
            }
            else if (filtrar == "precioDia")
            {
                ListaOrdenada = mylistaJ.OrderBy(a => a.precioxDia).ToList();
            }
            else if (filtrar == "precioPersona")
            {
                ListaOrdenada = mylistaJ.OrderBy(a => a.precioxPersona).ToList();
            }
            else if (filtrar == "maxPrecio")
            {
                List<KeyValuePair<string, string>> cookieslist = ControllerContext.HttpContext.Request.Cookies.ToList();
                DateTime myFechaDesdePedida = Convert.ToDateTime("01/01/0001 0:00:00");
                DateTime myFechaHastaPedida = Convert.ToDateTime("01/01/0001 0:00:00");
                int cantidadDePersonas = 0;
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


                ListaOrdenada = mylistaJ.OrderBy(a => (a.precioxDia * dias)).ToList();
            }


            if (ListaOrdenada != null)
            {
                return View("BuscadorAction", ListaOrdenada);
            }
            else
            {
                return NotFound();
                //return View("BuscadorAction", mylistaJ);
            }

        }



        // GET: Alojamiento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alojamiento = await _context.Alojamiento
                .FirstOrDefaultAsync(m => m.codigo == id);
            if (alojamiento == null)
            {
                return NotFound();
            }

            return View(alojamiento);
        }

        public IActionResult Buscador()
        {
            return View();
        }


        [HttpPost]     
        public IActionResult BuscadorAction(string tipo, string cantPersonasPedidas, string fechaDesde, string fechaHasta, string ciudad)
        {
            DateTime hoy = DateTime.Today;

            ControllerContext.HttpContext.Response.Cookies.Append("myCantidadDePersonas", cantPersonasPedidas);
            ControllerContext.HttpContext.Response.Cookies.Append("myfechaDesde", fechaDesde);
            ControllerContext.HttpContext.Response.Cookies.Append("myfechaHasta", fechaHasta);
            List<Alojamiento> MyLista = null;
            
            List<Reserva> MyListaReservas = null;

            if(!(Convert.ToDateTime(fechaDesde) >= hoy)) // que no acepte fecha anterior
            {
                return RedirectToAction(nameof(Buscador));
            }

            if (tipo == null)
            {
                MyLista = _context.Alojamiento.Where(a => ((a.barrio == ciudad || a.ciudad == ciudad) && (a.cantPersonas >= Convert.ToInt32(cantPersonasPedidas)))).ToList();
            }
            else if (tipo.ToLower() == "hotel" || tipo.ToLower() == "cabaña")
            {
                MyLista = _context.Alojamiento.Where(a => ((a.barrio == ciudad || a.ciudad == ciudad) && (a.tipo == tipo) && (a.cantPersonas >= Convert.ToInt32(cantPersonasPedidas)))).ToList();
            }
            else
            {
                return RedirectToAction(nameof(Buscador));
            }

             MyListaReservas = _context.Reserva.ToList();

           

            
                for (int i = 0; i < MyLista.Count; i++)
                {
                  Console.WriteLine("Mi Count for 1 = " + MyLista.Count + " mi i for 1 = " + i);
                System.Diagnostics.Debug.WriteLine("Mi Count for 1 = "+MyLista.Count+ " mi i for 1 = "+ i );
                    if (MyLista[i].tipo.ToLower() == "cabaña")
                    {
                    Console.WriteLine("Mi Count if = " + MyLista.Count + " mi if  = " + i);
                    System.Diagnostics.Debug.WriteLine("Mi Count if = " + MyLista.Count + " mi if  = " + i);

                    for (int e = 0; e < MyListaReservas.Count; e++)
                        {
                        Console.WriteLine("Mi Count for 2 = " + MyLista.Count + " mi i for 2 = " + i);
                        System.Diagnostics.Debug.WriteLine("Mi Count for 2 = " + MyLista.Count + " mi i for 2 = " + i);

                        System.Diagnostics.Debug.WriteLine(" e " + e);
                        if (Convert.ToInt32(MyListaReservas[e].codigoAlojamiento) == MyLista[i].codigo)
                        { 
                            System.Diagnostics.Debug.WriteLine(" if e 1 " + e);
                            if(((DateTime.Compare(Convert.ToDateTime(fechaDesde), MyListaReservas[e].FDesde) < 0) && (DateTime.Compare(Convert.ToDateTime(fechaHasta), MyListaReservas[e].FDesde) < 0)) || ((DateTime.Compare(Convert.ToDateTime(fechaDesde), MyListaReservas[e].FHasta) > 0) && (DateTime.Compare(Convert.ToDateTime(fechaHasta), MyListaReservas[e].FHasta) > 0)))
                            {
                                System.Diagnostics.Debug.WriteLine(" if e 2 " + e);
                            }
                            else
                            {
                                MyLista.RemoveAt(i);
                                e = MyListaReservas.Count + 1;
                            }
                        }
                       
                        }
                    }
                }
            



            return View(MyLista);
        }
        // GET: Alojamiento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alojamiento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("codigo,tipo,ciudad,barrio,estrellas,cantPersonas,tv,precioxDia,precioxPersona,habitacion,baño,ocupado")] Alojamiento alojamiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alojamiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alojamiento);
        }

        // GET: Alojamiento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alojamiento = await _context.Alojamiento.FindAsync(id);
            if (alojamiento == null)
            {
                return NotFound();
            }
            return View(alojamiento);
        }

        // POST: Alojamiento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("codigo,tipo,ciudad,barrio,estrellas,cantPersonas,tv,precioxDia,precioxPersona,habitacion,baño,ocupado")] Alojamiento alojamiento)
        {
            if (id != alojamiento.codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alojamiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlojamientoExists(alojamiento.codigo))
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
            return View(alojamiento);
        }

        // GET: Alojamiento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alojamiento = await _context.Alojamiento
                .FirstOrDefaultAsync(m => m.codigo == id);
            if (alojamiento == null)
            {
                return NotFound();
            }

            return View(alojamiento);
        }

        // POST: Alojamiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alojamiento = await _context.Alojamiento.FindAsync(id);
            _context.Alojamiento.Remove(alojamiento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlojamientoExists(int id)
        {
            return _context.Alojamiento.Any(e => e.codigo == id);
        }
    }
}
