using FluentValidation;
using Magic8Ball.Application.Commands;
using Magic8Ball.Application.Dto;
using Magic8Ball.Application.Enums;
using Magic8Ball.Application.Models;
using Magic8Ball.Domain.Common;
using Magic8Ball.Domain.Enums;
using Magic8Ball.Domain.Repositories;
using Magic8Ball.Infra.IntegrationEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace Magic8Ball.Application.UseCases.Answer
{
    public record CreateAnswerRequest : ICreateCommand
    {
        public string Message { get; set; } = string.Empty;
        public string AnswerType { get; set; } = string.Empty;

        public Magic8Ball.Domain.Entities.Answer ToAnswer()
        {
            var answer = new Magic8Ball.Domain.Entities.Answer();
            answer.Type = Enum.Parse<AnswerType>(AnswerType);
            answer.Message = Message;
            return answer;
        }

        public class CreateValidator: AbstractValidator<CreateAnswerRequest>
        {
            public CreateValidator()
            {
                RuleFor(v=>v.Message)
                    .NotEmpty().WithMessage("Message is required.");

                RuleFor(v => v.AnswerType)
                    .NotEmpty().WithMessage("AnswerType is required.");
            }
        }
    }

    public class CreateAnswerHandler : AnswerHandler, IRequestHandler<CreateAnswerRequest, ActionResult>
    {
        public CreateAnswerHandler(IAnswerRepository answerRepository, ILogRepository logRepository, IUnitOfWork unitOfWork) : base(answerRepository, logRepository, unitOfWork)
        { 
        }

        public async Task<ActionResult> Handle(CreateAnswerRequest request, CancellationToken cancellationToken)
        {
            var log = new Domain.Entities.Log()
            {
                LogMessage = "Request to create an Answer",
                EndPoint = Enum.GetName(typeof(EndPoint), EndPoint.Answer)!,
                HttpVerb = Enum.GetName(typeof(HttpVerb), HttpVerb.POST)!,
                PayloadRequest = JsonSerializer.Serialize(request),
            };

            var entity = request.ToAnswer();
            var created = await _answerRepository.AddAsync(entity, cancellationToken: cancellationToken);

            var @event = new AnswerCreated { AnswerId = created.Id.ToString(), Message = created.Message, MessageType = created.Type.ToString() };
            var result = new AnswerDto(created.Id, created.Message, created.Type.ToString());
            log.PayloadResponse = JsonSerializer.Serialize(result);
            await AddLogAsync(log);

            return Ok(ResultModel<AnswerDto>.Create(result));
        }
    }
}
