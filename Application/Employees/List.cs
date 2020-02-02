using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataStore;
using Domain;
using MediatR;
using Raven.Client.Documents;

namespace Application.Employees
{
    public class List
    {
        public class Query : IRequest<List<Employee>> { }

        public class Handler : IRequestHandler<Query, List<Employee>>
        {
            private readonly IDocumentStore _store;
            public Handler(DocumentStoreHolder storeHolder)
            {
                _store = storeHolder.Store;
            }

            public async Task<List<Employee>> Handle(Query request, CancellationToken cancellationToken)
            {
                using (var session = _store.OpenAsyncSession())
                {
                    var employees = await session.Query<Employee>().ToListAsync();

                    return employees;
                }
            }
        }
    }
}