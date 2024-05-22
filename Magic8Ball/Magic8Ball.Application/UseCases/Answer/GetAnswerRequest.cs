using FluentValidation;
using Magic8Ball.Application.Commands;
using Magic8Ball.Application.Dto;
using Magic8Ball.Application.Enums;
using Magic8Ball.Application.Models;
using Magic8Ball.Domain.Common;
using Magic8Ball.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Linq.Expressions;

namespace Magic8Ball.Application.UseCases.Answer
{
    public record GetAnswerRequest : IQuery
    {
        public int Id { get; set; }

        public class GetValidator : AbstractValidator<GetAnswerRequest>
        {
            public GetValidator()
            {
                RuleFor(v => v.Id)
                    .GreaterThan(0).WithMessage("Id must be greater than 0");
            }
        }
    }

    public class GetAnswerHandler : AnswerHandler, IRequestHandler<GetAnswerRequest, ActionResult>
    {
        public GetAnswerHandler(IAnswerRepository answerRepository, ILogRepository logRepository, IUnitOfWork unitOfWork) : base(answerRepository, logRepository, unitOfWork)
        {
        }

        public async Task<ActionResult> Handle(GetAnswerRequest request, CancellationToken cancellationToken)
        {
            var log = new Domain.Entities.Log()
            {
                LogMessage = "Request to get an Answer",
                EndPoint = Enum.GetName(typeof(EndPoint), EndPoint.Answer)!,
                HttpVerb = Enum.GetName(typeof(HttpVerb), HttpVerb.GET)!,
                PayloadRequest = JsonSerializer.Serialize(request),
            };

            var item = await _answerRepository.GetAsync(request.Id, cancellationToken);
            if (item is null)
            {
                var ex = new Exceptions.AnswerNotFoudException($"Couldn't find Answer = {request.Id}");

                log.PayloadResponse = JsonSerializer.Serialize(ex);
                await AddLogAsync(log);

                return NotFound(ResultModel<Application.Exceptions.AnswerNotFoudException>.Create(ex));
            }

            var result = new AnswerDto(item.Id, item.Message, item.Type.ToString());
            
            log.PayloadResponse = JsonSerializer.Serialize(result);
            await AddLogAsync(log);

            return Ok(ResultModel<AnswerDto>.Create(result));
        }
    }
}
