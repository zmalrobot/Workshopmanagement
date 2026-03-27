using MongoDB.Bson.Serialization.Attributes;

namespace Workshopmanagement.DatabaseManagment.DataObject
{
    public class SearcherEmployee
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? InsertDateFrom { get; set; }
        public DateTime? InsertDateTo { get; set; }
    }

    public class Employee
    {
        [BsonId]
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? PhonePrefix { get; set; }
        public string? Email { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }

    public enum EmployeeRole
    {
        None,
        Manager,
        Employee
    }
}
