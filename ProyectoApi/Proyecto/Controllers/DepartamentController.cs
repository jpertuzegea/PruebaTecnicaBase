//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Septiembre 2025</date>
//-----------------------------------------------------------------------

using Commons.Dtos.Configurations;
using Commons.Dtos.Domains;
using Interfaces.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto.Controllers
{
    [Route("api/Departament")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DepartamentController : ControllerBase
    {

        private readonly IDepartamentService IDepartamentervices;

        public DepartamentController(IDepartamentService IDepartamentervices)
        {
            this.IDepartamentervices = IDepartamentervices;
        }


        /// <summary>
        /// Lista Deprtamentos
        /// </summary>
        [HttpGet("DepartamentList")]
        public async Task<ActionResult<ResultModel<DepartamentDto[]>>> Departament()
        {
            return await IDepartamentervices.DepartamentList();
        }


        /// <summary>
        /// Agrega Deprtamento
        /// </summary>
        [HttpPost("DepartamentAdd")]
        public async Task<ActionResult<ResultModel<string>>> DepartamentAdd([FromForm()] DepartamentDto DepartamentDto)
        {
            return await IDepartamentervices.DepartamentAdd(DepartamentDto);
        }


        /// <summary>
        /// Obtiene Deprtamento por id 
        /// </summary>
        [HttpPost("GetDepartamentByDepartamentId")]
        public async Task<ActionResult<ResultModel<DepartamentDto>>> GetDepartamentByDepartamentId([FromBody] int DepartamentId)
        {
            return await IDepartamentervices.GetDepartamentByDepartamentId(DepartamentId);
        }


        /// <summary>
        /// Actualiza Deprtamento
        /// </summary>

        [HttpPut("DepartamentUpdt")]
        public async Task<ActionResult<ResultModel<string>>> DepartamentUpdt([FromBody] DepartamentDto DepartamentDto)
        {
            return await IDepartamentervices.DepartamentUpdate(DepartamentDto);
        }


        /// <summary>
        /// Elimina Deprtamento
        /// </summary>
        [HttpDelete("DepartamentDelete/{DepartamentId}")]
        public async Task<ActionResult<ResultModel<string>>> DepartamentDelete(int DepartamentId)
        {
            return await IDepartamentervices.DepartamentDelete(DepartamentId);
        }

    }
}