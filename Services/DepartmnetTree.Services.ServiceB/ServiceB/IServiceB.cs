using DepartmentTree.Services.ServiceA;

namespace DepartmnetTree.Services.ServiceB;

public interface IServiceB
{
    public Task<UnitStatusModel> GetStatusesAsync(int Id);
    public Task UpdateUnitAsync(int id, UpdateModel model);
    public Task<IEnumerable<OtherUnitModel>> GetUnitsAsync();
    public Task SyncUnitsAsync(IEnumerable<OtherUnitModel> models);



}
