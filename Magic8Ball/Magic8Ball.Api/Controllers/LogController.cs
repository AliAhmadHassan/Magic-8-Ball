using Magic8Ball.Api.Extensions;
using Magic8Ball.Application.Dto;
using Magic8Ball.Application.Models;
using Magic8Ball.Application.UseCases.Log;
using Magic8Ball.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net;

namespace Magic8Ball.Api.Controllers
{

    public class LogController : BaseController
    {
        private readonly IMediator _mediator;

        public LogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Consultar Janelas de Agendamento
        /// </summary>
        /// <response code="200">OK</response>
        /// <response code="400">The request could not be understood by the server</response>
        /// <response code="401">The requested resource requires authentication</response>
        /// <response code="404">The requested resource does not exist on the server</response>
        /// <response code="500">That a generic error has occurred on the server</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<LogDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GeneralResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromHeader(Name = "x-query")] string? xQuery)
        {
            var queryModel = HttpContext.SafeGetListQuery<GetLogsRequest, ListResultModel<LogDto>, Log>(xQuery);
            var result = await _mediator.Send(queryModel);
            return Ok(result);
        }
    }
}
