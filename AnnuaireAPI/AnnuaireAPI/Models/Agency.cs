using System.Text.Json.Serialization;

namespace AnnuaireAPI.Models;

public partial class Agency
{
    public int Id { get; set; }

    public string City { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
