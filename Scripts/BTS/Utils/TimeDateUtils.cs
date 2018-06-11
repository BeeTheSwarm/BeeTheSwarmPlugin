using UnityEngine;
using System.Collections;
using System;

public static class TimeDateUtils {
    private static readonly DateTime EPOCH_START = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static int CurrentTimeStamp { get; internal set; }

    public static UInt64 GetUTCOffset() {
        DateTime time = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
        TimeSpan offset = TimeZoneInfo.Utc.GetUtcOffset(time);
        UInt64 off = (UInt64)offset.TotalSeconds;
        return off;
    }

    public static DateTime FromUnixTime(long unixTime) {
        return EPOCH_START.AddSeconds(unixTime);
    }

    public static int GetDaysDifference(DateTime date) {
        DateTime dateMidnight = new DateTime(date.Year, date.Month, date.Day);
        DateTime now = DateTime.Now;
        DateTime todayMidnight = new DateTime(now.Year, now.Month, now.Day);
        return (todayMidnight - dateMidnight).Days;
    }
}
