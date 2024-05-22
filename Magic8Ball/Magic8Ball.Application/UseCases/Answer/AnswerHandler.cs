using Magic8Ball.Application.Commands;
using Magic8Ball.Application.Dto;
using Magic8Ball.Application.Models;
using Magic8Ball.Domain.Common;
using Magic8Ball.Domain.Repositories;
using Magic8Ball.Infra.IntegrationEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Application.UseCases.Answer
{
    public abstract class AnswerHandler: Base.BaseHandler
    {
        protected readonly IAnswerRepository _answerRepository;

        public AnswerHandler(IAnswerRepository answerRepository, ILogRepository logRepository, IUnitOfWork unitOfWork): base(logRepository, unitOfWork)
        {
            _answerRepository = answerRepository;
        }
    }
}
