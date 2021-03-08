using System;
using Xunit;
using Xunit.Abstractions;
using domain.NOAAStationAggregate;
using domain.VatsimMETARAggregate;

namespace tests
{
    public class WebApiUnitTests
    {

        //https://xunit.net/docs/capturing-output
        private readonly ITestOutputHelper outputHelper;


        // runner: dotnet test -l "console;verbosity=detailed"

        //dependency injection
        public WebApiUnitTests(ITestOutputHelper outputHelper) {
            this.outputHelper = outputHelper;
        }

        [Fact]
        public void TestCanGetObservationDateFromRaw()
        {
            //Arrange
            // test metar
            string testMetar = @"PHNL 301653Z 36004KT 10SM FEW025 21/18 A3000 RMK AO2 SLP157 T02060178";
            
            DateTime example = new DateTime(DateTime.UtcNow.Year,   //year
                                            DateTime.UtcNow.Month,  //month
                                            30,                     //day
                                            16,                     //hour utc
                                            53,                     //minute utc
                                            0);                     //second utc
            
            outputHelper.WriteLine($"testing: {example}");

            //Act
            DateTime obs = VatsimMETARHelper.ParseObservationTimeFromString(testMetar);

            //Assert
            Assert.Equal(example, obs);
        }

        [Fact]
        public void TestDayParsesSuccessfullyFromMETARTimeStamp() {

            //Arrange
            //test timestamp
            string testTimeStamp = "301653Z";

            //Act
            string day = VatsimMETARHelper.ParseDayFromMETARZuluTimeStamp(testTimeStamp);

            //Assert
            Assert.Equal("30", day);

        }

        [Fact]
        public void TestHourParsesSuccessfullyFromMETARTimeStamp() {

            //Arrange
            //test timestamp
            string testTimeStamp = "301653Z";

            //Act
            string hour = VatsimMETARHelper.ParseHourFromMETARZuluTimeStamp(testTimeStamp);

            //Assert
            Assert.Equal("16", hour);

        }

        [Fact]
        public void TestMinuteParsesSuccessfullyFromMETARTimeStamp() {

            //Arrange
            //test timestamp
            string testTimeStamp = "301653Z";

            //Act
            string minute = VatsimMETARHelper.ParseMinuteFromMETARZuluTimeStamp(testTimeStamp);

            //Assert
            Assert.Equal("53", minute);

        }

    }
}
