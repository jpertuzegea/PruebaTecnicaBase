//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Septiembre 2025</date>
//-----------------------------------------------------------------------

using Commons.Dtos.Configurations;
using Interfaces.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto.Controllers
{
    [Route("api/Login")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LoginController : ControllerBase
    {

        private readonly ILoginServices ILoginervices;

        public LoginController(ILoginServices _ILoginervices)
        {
            this.ILoginervices = _ILoginervices;
        }
         
        /// <summary>
        /// Permite iniciar sesion User:jorge Password:123456789
        /// </summary>
        [HttpPost("LogIn")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel<LoginDto>>> LogIn([FromBody] LoginDto LoginDto)
        {
            return await ILoginervices.Login(LoginDto);
        }


    }
}