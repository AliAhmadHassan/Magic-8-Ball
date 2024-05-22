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

namespace Magic8Ball.Application.UseCases.Answer
{
    public record GetYesNoQuestionAnswerRequest : IQuery
    {
        public string Question { get; set; }

        public class GetYesNoQuestionAnswerValidator : AbstractValidator<GetYesNoQuestionAnswerRequest>
        {
            public GetYesNoQuestionAnswerValidator() {
                RuleFor(x => x.Question)
                        .NotEmpty().WithMessage("Question is required.");
            }
        }
    }

    public class GetYesNoQuestionAnswerHandler : AnswerHandler, IRequestHandler<GetYesNoQuestionAnswerRequest, ActionResult>
    {
        public GetYesNoQuestionAnswerHandler(IAnswerRepository answerRepository, ILogRepository logRepository, IUnitOfWork unitOfWork) : base(answerRepository, logRepository, unitOfWork)
        {
        }

        public async Task<ActionResult> Handle(GetYesNoQuestionAnswerRequest request, CancellationToken cancellationToken)
        {
            var log = new Domain.Entities.Log()
            {
                LogMessage = "Request to get an Yes/No Answer",
                EndPoint = Enum.GetName(typeof(EndPoint), EndPoint.Answer)!,
                HttpVerb = Enum.GetName(typeof(HttpVerb), HttpVerb.GET)!,
                PayloadRequest = JsonSerializer.Serialize(request),
            };

            var item = await _answerRepository.GetRandomAsync(cancellationToken);
            if (item is null)
            {
                var ex = new Exceptions.AnswerNotFoudException($"Couldn't find any answer");

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
