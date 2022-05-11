using Business.Services;
using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestNearestElevatorCall()
        {
            List<Elevator> elevatorsList = new List<Elevator>{
                new Elevator(1, 10),
                new Elevator(2, 3),
                new Elevator(3, 8)
            };
            Building building = new Building(12, elevatorsList);
            ElevatorManager elevatorManager = new ElevatorManager();
            int elevatorExpectedElevatorId = 2;
            int elevatorIdWhichComes = elevatorManager.GetClosesedElevator(building, 4);

            Assert.Equal(elevatorExpectedElevatorId, elevatorIdWhichComes);
        }
    }
}
