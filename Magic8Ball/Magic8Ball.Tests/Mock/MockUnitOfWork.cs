using Magic8Ball.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Tests.Mock
{
    public class MockUnitOfWork : IUnitOfWork
    {
        public Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<int>(1);// throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}
