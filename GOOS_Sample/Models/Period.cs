using System;

namespace GOOS_Sample.Models
{
    public class Period
    {
        private DateTime _startDate;
        private DateTime _endDate;

        public Period(DateTime startDate, DateTime endDate)
        {
            this._startDate = startDate;
            this._endDate = endDate;
        }
    }
}