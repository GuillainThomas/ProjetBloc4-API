using System.Text.Json.Serialization;

namespace AnnuaireAPI.Models;

public partial class Service
{
    public Service() { }

    public Service(ServiceDto serviceDto)
    {
        Id = serviceDto.Id;
        Name = serviceDto.Name;
    }

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
