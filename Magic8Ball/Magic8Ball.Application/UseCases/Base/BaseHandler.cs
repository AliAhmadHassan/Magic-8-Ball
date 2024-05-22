using Magic8Ball.Domain.Common;
using Magic8Ball.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Application.UseCases.Base
{
    public abstract class BaseHandler
    {
        protected readonly ILogRepository _logRepository;
        private readonly IUnitOfWork _unitOfWork;
        public BaseHandler(ILogRepository logRepository, IUnitOfWork unitOfWork)
        {
            _logRepository = logRepository;
            _unitOfWork = unitOfWork;
        }

        protected virtual OkResult Ok() => new OkResult();
        protected virtual OkObjectResult Ok(object? value) => new OkObjectResult(value);
        protected virtual NotFoundResult NotFound() => new NotFoundResult();
        protected virtual NotFoundObjectResult NotFound(object? value) => new NotFoundObjectResult(value);

        protected async virtual Task AddLogAsync(Domain.Entities.Log log, CancellationToken cancellationToken = default)
        {
            await _logRepository.AddAsync(log, cancellationToken: cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
