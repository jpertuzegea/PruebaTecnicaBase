//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Septiembre 2025</date>
//-----------------------------------------------------------------------

using Infraestructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;



namespace Infraestructure
{
    /// <summary>
    /// Contexto de base de datos principal para la aplicación.
    /// Implementa la configuración de Entity Framework Core y 
    /// expone los DbSet para interactuar con las entidades.
    /// </summary>
    public class ContextDB : DbContext
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor que recibe opciones de configuración de DbContext e inyección de IConfiguration.
        /// </summary>
        /// <param name="options">Opciones de configuración para DbContext.</param>
        /// <param name="configuration">Configuración de la aplicación (ejemplo: cadena de conexión).</param>
        public ContextDB(DbContextOptions<ContextDB> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Constructor alternativo que solo recibe IConfiguration.
        /// </summary>
        /// <param name="IConfiguration">Configuración de la aplicación.</param>
        public ContextDB(IConfiguration IConfiguration)
        {
            _configuration = IConfiguration;
        }

        /// <summary>
        /// Configuración del proveedor de base de datos (SQL Server en este caso).
        /// </summary>
        /// <param name="optionsBuilder">Constructor de opciones de DbContext.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                _configuration.GetConnectionString("BDConnetion"),
                opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds)
            );
        }

        /// <summary>
        /// Representa la tabla Departament en la base de datos.
        /// </summary>
        public DbSet<Departament> Departament { get; set; }
    }
}