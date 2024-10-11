namespace DepartmentTree.Services.ServiceA;

public interface IServiceA
{
    public Task<UnitStatusModel> GetUnitStatusByIdAsync(int id);
}
