using System.Text.Json.Serialization;

namespace AnnuaireAPI.Models;

public partial class Service
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
