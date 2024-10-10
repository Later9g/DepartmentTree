using System.ComponentModel.DataAnnotations;

namespace DepartmentTree.Context.Entities;

public class Unit
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public int? ParentId { get; set; } // Связь с родительским подразделением
}
