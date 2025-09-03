//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Septiembre 2025</date>
//-----------------------------------------------------------------------

using AutoMapper;
using Commons.Dtos.Domains;
using Infraestructure.Entities;

namespace Services.Mappers
{
    /// <summary>
    /// Configuración de mapeos entre entidades del dominio y Data Transfer Objects (DTOs).
    /// Se utiliza AutoMapper para simplificar la conversión entre modelos de base de datos 
    /// y objetos expuestos en la capa de aplicación.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Inicializa las reglas de mapeo de AutoMapper.
        /// </summary>
        public MappingProfile()
        {
            /// <summary>
            /// Convierte de <see cref="Departament"/> (entidad de base de datos)
            /// a <see cref="DepartamentDto"/> (objeto de transferencia).
            /// Incluye la lógica para traducir el valor numérico del estado
            /// en una representación textual legible.
            /// </summary>
            CreateMap<Departament, DepartamentDto>()
                .ForMember(dest => dest.NameState, opt => opt.MapFrom(src =>
                    src.State == 1 ? "Activo" :
                    src.State == 0 ? "Inactivo" : "Desconocido"
                ));

            /// <summary>
            /// Convierte de <see cref="DepartamentDto"/> a <see cref="Departament"/>.
            /// Útil para operaciones de inserción y actualización en la base de datos.
            /// </summary>
            CreateMap<DepartamentDto, Departament>();
        }
    }
}