namespace AnnuaireAPI.Models;

public partial class Employee
{
    public Employee() { }

    public Employee(EmployeeDto employeeDto)
    {
        Id = employeeDto.Id;
        LastName = employeeDto.LastName;
        FirstName = employeeDto.FirstName;
        MobilePhoneNumber = employeeDto.MobilePhoneNumber;
        FixedPhoneNumber = employeeDto.FixedPhoneNumber;
        Email = employeeDto.Email;
        AgencyId = employeeDto.AgencyId;
        ServiceId = employeeDto.ServiceId;
    }

    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string MobilePhoneNumber { get; set; } = null!;

    public string FixedPhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int AgencyId { get; set; }

    public int ServiceId { get; set; }

    public virtual Agency Agency { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
