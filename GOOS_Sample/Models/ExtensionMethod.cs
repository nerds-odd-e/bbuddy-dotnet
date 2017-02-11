using GOOS_Sample.Models.DataModels;
using System;

namespace GOOS_Sample.Models
{
    public static class ExtensionMethod
    {
        public static decimal GetOverlappingAmount(this Budget budget, Period period)
        {
            var dailyAmount = budget.DailyAmount();

            int overlappigDays = budget.GetOverlappingDays(period);

            return dailyAmount * overlappigDays;
        }

        private static decimal DailyAmount(this Budget budget)
        {
            var days = DaysInMonth(budget.YearMonth);

            return budget.Amount / days;
        }

        private static int DaysInMonth(string yearMonth)
        {
            return DateTime.DaysInMonth(Convert.ToInt16(yearMonth.Split('-')[0]),
                Convert.ToInt16(yearMonth.Split('-')[1]));
        }

        private static DateTime FirstDay(this string yearMonth)
        {
            return DateTime.Parse($"{yearMonth}-01");
        }

        private static DateTime GetEndDateBoundary(Budget budget, Period period)
        {
            var lastDay = budget.YearMonth.LastDay();

            return period.EndDate > lastDay ? lastDay : period.EndDate;
        }

        private static int GetOverlappingDays(this Budget budget, Period period)
        {
            var endDateBoundary = GetEndDateBoundary(budget, period);

            var startDateBoundary = GetStartBoundary(budget, period);

            return new TimeSpan(endDateBoundary.AddDays(1).Ticks - startDateBoundary.Ticks).Days;
        }

        private static DateTime GetStartBoundary(Budget budget, Period period)
        {
            var firstDay = budget.YearMonth.FirstDay();

            return period.StartDate < firstDay ? firstDay : period.StartDate;
        }

        private static DateTime LastDay(this string yearMonth)
        {
            var daysInMonth = DaysInMonth(yearMonth);

            return DateTime.Parse($"{yearMonth}-{daysInMonth}");
        }
    }
}