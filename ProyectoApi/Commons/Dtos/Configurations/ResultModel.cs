//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Septiembre 2025</date>
//-----------------------------------------------------------------------

namespace Commons.Dtos.Configurations
{
    /// <summary>
    /// Representa un modelo genérico de resultado que encapsula la 
    /// respuesta de una operación de negocio o servicio.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de dato que contendrá la respuesta (<see cref="Data"/>).
    /// Debe ser una clase de referencia.
    /// </typeparam>
    public class ResultModel<T> where T : class
    {
        /// <summary>
        /// Indica si la operación produjo un error.
        /// <c>true</c> si hubo error, <c>false</c> si fue exitosa.
        /// </summary>
        public bool HasError { get; set; }

        /// <summary>
        /// Mensaje técnico con detalles adicionales del error,
        /// como el stack trace o información de depuración.
        /// Suele usarse solo para desarrolladores.
        /// </summary>
        public string? ExceptionMessage { get; set; }

        /// <summary>
        /// Mensaje amigable o descriptivo con el resultado de la operación.
        /// Ejemplos: "Operación exitosa", "Departamento no encontrado".
        /// </summary>
        public string? Messages { get; set; }

        /// <summary>
        /// Objeto o colección de datos que devuelve la operación.
        /// Será <c>null</c> en caso de error o si no se encontraron resultados.
        /// </summary>
        public T? Data { get; set; }
    }
}