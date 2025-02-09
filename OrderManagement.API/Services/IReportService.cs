using OrderManagement.API.Models;

namespace OrderManagement.API.Services
{
    public interface IReportService : IHostedService
    {
        Task<List<ReportData>> GetCompletedOrderReportAsync();
    }
}
