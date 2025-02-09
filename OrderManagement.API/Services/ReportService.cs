using OrderManagement.API.Models;
using OrderManagement.API.Models.Enums;

namespace OrderManagement.API.Services
{
    public class ReportService : BackgroundService, IReportService
    {
        private readonly IOrderRepository _orderRepository;
        private List<ReportData> _reportData = new List<ReportData>();
        private readonly TimeSpan _reportInterval = TimeSpan.FromMinutes(2);

        public ReportService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await GenerateCompletedOrderReportAsync();
                await Task.Delay(_reportInterval, stoppingToken);
            }
        }

        public Task<List<ReportData>> GetCompletedOrderReportAsync()
        {
            return Task.FromResult(_reportData.ToList());
        }

        private async Task GenerateCompletedOrderReportAsync()
        {
            _reportData.Clear();
            var completedOrders = (await _orderRepository.GetOrders()).Where(o => Equals(o.OrderStatus, OrderStatus.Completed)).ToList();
            if (!completedOrders.Any())
                return;

            _reportData = completedOrders.GroupBy(o => o.Customer.CustomerName)
                                           .Select(gr => new ReportData
                                           {
                                               CustomerName = gr.Key,
                                               TotalQuantity = gr.Sum(o => o.Items.Sum(i => i.Quantity)),
                                               TotalAmount = gr.Sum(o => o.Items.Sum(i => i.Price))
                                           }).ToList();
        }
    }
}
