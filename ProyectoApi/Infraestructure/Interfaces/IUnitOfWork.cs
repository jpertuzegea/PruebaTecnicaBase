//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Septiembre 2025</date>
//-----------------------------------------------------------------------

namespace Infraestructure.Interfaces
{
    /// <summary>
    /// Define un contrato para implementar el patrón Unit of Work.
    /// Coordina la persistencia de cambios en el contexto de datos
    /// y gestiona transacciones en la base de datos.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Obtiene un repositorio genérico para una entidad específica.
        /// </summary>
        /// <typeparam name="TEntity">Entidad de dominio.</typeparam>
        /// <returns>Instancia de <see cref="IRepository{TEntity}"/>.</returns>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        /// <summary>
        /// Persiste los cambios pendientes en la base de datos de manera sincrónica.
        /// </summary>
        /// <returns>True si se realizaron cambios, de lo contrario False.</returns>
        bool SaveChanges();

        /// <summary>
        /// Persiste los cambios pendientes en la base de datos de manera asincrónica.
        /// </summary>
        /// <returns>Número de registros afectados.</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Inicia una transacción en el contexto de datos.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Confirma la transacción en curso, aplicando todos los cambios.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Revierte la transacción en curso, deshaciendo todos los cambios no confirmados.
        /// </summary>
        void RollbackTransaction();
    }
}