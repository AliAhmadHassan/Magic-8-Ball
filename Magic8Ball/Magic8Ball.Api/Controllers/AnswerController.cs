using Magic8Ball.Api.Extensions;
using Magic8Ball.Application.Dto;
using Magic8Ball.Application.Models;
using Magic8Ball.Application.UseCases.Answer;
using Magic8Ball.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Magic8Ball.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AnswerController : BaseController
    {
        private readonly IMediator _mediator;

        public AnswerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Search all answers actived
        /// </summary>
        /// <param name="xQuery">Filters</param>
        /// <response code="200">OK</response>
        /// <response code="400">The request could not be understood by the server</response>
        /// <response code="401">The requested resource requires authentication</response>
        /// <response code="404">The requested resource does not exist on the server</response>
        /// <response code="500">That a generic error has occurred on the server</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResultModel<ListResultModel<AnswerDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GeneralResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromHeader(Name = "x-query")] string? xQuery)
        {
            var queryModel = HttpContext.SafeGetListQuery<GetAnswersRequest, ListResultModel<AnswerDto>, Answer>(xQuery);
            var result = await _mediator.Send(queryModel);
            return Ok(result);
        }

        /// <summary>
        /// Create a new Answer
        /// </summary>
        /// <response code="200">Agendamento criado com sucesso</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Informação não encontrada</response>
        /// <response code="500">Ocorreu um erro interno</response>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResultModel<AnswerDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GeneralResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateAnswerRequest answerRequest)
            => await _mediator.Send(new CreateAnswerRequest() { Message = answerRequest.Message, AnswerType = answerRequest.AnswerType });

        /// <summary>
        /// Update an Answer
        /// </summary>
        /// <param name="id">Id of Answer that will be changed</param>
        /// <response code="200">Agendamento criado com sucesso</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Informação não encontrada</response>
        /// <response code="500">Ocorreu um erro interno</response>
        /// <returns></returns>
        [HttpPut("{answerId}")]
        [ProducesResponseType(typeof(ResultModel<AnswerDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GeneralResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Put(int answerId, [FromBody] UpdateAnswerRequest answerRequest)
            => await _mediator.Send(new UpdateAnswerRequest() { Message = answerRequest.Message, AnswerType = answerRequest.AnswerType, Id = answerId });


        /// <summary>
        /// Get an Answer
        /// </summary>
        /// <param name="id">Id of Answer</param>
        /// <response code="200">Agendamento criado com sucesso</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Informação não encontrada</response>
        /// <response code="500">Ocorreu um erro interno</response>
        /// <returns></returns>
        [HttpGet("{answerId}")]
        [ProducesResponseType(typeof(ResultModel<AnswerDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GeneralResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(int answerId)
            => await _mediator.Send(new GetAnswerRequest() { Id = answerId });

        /// <summary>
        /// Get an Yes-No Answer
        /// </summary>
        /// <param name="id">Id of Answer</param>
        /// <response code="200">Agendamento criado com sucesso</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Informação não encontrada</response>
        /// <response code="500">Ocorreu um erro interno</response>
        /// <returns></returns>
        [HttpGet("yes-no-question/{question}")]
        [ProducesResponseType(typeof(ResultModel<AnswerDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GeneralResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByYesNoQuestion(string question)
            => await _mediator.Send(new GetYesNoQuestionAnswerRequest() { Question = question });

        /// <summary>
        /// Delete an Answer
        /// </summary>
        /// <param name="id">Id of Answer</param>
        /// <response code="200">Agendamento criado com sucesso</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Informação não encontrada</response>
        /// <response code="500">Ocorreu um erro interno</response>
        /// <returns></returns>
        [HttpDelete("{answerId}")]
        [ProducesResponseType(typeof(ResultModel<AnswerDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GeneralResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int answerId)
            => await _mediator.Send(new DeleteAnswerRequest() { Id = answerId });

    }
}
