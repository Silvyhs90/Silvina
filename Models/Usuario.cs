using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace tpc5FI.Models
{
	public class Usuario 
	{	[Key]
		public int usuarioID { get; set; }
		[Display(Name = "Documento")]
		[Required(ErrorMessage = "Este campo es requerido")]
		public int dni { get; set; }
		[Display(Name = "Nombre")]
		[Required(ErrorMessage = "Este campo es requerido")]
		public string nombre { get; set; }
		[Display(Name = "Email")]
		[Required(ErrorMessage = "Este campo es requerido")]
		public string mail { get; set; }
		[Display(Name = "Contraseña")]
		[Required(ErrorMessage = "Este campo es requerido")]
		public string password { get; set; }
		[Display(Name = "Es Administrador")]
		public bool esAdmin { get; set; }
		[Display(Name = "Bloqueado")]
		public bool bloqueado { get; set; }

		public int cant_intentos { get; set; }


		public Usuario()
		{

		}

		public Usuario(int dni, string nombre, string mail, string password, bool admin, bool bloqueado, int cant_intentos)
		{
			this.dni = dni;
			this.nombre = nombre;
			this.mail = mail;
			this.password = password;
			esAdmin = admin;
			this.bloqueado = bloqueado;
			this.cant_intentos = cant_intentos;
		}

		public int getDNI()
		{
			return dni;
		}

		public int getCodigo()
		{
			return usuarioID;
		}

		public String getNombre()
		{
			return nombre;
		}

		public String getMail()
		{
			return mail;
		}

		public String getPassword()
		{
			return password;
		}
		
		public bool getBloqueado()
		{
			return bloqueado;
		}

		public bool getAdmin()
		{
			return esAdmin;
		}

		public void setBloqueado()
		{
			if (bloqueado == false)
			{
				bloqueado = true;

			}
			else if (bloqueado == true)
			{
				bloqueado = false;

			}

		}
		public void setAdmin()
		{
			if (esAdmin == false)
			{
				esAdmin = true;

			}
			else if (esAdmin == true)
			{
				esAdmin = false;

			}

		}

		public void setDNI(int dni)
		{
			this.dni = dni;
		}

		public void setNombre(String nombre)
		{
			this.nombre = nombre;
		}

		public void setPassword(String password)
		{
			this.password = password;
		}

		public void setMail(String mail)
		{
			this.mail = mail;
		}

	}
}
