using System;
using System.Collections.Generic;
using System.Text;

namespace FindHousingProject.Common.Utils
{
    public struct Period
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsIntersectOrInclude(Period other) => (Start < other.End & End > other.End) || (End < other.Start & Start > other.End) || (Start >= other.Start & End >= other.End);
        public Period(DateTime start, DateTime end)
        {
            Start = start; // start > end ? start : end;
            End = end; // start > end ? end : start;
        }
        public static Period Create(DateTime start, DateTime end) => new Period(start, end);
    }
}
