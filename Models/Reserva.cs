using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace tpc5FI.Models
{
	public class Reserva 
	{   
		[Key]
		public int id { get; set; }
		[Display(Name = "Fecha Desde")]
		public DateTime FDesde { get; set; }
		[Display(Name = "Fecha Hasta")]
		public DateTime FHasta { get; set; }
		[Display(Name = "Nombre Alojamiento")]
		public string nombreAlojamiento { get; set; }
		[Display(Name = "Codigo de alojamiento")]
		public int codigoAlojamiento { get; set; }
		[Display(Name = "Codigo Usuario")]
		public int codigoPersona { get; set; }
		[Display(Name = "Precio")]
		public float precio { get; set; }

		public Reserva()
		{

		}

		public Reserva(DateTime desde, DateTime hasta, int codigoAlojamiento, int codigoPerson, float precio, string nombreAlojamiento)
		{

			FDesde = desde;
			FHasta = hasta;
			this.codigoAlojamiento = codigoAlojamiento;
			codigoPersona = codigoPerson;
			this.precio = precio;
			this.nombreAlojamiento = nombreAlojamiento;
		}

		public Reserva(int id, DateTime desde, DateTime hasta, int propiedad, int persona, float precio)
		{
			this.id = id;
			FDesde = desde;
			FHasta = hasta;
			codigoAlojamiento = propiedad;
			codigoPersona = persona;
			this.precio = precio;
		}

		public Reserva(  int propiedad, int persona, float precio,string nombreAlojamiento)
		{
			
			codigoAlojamiento = propiedad;
			codigoPersona = persona;
			this.precio = precio;
			this.nombreAlojamiento = nombreAlojamiento;
		}

		public int getID()
		{
			return id;
		}

		public DateTime getFDesde()
		{
			return FDesde;
		}

		public DateTime getFHasta()
		{
			return FHasta;
		}

		public int getAlojamiento()
		{
			return codigoAlojamiento;
		}

		public int getUsuario()
		{
			return codigoPersona;
		}

		public float getPrecio()
		{
			return precio;
		}

		public void setID(int id)
		{
			this.id = id;
		}

		public void setPrecio(float precio)
		{
			this.precio = precio;
		}

		public void setUsuario(int usuario)
		{
			codigoPersona = usuario;
		}

		public void setAlojamiento(int alojamiento)
		{
			codigoAlojamiento = alojamiento;
		}

		public void setFhasta(DateTime fHasta)
		{
			FHasta = fHasta;
		}

		public void setFDesde(DateTime fDesde)
		{
			this.FDesde = fDesde;
		}

		public void setId(int id)
		{
			this.id = id;
		}
	}
}
