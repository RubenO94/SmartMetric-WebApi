﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.Domain.RepositoryContracts;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DepartmentsController : CustomBaseController
    {
        private readonly ISmartTimeRepository _smartTimeRepository;

        public DepartmentsController(ISmartTimeRepository smartTimeRepository)
        {
            _smartTimeRepository = smartTimeRepository;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetDepartmentsByPerfilId([FromQuery] int perfilID)
        //{
        //    var result = await _smartTimeRepository.GetDepartmentsByPerfilId(perfilID);

        //    return Ok(result);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetDepartmentsByChiefId([FromQuery] int chiefId)
        //{
        //    var result = await _smartTimeRepository.GetDepartmentsByChiefId(chiefId);

        //    return Ok(result);
        //}
    }
}
