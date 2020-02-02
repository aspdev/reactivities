using Application.Helpers;
using Domain;

namespace Application.Dtos
{
    public class EmployeeDto
    {
        public RavenId<Employee> Id { get; set; }
        public string Name { get; set; }
    }
}