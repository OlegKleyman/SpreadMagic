using System;

namespace SpreadMagic.Core
{
    public class DateProvider : IDateProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}