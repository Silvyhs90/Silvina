using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tp5Fi.Data;
using tpc5FI.Models;
using System.Web;



namespace tp5Fi.Models
{
    public class UsuarioController : Controller
    {
        private readonly MyContext _context;
        
        public UsuarioController(MyContext context)
        {
            _context = context;           
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuario.ToListAsync());
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {   
            if (id == null)
            {
                int idUsuario = 0;
                List<KeyValuePair<string, string>> cookieslist = ControllerContext.HttpContext.Request.Cookies.ToList(); // guardo las cookies que tenga y lo trato como lista
                foreach (KeyValuePair<string, string> campos in cookieslist)  //lista clave/valor
                {

                    if (campos.Key.Equals("myUsuario"))  // si es igual al id del usuario convierte a int
                    {
                        idUsuario = Convert.ToInt32(campos.Value);
                    }
                }

                if (idUsuario > 0)
                {
                    Usuario Myusuario = await _context.Usuario.FirstAsync(m => m.usuarioID == idUsuario);
                    if (Myusuario.esAdmin == true)
                    {
                        var usuario1 = await _context.Usuario
                   .FirstOrDefaultAsync(m => m.usuarioID == id);
                        return View(usuario1);
                    }
                    return RedirectToAction("DetailsUsuario", new { id = idUsuario });
                }
                return NotFound();
                
            }


            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.usuarioID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        public async Task<IActionResult> DetailsUsuario(int? id)
        {
            if (id == null)
            {
                List<KeyValuePair<string, string>> cookieslist = ControllerContext.HttpContext.Request.Cookies.ToList();
                int idUsuario = 0;
                foreach (KeyValuePair<string, string> campos in cookieslist)
                {
                    if (campos.Key.Equals("myUsuario"))
                    {
                        idUsuario = Convert.ToInt32(campos.Value);
                    }
                }
                if (idUsuario > 0)
                {
                    var usuarioif = await _context.Usuario
                    .FirstOrDefaultAsync(m => m.usuarioID == idUsuario);
                    if (usuarioif == null)
                    {
                        return NotFound();
                    }

                    return View(usuarioif);
                }
                else
                {
                    return NotFound();
                }
            }


            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.usuarioID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }


        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }


        public IActionResult CreateUsuario()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("usuarioID,dni,nombre,mail,password,esAdmin,bloqueado")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
               
            }

            List<KeyValuePair<string, string>> cookieslist = ControllerContext.HttpContext.Request.Cookies.ToList();
            int idUsuario = 0;
            foreach (KeyValuePair<string, string> campos in cookieslist)
            {
                if (campos.Key.Equals("myUsuario"))
                {
                    idUsuario = Convert.ToInt32(campos.Value);
                }
            }

            if(idUsuario > 0)
            {
                Usuario Myusuario = await _context.Usuario.FirstAsync(m => m.usuarioID == idUsuario);
                if (Myusuario.esAdmin == true)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Login", "Usuario");
                }

            }

            return RedirectToAction("Login", "Usuario");

        }

        public IActionResult Login()
        {
            ControllerContext.HttpContext.Response.Cookies.Delete("myUsuario");
            return View();
        }


        public IActionResult Logout()
        {
            return RedirectToAction(nameof(Login));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("usuarioID,dni,nombre,mail,password,esAdmin,bloqueado")] Usuario usuario)
        {

            if (UsuarioExistsNamePass(usuario.dni, usuario.password))
            {
                int Id = UsuarioAutetication(usuario.dni, usuario.password);
                Usuario Myusuario = _context.Usuario.First(m => m.usuarioID == Id);

                if (Id != 0)
                {
                    if (Myusuario.bloqueado != true)
                    {
                        if (Myusuario.esAdmin == true)
                        {
                            ControllerContext.HttpContext.Response.Cookies.Append("myUsuario", Id.ToString());

                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ControllerContext.HttpContext.Response.Cookies.Append("myUsuario", Id.ToString());
                            return RedirectToAction("DetailsUsuario", new { id = Id });
                        }
                    }
                }
                else
                {
                    /*Myusuario.cant_intentos++;
                    if(Myusuario.cant_intentos >= 3)
                    {
                        Myusuario.bloqueado = true;

                    }

                    update(__)*/
                    return RedirectToAction(nameof(Login));
                }
            }
            return RedirectToAction(nameof(Login));

        }


        // GET: Usuario/Edit/5
        /* public async Task<IActionResult> Edit(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             Usuario usuario = await _context.Usuario.FindAsync(id);
             if (usuario == null)
             {
                 return NotFound();
             }
             if (usuario.esAdmin == true)
             {
                 return View(usuario);
             }
                 return RedirectToAction("EditUsuario", new { id = usuario.usuarioID });
         }*/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Usuario usuario = await _context.Usuario.FirstAsync(m => m.usuarioID == id);
            if (usuario == null)
            {
                return NotFound();
            }
            int idUsuario = 0;
            List<KeyValuePair<string, string>> cookieslist = ControllerContext.HttpContext.Request.Cookies.ToList();
            foreach (KeyValuePair<string, string> campos in cookieslist)
            {

                if (campos.Key.Equals("myUsuario"))
                {
                    idUsuario = Convert.ToInt32(campos.Value);
                }
            }

            if (idUsuario > 0)
            {
                Usuario Myusuario = await _context.Usuario.FirstAsync(m => m.usuarioID == idUsuario);
                if (Myusuario.esAdmin == true)
                {
                    return View(usuario);
                }
                return RedirectToAction("EditUsuario", new { id = usuario.usuarioID });
            }
            return NotFound();

        }

        //usuario/editusuario
        public IActionResult EditUsuario(int? id)
        {

            Usuario usuario = _context.Usuario.First(m => m.usuarioID == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUsuario(int id, [Bind("usuarioID,dni,nombre,mail,password,esAdmin,bloqueado")] Usuario usuario)
        {
            if (id != usuario.usuarioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.usuarioID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
                if (usuario.esAdmin == true)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("DetailsUsuario", new { id = usuario.usuarioID });
                }
            }
            return View(usuario);
        }
        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("usuarioID,dni,nombre,mail,password,esAdmin,bloqueado")] Usuario usuario)
        {
            if (id != usuario.usuarioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.usuarioID))
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
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.usuarioID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }


        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.usuarioID == id);
        }

        private bool UsuarioExistsNamePass(int dni, String password)
        {
            if (_context.Usuario.Any(usuario => usuario.dni == dni && usuario.password == password))
            {
                return true;
            }
            return false;
        }

        private int UsuarioAutetication(int dni, String password)
        {
            Usuario usuario = _context.Usuario.First(m => m.dni == dni && m.password == password);

            if (usuario != null)
            {
                return usuario.usuarioID;
            }

            return 0;
        }
    }
}
