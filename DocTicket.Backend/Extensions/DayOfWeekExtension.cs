namespace DocTicket.Backend.Extensions
{
    public static class DayOfWeekExtension
    {
        public static DateTime ToDateTime(this DayOfWeek dayOfWeek)
        {
            DateTime desiredDateTime;
            DateTime currentDate = DateTime.Now;

            if (currentDate.DayOfWeek < dayOfWeek)
            {
                desiredDateTime = currentDate.AddDays(dayOfWeek - currentDate.DayOfWeek);
            }
            else if (currentDate.DayOfWeek > dayOfWeek)
            {
                desiredDateTime = currentDate.AddDays(7 - (int)currentDate.DayOfWeek + (int)dayOfWeek);
            }
            else
            {
                desiredDateTime = currentDate;
            }

            return desiredDateTime;
        }
    }
}
