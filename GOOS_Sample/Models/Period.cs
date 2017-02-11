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

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public string StartMonthString => this._startDate.ToString("yyyy-MM");

        public string EndMonthString => this._endDate.ToString("yyyy-MM");
    }
}