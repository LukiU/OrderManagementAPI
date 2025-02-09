using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderManagement.API.Controllers;
using OrderManagement.API.Models;
using OrderManagement.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Tests
{
    public class ReportControllerTests
    {
        private readonly Mock<IReportService> _reportServiceMock;
        private readonly ReportController _controller;

        public ReportControllerTests()
        {
            _reportServiceMock = new Mock<IReportService>();
            _controller = new ReportController(_reportServiceMock.Object);
        }

        [Fact]
        public async Task GetReport_ReturnsOkResult_WithReportData()
        {
            var reportData = new List<ReportData> { 
                new ReportData { 
                    CustomerName = "Test Customer", 
                    TotalQuantity = 1, 
                    TotalAmount = 10 
                } 
            };
            _reportServiceMock.Setup(repo => repo.GetCompletedOrderReportAsync()).ReturnsAsync(reportData);

            var result = await _controller.GetCompletedOrderReport();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedReportData = Assert.IsType<List<ReportData>>(okResult.Value);
            Assert.Single(returnedReportData);
            Assert.Equal("Test Customer", returnedReportData.First().CustomerName);
        }
    }
}

