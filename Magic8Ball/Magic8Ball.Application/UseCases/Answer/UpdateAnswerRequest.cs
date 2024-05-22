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
    public record UpdateAnswerRequest : IUpdateCommand<int>
    {
        public int Id { get; init; }
        public string Message { get; set; } = string.Empty;
        public string AnswerType { get; set; } = string.Empty;

        public class UpdateAnswerValidator : AbstractValidator<UpdateAnswerRequest>
        {
            public UpdateAnswerValidator()
            {
                RuleFor(v => v.Id)
                    .NotNull().WithMessage("Id is required.");

                RuleFor(v => v.Message)
                    .NotEmpty().WithMessage("Message is required.");

                RuleFor(v => v.AnswerType)
                    .NotEmpty().WithMessage("Type is required.");
            }
        }
    }

    public class UpdateAnswerHandler : AnswerHandler, IRequestHandler<UpdateAnswerRequest, ActionResult>
    {
        public UpdateAnswerHandler(IAnswerRepository answerRepository, ILogRepository logRepository, IUnitOfWork unitOfWork) : base(answerRepository, logRepository, unitOfWork)
        {
        }

        public async Task<ActionResult> Handle(UpdateAnswerRequest request, CancellationToken cancellationToken)
        {
            var log = new Domain.Entities.Log()
            {
                LogMessage = "Request to update an Answer",
                EndPoint = Enum.GetName(typeof(EndPoint), EndPoint.Answer)!,
                HttpVerb = Enum.GetName(typeof(HttpVerb), HttpVerb.PUT)!,
                PayloadRequest = JsonSerializer.Serialize(request),
            };

            var entity = await _answerRepository.GetAsync(request.Id, cancellationToken);
            if (entity is null)
            {
                var ex = new Exceptions.AnswerNotFoudException($"Couldn't find Answer = {request.Id}");

                log.PayloadResponse = JsonSerializer.Serialize(ex);
                await AddLogAsync(log);

                return NotFound(ResultModel<Application.Exceptions.AnswerNotFoudException>.Create(ex));
            }

            entity.Message = request.Message;
            entity.Type = Enum.Parse<AnswerType>(request.AnswerType);

            var created = await _answerRepository.UpdateAsync(entity, cancellationToken: cancellationToken);

            var @event = new AnswerUpdated { AnswerId = created.Id.ToString(), Message = created.Message, MessageType = created.Type.ToString() };
            var updated = new AnswerDto(created.Id, created.Message, created.Type.ToString());

            log.PayloadResponse = JsonSerializer.Serialize(updated);
            await AddLogAsync(log);

            return Ok(ResultModel<AnswerDto>.Create(updated));
        }
    }
}
