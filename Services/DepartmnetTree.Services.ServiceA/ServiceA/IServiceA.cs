namespace DepartmentTree.Services.ServiceA;

public interface IServiceA
{
    public Task<IEnumerable<UnitStatusModel>> GetStatusesAsync();
}
