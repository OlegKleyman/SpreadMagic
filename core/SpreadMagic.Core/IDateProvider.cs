using System;

namespace SpreadMagic.Core
{
    public interface IDateProvider
    {
        DateTime UtcNow { get; }
    }
}