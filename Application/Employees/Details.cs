using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Helpers;
using DataStore;
using Domain;
using MediatR;
using Raven.Client.Documents;

namespace Application.Employees
{
    public class Details
    {
        public class Query : IRequest<EmployeeDto>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, EmployeeDto>
        {
            private readonly IDocumentStore _store;
            public Handler(DocumentStoreHolder documentStore)
            {
                _store = documentStore.Store;
            }
            public async Task<EmployeeDto> Handle(Query request, CancellationToken cancellationToken)
            {
                using (var session = _store.OpenAsyncSession())
                {
                    var employee = await session.LoadAsync<Employee>(request.Id);

                    var employeeDto = new EmployeeDto
                    {
                        Id = new RavenId<Employee>(employee.Id),
                        Name = employee.Name
                    };

                    return employeeDto;
                }
            }
        }
    }
}