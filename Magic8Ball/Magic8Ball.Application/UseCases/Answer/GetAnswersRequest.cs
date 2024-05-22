using FluentValidation;
using Magic8Ball.Application.Commands;
using Magic8Ball.Application.Dto;
using Magic8Ball.Application.Enums;
using Magic8Ball.Application.Models;
using Magic8Ball.Domain.Common;
using Magic8Ball.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Text.Json;

namespace Magic8Ball.Application.UseCases.Answer
{
    public record GetAnswersRequest: IListQuery<Domain.Entities.Answer>
    {
        public List<string> Includes { get; init; } = new();
        public List<Func<Domain.Entities.Answer, bool>> Filters { get; init; } = new();
        public List<string> Sorts { get; init; } = new();
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 20;        

        public class GetAnswersValidator : AbstractValidator<GetAnswersRequest>
        {
            public GetAnswersValidator() {
                RuleFor(x => x.Page)
                    .GreaterThanOrEqualTo(1).WithMessage("Page should at least greater than or equal to 1.");

                RuleFor(x => x.PageSize)
                    .GreaterThanOrEqualTo(1).WithMessage("PageSize should at least greater than or equal to 1.");
            }
        }
    }

    public class GetAnswersHandler : AnswerHandler, IRequestHandler<GetAnswersRequest, ActionResult>
    {
        public GetAnswersHandler(IAnswerRepository answerRepository, ILogRepository logRepository, IUnitOfWork unitOfWork) : base(answerRepository, logRepository, unitOfWork)
        {
        }

        public async Task<ActionResult> Handle(GetAnswersRequest request, CancellationToken cancellationToken)
        {
            var log = new Domain.Entities.Log()
            {
                LogMessage = "Request to get Answers",
                EndPoint = Enum.GetName(typeof(EndPoint), EndPoint.Answer)!,
                HttpVerb = Enum.GetName(typeof(HttpVerb), HttpVerb.GET)!,
                PayloadRequest = JsonSerializer.Serialize(request),
            };

            var answers = await _answerRepository.GetAsync(request.Filters, request.Page, request.PageSize, cancellationToken);

            var answerModels = answers.Items.Select(x => new AnswerDto(x.Id, x.Message, x.Type.ToString()));

            var resultModel = ListResultModel<AnswerDto>.Create(answerModels.ToList(), answers.TotalItemCount, answers.PageNumber, answers.PageSize);

            log.PayloadResponse = JsonSerializer.Serialize(resultModel);
            await AddLogAsync(log);

            return Ok(ResultModel<ListResultModel<AnswerDto>>.Create(resultModel));
        }
    }
}
