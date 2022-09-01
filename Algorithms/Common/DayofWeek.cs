using System;

namespace Algorithms.Common
{
    public static class DayofWeek

    {
        /// <summary>
        /// Determine day of week of the date
        /// </summary>
        /// <returns>Return string of day of week of the date</returns>
        /// <param name="date">Date.</param>
        public static string GetDayofWeek(DateTime date)
        {

            int count = 1; //Int to shows Ordinal number of occurrence of day of week
            DateTime Occurrence = date.AddDays(-7); //Date to check the ordinal number of day of week 

            for (string month = date.ToString("MMMM"); month == Occurrence.ToString("MMMM"); Occurrence = Occurrence.AddDays(-7))
            {
                count++;
            }

            //switch case for count to return string of day of week of the date
            switch (count)
            {
                case 1:
                    return count + "st " + date.ToString("ddd");
                case 2:
                    return count + "nd " + date.ToString("ddd");
                case 3:
                    return count + "rd " + date.ToString("ddd");
                default:
                    return count + "th " + date.ToString("ddd");
            }

        }


        /// <summary>
        /// Determine whether the day of week  
        /// </summary>
        /// <returns>Return string of day of week of the date</returns>
        /// <param name="date">Date.</param>
        /// <param name="month">Month.</param>
        /// <param name="year">Year.</param>
        //overload
        public static string GetDayofWeek(int date, int month, int year)
        {
            DateTime date2 = new DateTime(year, month, date);
            return GetDayofWeek(date2);
        }

        /// <summary>
        /// Check whether the date is 1st/2nd/3rd.... day of week of the month
        /// </summary>
        /// <returns><c>true</c>if the date is (For Ex: 1st Monday of the month) <c>false</c>if the date is not (For Ex: 1st Monday of the month) </returns>
        /// <param name="date">Date.</param>
        /// <param name="occurrence">Ordinal number of day of week.</param>
        /// <param name="weekday">Day of Week.</param>
        public static bool checkDay(DateTime date, int occurrence, int weekday)
        {
            string compare = ""; //string to compare with the actual day of week from previous function
            int diff = (int)date.DayOfWeek - weekday; //get the difference between actual day of week of the date and the weekday parameter

            if (date.ToString("MMMM") == date.AddDays(diff).ToString("MMMM")) //check after the difference the date is still on the same month or not
            {
                switch (occurrence)
                {
                    case 1:
                        compare = occurrence + "st " + date.AddDays(diff).ToString("ddd");
                        break;
                    case 2:
                        compare = occurrence + "nd " + date.AddDays(diff).ToString("ddd");
                        break;
                    case 3:
                        compare = occurrence + "rd " + date.AddDays(diff).ToString("ddd");
                        break;
                    default:
                        compare = occurrence + "th " + date.AddDays(diff).ToString("ddd");
                        break;
                }
            }
            else
            {
                return false;
            }
            return compare == GetDayofWeek(date); //check whether compare is the same as GetDayofWeek(date) or not 
        }

        /// <summary>
        /// Check whether the date is 1st/2nd/3rd.... day of week of the month
        /// </summary>
        /// <returns><c>true</c>if the date is (For Ex: 1st Monday of the month) <c>false</c>if the date is not (For Ex: 1st Monday of the month) </returns>
        /// <param name="date">Date.</param>
        /// <param name="month">Month.</param>
        /// <param name="year">Year.</param>
        /// <param name="occurrence">Ordinal number of day of week.</param>
        /// <param name="weekday">Day of Week.</param>
        //overload
        public static bool checkDay(int date, int month, int year, int occurrence, int weekday)
        {
            DateTime date2 = new DateTime(year, month, date);
            return checkDay(date2, occurrence, weekday);
        }

        /// <summary>
        /// Get the 1st/2nd/3rd.... day of week of the month
        /// </summary>
        /// <returns>DateTime of 1st/2nd/3rd.... day of week of the month</returns>
        /// <param name="month">Month.</param>
        /// <param name="year">Year.</param>
        /// <param name="occurrence">Ordinal number of day of week.</param>
        /// <param name="weekday">Day of Week.</param>
        public static DateTime GetDay(int month, int year, int occurrence, int weekday)
        {
            DateTime date2 = new DateTime(year, month, 1); //1st of the month inputed
            int week = 7; //number of days in a week
            int day = (int)date2.DayOfWeek; //day of week of 1st of month

            //loop to get first wanted day of week in the month
            while (weekday != day)
            {
                date2 = date2.AddDays(1);
                day++;
                if (day > 6)
                {
                    day = 0;
                }
            }
            //add the number of days with the multiplication of occurence - 1
            if (occurrence != 1)
            {
                int add_days = week * (occurrence - 1);
                date2 = date2.AddDays(add_days);
            }

            return date2;
        }

        /// <summary>
        /// Get the 1st/2nd/3rd.... day of week of the year
        /// </summary>
        /// <returns>DateTime of 1st/2nd/3rd.... day of week of the year</returns>
        /// <param name="month">Month.</param>
        /// <param name="year">Year.</param>
        /// <param name="occurrence">Ordinal number of day of week.</param>
        /// <param name="weekday">Day of Week.</param>
        //overload
        public static DateTime GetDay(int year, int occurrence, int weekday)
        {
            return GetDay(1, year, occurrence, weekday);
        }

    }

}