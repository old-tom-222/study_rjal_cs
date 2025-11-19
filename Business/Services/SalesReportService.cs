using System;
using System.Collections.Generic;
using System.Linq;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class SalesReportService
    {
        private readonly InventoryTransactionRepository _transactionRepo;
        private readonly SalesReportRepository _reportRepo;

        public SalesReportService()
        {
            _transactionRepo = new InventoryTransactionRepository();
            _reportRepo = new SalesReportRepository();
        }

        /// <summary>
        /// 获取产品销售报表
        /// </summary>
        public List<SalesReportModel> GetProductSalesReport(DateTime startDate, DateTime endDate)
        {
            // 直接调用Repository层的方法获取产品销售报表数据
            return _reportRepo.GetProductSalesReport(startDate, endDate);
        }

        /// <summary>
        /// 获取每日销售报表
        /// </summary>
        public List<DailySalesReportModel> GetDailySalesReport(DateTime startDate, DateTime endDate)
        {
            var reportItems = new List<DailySalesReportModel>();
            
            // 实际应用中需要根据销售订单数据生成
            // 这里模拟生成一些数据用于演示
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // 随机生成一些测试数据
                int totalOrders = new Random(date.Day).Next(10, 50);
                int totalItemsSold = totalOrders * new Random(date.Day + 10).Next(1, 5);
                decimal averageOrderValue = new Random(date.Day + 20).Next(100, 500);
                decimal totalRevenue = totalOrders * averageOrderValue;

                reportItems.Add(new DailySalesReportModel
                {
                    Date = date,
                    TotalOrders = totalOrders,
                    TotalItemsSold = totalItemsSold,
                    TotalRevenue = totalRevenue,
                    AverageOrderValue = averageOrderValue
                });
            }

            return reportItems;
        }

        /// <summary>
        /// 获取销售趋势报表
        /// </summary>
        public List<MonthlyTrendModel> GetSalesTrendReport(DateTime startDate, DateTime endDate, int granularity = 2) // 0=日, 1=周, 2=月
        {
            // 调用Repository获取真实的销售趋势数据
            return _reportRepo.GetSalesTrendReport(startDate, endDate, granularity);
        }

        /// <summary>
        /// 获取客户销售排名
        /// </summary>
        public List<CustomerRankingModel> GetCustomerRankings(DateTime startDate, DateTime endDate, int topN = 10)
        {
            return _reportRepo.GetCustomerRankings(startDate, endDate, topN);
        }

        /// <summary>
        /// 获取产品销售排名
        /// </summary>
        public List<ProductRankingModel> GetProductRankings(DateTime startDate, DateTime endDate, int topN = 10)
        {
            return _reportRepo.GetProductRankings(startDate, endDate, topN);
        }

        /// <summary>
        /// 获取月度销售数据
        /// </summary>
        public List<MonthlySalesData> GetMonthlySalesData(int year)
        {
            return _reportRepo.GetMonthlySalesData(year);
        }

        /// <summary>
        /// 获取销售统计数据
        /// </summary>
        public SalesReportModel GetSalesSummary(DateTime startDate, DateTime endDate)
        {
            var summary = new SalesReportModel
            {
                StartDate = startDate,
                EndDate = endDate
            };

            // 可以在这里添加更多的统计逻辑
            // 目前使用现有的Model类，后续可以创建专门的统计Model
            
            return summary;
        }

        /// <summary>
        /// 获取当前年份的销售报表
        /// </summary>
        public List<MonthlySalesData> GetCurrentYearSales()
        {
            return GetMonthlySalesData(DateTime.Now.Year);
        }

        /// <summary>
        /// 获取最近N天的销售统计
        /// </summary>
        public SalesReportModel GetRecentSales(int days = 30)
        {
            var endDate = DateTime.Now;
            var startDate = endDate.AddDays(-days);
            return GetSalesSummary(startDate, endDate);
        }
    }
}