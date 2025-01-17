﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tp5Fi.Data;

namespace tp5Fi.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20210814004715_cinco")]
    partial class cinco
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("tpc5FI.Models.Alojamiento", b =>
                {
                    b.Property<int>("codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("barrio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("baño")
                        .HasColumnType("int");

                    b.Property<int>("cantPersonas")
                        .HasColumnType("int");

                    b.Property<string>("ciudad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("estrellas")
                        .HasColumnType("int");

                    b.Property<int>("habitacion")
                        .HasColumnType("int");

                    b.Property<bool>("ocupado")
                        .HasColumnType("bit");

                    b.Property<double>("precioxDia")
                        .HasColumnType("float");

                    b.Property<double>("precioxPersona")
                        .HasColumnType("float");

                    b.Property<string>("tipo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("tv")
                        .HasColumnType("bit");

                    b.HasKey("codigo");

                    b.ToTable("Alojamiento");
                });

            modelBuilder.Entity("tpc5FI.Models.Reserva", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FDesde")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FHasta")
                        .HasColumnType("datetime2");

                    b.Property<int>("codigoAlojamiento")
                        .HasColumnType("int");

                    b.Property<int>("codigoPersona")
                        .HasColumnType("int");

                    b.Property<string>("nombreAlojamiento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("precio")
                        .HasColumnType("real");

                    b.HasKey("id");

                    b.ToTable("Reserva");
                });

            modelBuilder.Entity("tpc5FI.Models.Usuario", b =>
                {
                    b.Property<int>("usuarioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("bloqueado")
                        .HasColumnType("bit");

                    b.Property<int>("cant_intentos")
                        .HasColumnType("int");

                    b.Property<int>("dni")
                        .HasColumnType("int");

                    b.Property<bool>("esAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("usuarioID");

                    b.ToTable("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
