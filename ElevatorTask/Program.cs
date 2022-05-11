using Business.Services;
using Repository;
using Repository.DataAccess;
using System;

namespace ElevatorTask
{
    internal class Program
    {
        private const string quit = "q";
        static void Main(string[] args)
        {
            Console.WriteLine("Please create building");

            Console.WriteLine("Please enter the building floors");
            int buildingFloors = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the required edlevators number");
            int elevatorsCount = Convert.ToInt32(Console.ReadLine());

            BuildingRepository building = new BuildingRepository(buildingFloors, elevatorsCount);
            Console.WriteLine($"Building successfully created: Building has {buildingFloors} floors and {elevatorsCount} elevators");
            Building currentBuilding = building.RetrieveBuilding();

            var manager = new ElevatorManager();
            string input = "";
            while (input != quit)
            {
                Console.WriteLine("Enter Floor:");
                input = Console.ReadLine();
                int floor;
                if (int.TryParse(input, out floor))
                    manager.ElevatorCall(currentBuilding, floor, 1);
                else if (input == quit)
                    Console.WriteLine("GoodBye!");
                else
                    Console.WriteLine("You have pressed an incorrect floor, Please try again");
            }
        }
    }
}
