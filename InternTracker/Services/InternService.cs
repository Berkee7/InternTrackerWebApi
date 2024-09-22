using System;
using System.Collections.Generic;

namespace InternTracker.Services
{
    public class InternService
    {
        public DateTime SetAgain(DateTime startDate, DateTime endDate, List<DateTime> holidays)
        {
            DateTime adjustedEndDate = endDate;
            var counter = 0;
            DateTime currentDate = startDate.AddDays(1);

            while (currentDate <= adjustedEndDate)
            {
                if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday || holidays.Contains(currentDate.Date))
                {
                    adjustedEndDate = adjustedEndDate.AddDays(1);
                }
                if (currentDate.DayOfWeek == DayOfWeek.Tuesday || currentDate.DayOfWeek == DayOfWeek.Friday)
                {
                    counter++;
                }

                currentDate = currentDate.AddDays(1);
            }

            Console.WriteLine("HomeOffice Gün Sayısı: " + counter);
            return adjustedEndDate;
        }
    }
}

