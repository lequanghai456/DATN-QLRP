using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLiRapPhim.Areas.Admin.Controllers
{
    public class CustomHireDate : ValidationAttribute
    {
        public readonly DateTime StartTime;

        public CustomHireDate(DateTime startTime)
        {
            StartTime = startTime;
        }

        public override bool IsValid(object value)
        {
            if (StartTime.AddMinutes((int)value).CompareTo(DateTime.Parse("24:00:00"))>0)
                return true;
            return false;
        }
    }
}