//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Septiembre 2025</date>
//-----------------------------------------------------------------------

namespace Infraestructure.Entities
{
    /// <summary>
    /// Representa la entidad de base de datos para los Departamentos.
    /// Contiene la información básica de identificación, nombre y estado.
    /// </summary>
    public class Departament
    {
        /// <summary>
        /// Identificador único del departamento.
        /// Corresponde a la clave primaria en la base de datos.
        /// </summary>
        public int DepartamentId { get; set; }

        /// <summary>
        /// Nombre del departamento.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Estado del departamento representado como número.
        /// <list type="bullet">
        ///   <item><description>1 = Activo</description></item>
        ///   <item><description>0 = Inactivo</description></item>
        ///   <item><description>Otro valor = Desconocido</description></item>
        /// </list>
        /// </summary>
        public byte State { get; set; }
    }
}