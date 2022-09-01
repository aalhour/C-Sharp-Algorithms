using Algorithms.Common;
using System;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public class DayofWeekTest
    {
        string expected;
        DateTime expected2, expected4;
        DateTime date2 = new DateTime(2022, 9, 1);


        [Fact]
        public void TestGetDayofWeek1()
        {
            string result = DayofWeek.GetDayofWeek(date2);
            expected = "1st Thu";
            Assert.True(expected == result);
        }
        [Fact]
        public void TestGetDayofWeek2()
        {
            string result = DayofWeek.GetDayofWeek(8, 9, 2022);
            expected = "2nd Thu";
            Assert.True(expected == result);
        }
        [Fact]
        public void TestcheckDay1()
        {
            Assert.True(DayofWeek.checkDay(date2, 1, 4));
        }
        [Fact]
        public void TestcheckDay2()
        {
            Assert.True(DayofWeek.checkDay(1, 9, 2022, 1, 4));
        }
        [Fact]
        public void TestGetDay1()
        {
            expected2 = new DateTime(2022, 9, 8);
            Assert.True(expected2 == DayofWeek.GetDay(9, 2022, 2, 4));
        }

        [Fact]
        public void TestGetDay2()
        {
            expected4 = new DateTime(2022, 9, 3);

            Assert.True(expected4 == DayofWeek.GetDay(2022, 36, 6));
        }
    }
}