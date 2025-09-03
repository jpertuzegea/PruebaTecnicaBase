//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Septiembre 2025</date>
//-----------------------------------------------------------------------

using System.Linq.Expressions;

namespace Infraestructure.Interfaces
{
    /// <summary>
    /// Define un contrato genérico para implementar el patrón Repository.
    /// Permite realizar operaciones CRUD y consultas sobre entidades
    /// de la base de datos, así como ejecutar procedimientos almacenados.
    /// </summary>
    /// <typeparam name="TEntity">Entidad de dominio a gestionar.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        #region Métodos de consulta (Get)

        /// <summary>
        /// Obtiene todos los registros de la entidad.
        /// </summary>
        Task<IEnumerable<TEntity>> Get();

        /// <summary>
        /// Obtiene los registros que cumplen con la condición especificada.
        /// </summary>
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// Obtiene los registros que cumplen la condición especificada
        /// e incluye las relaciones indicadas.
        /// </summary>
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> query, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Obtiene registros filtrados por condición y ordenados según el criterio especificado.
        /// </summary>
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, object>> orders, bool ascending = true);

        /// <summary>
        /// Obtiene registros filtrados, ordenados e incluyendo relaciones específicas.
        /// </summary>
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, object>> predicate, bool ascending = true, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Proyecta los resultados de la entidad hacia un tipo específico.
        /// </summary>
        Task<IEnumerable<TType>> Get<TType>(Expression<Func<TEntity, TType>> select) where TType : class;

        /// <summary>
        /// Proyecta resultados hacia un tipo específico con filtro aplicado.
        /// </summary>
        Task<IEnumerable<TType>> Get<TType>(Expression<Func<TEntity, TType>> select, Expression<Func<TEntity, bool>> query) where TType : class;

        #endregion

        #region Métodos de modificación (Add, Update, Remove)

        /// <summary>
        /// Agrega una nueva entidad al contexto.
        /// </summary>
        void Add(TEntity entity);

        /// <summary>
        /// Agrega un conjunto de entidades al contexto.
        /// </summary>
        void AddRange(List<TEntity> list);

        /// <summary>
        /// Actualiza una entidad existente en el contexto.
        /// </summary>
        void Update(TEntity entity);

        /// <summary>
        /// Elimina una entidad del contexto.
        /// </summary>
        void Remove(TEntity entity);

        /// <summary>
        /// Elimina un conjunto de entidades del contexto.
        /// </summary>
        void RemoveRange(List<TEntity> entity);

        /// <summary>
        /// Elimina entidades en base a una condición.
        /// </summary>
        void RemoveMasive(TEntity entity, Expression<Func<TEntity, bool>> query);

        #endregion

        #region Métodos de búsqueda (Find)

        /// <summary>
        /// Busca una entidad por su identificador.
        /// </summary>
        Task<TEntity> Find(int Id);

        /// <summary>
        /// Busca la primera entidad que cumpla la condición especificada.
        /// </summary>
        Task<TEntity> Find(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// Busca una entidad con condición y carga relaciones especificadas.
        /// </summary>
        Task<TEntity> Find(Expression<Func<TEntity, bool>> query, params Expression<Func<TEntity, object>>[] includes);

        #endregion

        #region Procedimientos almacenados

        /// <summary>
        /// Ejecuta un procedimiento almacenado en la base de datos
        /// y retorna el resultado como una colección de objetos.
        /// </summary>
        Task<IEnumerable<T>> ExecuteProcedure<T>(string nameProcedure, params object[] parameters) where T : class;

        #endregion
    }
}