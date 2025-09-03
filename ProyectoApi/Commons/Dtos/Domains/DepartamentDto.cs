//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Septiembre 2025</date>
//-----------------------------------------------------------------------

namespace Commons.Dtos.Domains
{

    /// <summary>
    /// Objeto de transferencia de datos (DTO) que representa 
    /// la información de un departamento dentro del sistema.
    /// </summary>
    public class DepartamentDto
    {
        /// <summary>
        /// Identificador único del departamento.
        /// </summary>
        public int DepartamentId { get; set; }

        /// <summary>
        /// Nombre del departamento.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Estado del departamento representado como valor numérico.
        /// Usualmente: 1 = Activo, 0 = Inactivo.
        /// </summary>
        public byte State { get; set; }

        /// <summary>
        /// Representación textual del estado del departamento
        /// (ejemplo: "Activo", "Inactivo").
        /// Puede ser <c>null</c> si no se ha asignado.
        /// </summary>
        public string? NameState { get; set; }
    }
}