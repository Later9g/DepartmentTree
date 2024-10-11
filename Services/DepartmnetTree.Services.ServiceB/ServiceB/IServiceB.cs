using DepartmentTree.Services.ServiceA;

namespace DepartmentTree.Services.ServiceB;

public interface IServiceB
{
    public Task<UnitStatusModel> GetStatusesAsync(int id);
    public Task UpdateUnitAsync(int id, UpdateModel model);
    public Task<IEnumerable<OtherUnitModel>> GetUnitsAsync();
    public Task SyncUnitsAsync(IEnumerable<OtherUnitModel> models);



}
