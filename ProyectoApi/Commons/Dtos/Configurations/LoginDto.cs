//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Septiembre 2025</date>
//-----------------------------------------------------------------------

namespace Commons.Dtos.Configurations
{
    /// <summary>
    /// Representa el objeto de transferencia de datos (DTO) 
    /// utilizado en el proceso de autenticación de usuarios.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Nombre de usuario con el cual se realiza el inicio de sesión.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Contraseña del usuario. Puede ser <c>null</c> en escenarios 
        /// donde se use autenticación con token u otro mecanismo.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Indica si el usuario se encuentra autenticado en el sistema.
        /// </summary>
        public bool IsLogued { get; set; }

        /// <summary>
        /// Token JWT generado tras una autenticación exitosa. 
        /// Se utiliza para autorizar solicitudes posteriores.
        /// </summary>
        public string? Token { get; set; }
    }
}

