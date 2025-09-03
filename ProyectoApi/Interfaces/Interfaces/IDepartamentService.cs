//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Septiembre 2025</date>
//-----------------------------------------------------------------------

using Commons.Dtos.Configurations;
using Commons.Dtos.Domains;

namespace Interfaces.Interfaces
{
    /// <summary>
    /// Define el contrato para los servicios relacionados con la gestión 
    /// de Departamentos dentro del sistema.
    /// 
    /// Incluye operaciones CRUD (Crear, Leer, Actualizar, Eliminar),
    /// así como listados y búsquedas por identificador.
    /// </summary>
    public interface IDepartamentService
    {
        /// <summary>
        /// Agrega un nuevo departamento al sistema.
        /// </summary>
        /// <param name="DepartamentDto">
        /// Objeto <see cref="DepartamentDto"/> que contiene la información del
        /// departamento a registrar.
        /// </param> 
        Task<ResultModel<string>> DepartamentAdd(DepartamentDto DepartamentDto);

        /// <summary>
        /// Obtiene la lista completa de todos los departamentos disponibles.
        /// </summary> 
        Task<ResultModel<DepartamentDto[]>> DepartamentList();

        /// <summary>
        /// Busca un departamento por su identificador único.
        /// </summary>
        /// <param name="Id">
        /// Identificador único del departamento que se desea consultar.
        /// </param> 
        Task<ResultModel<DepartamentDto>> GetDepartamentByDepartamentId(int Id);

        /// <summary>
        /// Actualiza los datos de un departamento existente.
        /// </summary>
        /// <param name="DepartamentModel">
        /// Objeto <see cref="DepartamentDto"/> que contiene los datos 
        /// actualizados del departamento.
        /// </param> 
        Task<ResultModel<string>> DepartamentUpdate(DepartamentDto DepartamentModel);

        /// <summary>
        /// Elimina un departamento del sistema mediante su identificador único.
        /// </summary>
        /// <param name="DepartamentId">
        /// Identificador único del departamento que se desea eliminar.
        /// </param> 
        Task<ResultModel<string>> DepartamentDelete(int DepartamentId);
    }
}