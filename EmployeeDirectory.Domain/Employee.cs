using EmployeeDirectory.Domain.Enums;

namespace EmployeeDirectory.Domain
{
    public class Employee : EntityBase
    {
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public EmployeeGender Gender { get; set; }
        
        public int GetEmployeeAge()
        {
            return DateTime.Now.Year - DateOfBirth.Year - (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);
        }
    }
}