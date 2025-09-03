//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Septiembre 2025</date>
//-----------------------------------------------------------------------

using Commons.Dtos.Configurations;



namespace Interfaces.Interfaces
{
    /// <summary>
    /// Define el contrato para los servicios relacionados con la 
    /// autenticación de usuarios dentro del sistema.
    /// </summary>
    public interface ILoginServices
    {
        /// <summary>
        /// Permite realizar el inicio de sesión de un usuario.
        /// </summary>
        /// <param name="LoginDto">
        /// Objeto <see cref="LoginDto"/> que contiene las credenciales 
        /// (nombre de usuario y contraseña) necesarias para la autenticación.
        /// </param>
        /// <returns>
        /// Un <see cref="ResultModel{T}"/> que contiene un 
        /// <see cref="LoginDto"/> actualizado con información de sesión,
        /// como el estado de autenticación (<c>IsLogued</c>) y el token JWT
        /// generado en caso de éxito.
        /// </returns>
        Task<ResultModel<LoginDto>> Login(LoginDto LoginDto);
    }
}