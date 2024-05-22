using FluentValidation;
using Magic8Ball.Application.Commands;
using Magic8Ball.Application.Enums;
using Magic8Ball.Application.Models;
using Magic8Ball.Domain.Common;
using Magic8Ball.Domain.Repositories;
using Magic8Ball.Infra.IntegrationEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Magic8Ball.Application.UseCases.Answer
{
    public record DeleteAnswerRequest : IDeleteCommand<int>
    {
        public int Id { get; init; }

        public class DeleteValidator : AbstractValidator<DeleteAnswerRequest>
        {
            public DeleteValidator()
            {
                RuleFor(v => v.Id)
                        .NotNull().WithMessage("Id is required.");
            }
        }
    }
    public class DeleteAnswerHandler : AnswerHandler, IRequestHandler<DeleteAnswerRequest, ActionResult>
    {
        public DeleteAnswerHandler(IAnswerRepository answerRepository, ILogRepository logRepository, IUnitOfWork unitOfWork) : base(answerRepository, logRepository, unitOfWork)
        {
        }

        public async Task<ActionResult> Handle(DeleteAnswerRequest request, CancellationToken cancellationToken)
        {
            var log = new Domain.Entities.Log()
            {
                LogMessage = "Request to delete an Answer",
                EndPoint = Enum.GetName(typeof(EndPoint), EndPoint.Answer)!,
                HttpVerb = Enum.GetName(typeof(HttpVerb), HttpVerb.DELETE)!,
                PayloadRequest = JsonSerializer.Serialize(request),
            };

            var entity = await _answerRepository.GetAsync(request.Id, cancellationToken);
            if (entity is null) {
                var ex = new Exceptions.AnswerNotFoudException($"Couldn't find Answer = {request.Id}");

                log.PayloadResponse = JsonSerializer.Serialize(ex);
                await AddLogAsync(log);

                return NotFound(ResultModel<Application.Exceptions.AnswerNotFoudException>.Create(ex));
            }

            await _answerRepository.RemoveAsync(entity.Id, cancellationToken: cancellationToken);

            var @event = new AnswerDeleted { Id = entity.Id.ToString() };

            log.PayloadResponse = "";
            await AddLogAsync(log);

            return Ok(ResultModel<bool>.Create(true));
        }
    }
}
