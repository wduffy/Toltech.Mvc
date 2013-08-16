
namespace System
{

    public enum MonthName
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public static class DateTimeExtensions
    {

        ///<summary>
        ///Returns the name of a month from it's given numeric position in the calendar year
        ///</summary>
        ///<returns>A <see cref="DateTime.MonthName" /> which represents a month name</returns>
        ///<remarks></remarks>
        public static MonthName MonthName(this DateTime datetime)
        {
            return (MonthName)datetime.Month;
        }

        ///<summary>
        ///Returns the end of day datestamp represented by this instance
        ///</summary>
        ///<returns></returns>
        ///<remarks></remarks>
        public static DateTime EndOfDay(this DateTime datetime)
        {
            return datetime.Date.AddSeconds(86399);
        }

        ///<summary>
        ///Returns the ordinal suffix for the day of the month represented by this instance
        ///</summary>
        ///<returns></returns>
        public static string OrdinalSuffix(this DateTime datetime)
        {
            int day = datetime.Day;
            
            if (day % 100 >= 11 && day % 100 <= 13)
                return String.Concat(day, "th");

            switch (day % 10)
            {
                case 1:
                    return String.Concat(day, "st");
                case 2:
                    return String.Concat(day, "nd");
                case 3:
                    return String.Concat(day, "rd");
                default:
                    return String.Concat(day, "th");
            }
        }
                
    }
            
}