using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Infra.IntegrationEvents
{
    public partial class AnswerCreated: INotification
    {
    }

    public partial class AnswerUpdated : INotification
    {
    }

    public partial class AnswerDeleted : INotification
    {
    }
}
