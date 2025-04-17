namespace AnnuaireAPI.Models;

public class EmployeeDto
{
    public EmployeeDto(Employee employee)
    {
        Id = employee.Id;
        LastName = employee.LastName;
        FirstName = employee.FirstName;
        MobilePhoneNumber = employee.MobilePhoneNumber;
        FixedPhoneNumber = employee.FixedPhoneNumber;
        Email = employee.Email;
        AgencyId = employee.AgencyId;
        ServiceId = employee.ServiceId;
    }

    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string MobilePhoneNumber { get; set; } = null!;

    public string FixedPhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int AgencyId { get; set; }

    public int ServiceId { get; set; }
}
