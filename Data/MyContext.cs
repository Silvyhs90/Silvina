using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tpc5FI.Models;

namespace tp5Fi.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
        }

        public DbSet<tpc5FI.Models.Alojamiento> Alojamiento { get; set; }

        public DbSet<tpc5FI.Models.Reserva> Reserva { get; set; }

        public DbSet<tpc5FI.Models.Usuario> Usuario { get; set; }


        
    }

}