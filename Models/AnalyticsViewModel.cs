using System;
using System.Collections.Generic;

namespace JewelleryVerificationProject.Models
{
    public class AnalyticsViewModel
    {
        public Dictionary<string, int> CertificateTypeCounts { get; set; } = new();

        public List<TrendData> WeeklyTrends { get; set; } = new();
        public List<TrendData> MonthlyTrends { get; set; } = new();
        public List<TrendData> SixMonthTrends { get; set; } = new();
        public List<TrendData> YearlyTrends { get; set; } = new();
    }

    public class TrendData
    {
        public string CertificateType { get; set; }
        public string PeriodLabel { get; set; }
        public int Count { get; set; }
    }
}
