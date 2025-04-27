using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Services;

public interface ITimeReportService: IModelService<TimeReport, Guid>
{
    public Task<IEnumerable<TimeReport>> GetTimeReports([FromQuery] int? ProjectId, [FromQuery] Guid? ClientId, [FromQuery] DateTime? Date);

}
