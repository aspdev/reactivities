using System.Threading.Tasks;
using API.DataStore;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;

namespace API.Controllers
{
    [Route("api/{controller}")]
    public class EmployeeController : Controller
    {
        private readonly IDocumentStore _store;
        public EmployeeController(ReactivitiesDocumentStoreHolder storeHolder)
        {
            _store = storeHolder.Store;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            using (var session = _store.OpenAsyncSession())
            {
                var employees = await session.Query<Employee>().ToListAsync();

                return Ok(employees);
            }
        }
    }
}