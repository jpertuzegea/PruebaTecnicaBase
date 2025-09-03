//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Septiembre 2025</date>
//-----------------------------------------------------------------------

namespace Commons.Dtos.Configurations
{
    /// <summary>
    /// Representa la configuración utilizada para la autenticación 
    /// basada en JWT (JSON Web Token).
    /// </summary>
    public class JWTAuthentication
    {
        /// <summary>
        /// Duración del token en minutos antes de que expire.
        /// </summary>
        public int ExpirationInMinutes { get; set; }

        /// <summary>
        /// Clave secreta utilizada para firmar y validar los tokens JWT.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Lista de orígenes permitidos para CORS, desde los cuales 
        /// se aceptarán solicitudes hacia la API.
        /// </summary>
        public string[] HostOriginPermited { get; set; }
    }



    /// <summary>
    /// Representa la configuración relacionada con el uso de caché
    /// en la aplicación.
    /// </summary>
    public class Cache
    {
        /// <summary>
        /// Tiempo de vida del caché en horas antes de que expire.
        /// </summary>
        public int ExpirationCacheInHours { get; set; }
    }


}