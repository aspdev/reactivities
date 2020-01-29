using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;

namespace Application.Employees
{
    public class List
    {
        public class Query : IRequest<List<Employee>> { }

        public class Handler : IRequestHandler<Query, List<Employee>>
        {
            public Handler()
            {

            }

            public Task<List<Employee>> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}