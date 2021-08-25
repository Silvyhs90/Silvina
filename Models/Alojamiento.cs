using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace tpc5FI.Models
{
    public class Alojamiento
    {
        [Key]
        public int codigo { get; set; }
        [Display(Name = "Tipo alojamiento")]
        public string tipo { get; set; }
        [Display(Name = "Ciudad")]
        public string ciudad { get; set; }
        [Display(Name = "Barrio")]
        public string barrio { get; set; }
        [Display(Name = "Estrellas")]
        public int estrellas { get; set; }
        [Display(Name = "Cantidad de personas")]
        public int cantPersonas { get; set; }
        [Display(Name = "Tiene Tv")]
        public bool tv { get; set; }
        [Display(Name = "Precio por Dia")]
        public double precioxDia { get; set; }
        [Display(Name = "Precio por Persona")]
        public double precioxPersona { get; set; }
        [Display(Name = "Habitaciones")]
        public int habitacion { get; set; }
        [Display(Name = "Baños")]
        public int baño { get; set; }
        [Display(Name = "Ocupado")]
        public bool ocupado { get; set; }

        public Alojamiento()
        {

        }

        public Alojamiento(string ciudad, string barrio, int estrellas, int cantPersonas, bool tv, double precioxDia , double precioxPersona, int habitacion, int baño, bool ocupado,string tipo)
        {
            this.ciudad = ciudad;
            this.barrio = barrio;
            this.estrellas = estrellas;
            this.cantPersonas = cantPersonas;
            this.tv = tv;
            this.precioxDia = precioxDia;
            this.precioxPersona = precioxPersona;
            this.habitacion = habitacion;
            this.baño = baño;
            this.ocupado = ocupado;
            this.tipo = tipo;

        }
    }
}
