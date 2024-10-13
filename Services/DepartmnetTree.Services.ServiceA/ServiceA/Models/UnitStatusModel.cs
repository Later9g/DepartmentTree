using System.Text.Json.Serialization;

namespace DepartmentTree.Services.ServiceA  ;
public class UnitStatusModel
{
    [JsonPropertyName("status")]
    public string Status { get; set; }
}

