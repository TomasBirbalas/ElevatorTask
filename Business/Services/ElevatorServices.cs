
using Repository;
using Repository.DataAccess;
using Repository.Models;
using System;
using System.Threading;

namespace Business
{
    public class ElevatorServices
    {
        //private static Elevator Elevator = new Elevator(1,1);

        private readonly IBuildingRepository _buildingRepository;
		Building currentBuilding;
		Elevator elevator;
		public ElevatorServices(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;

            currentBuilding = _buildingRepository.RetrieveBuilding();
            elevator = currentBuilding.Elevators[0];
        }
		public void Stop(int floor)
		{
			elevator.Status = ElevatorStatus.Stoped;
			elevator.CurrentFloor = floor;
			Console.WriteLine($"Stopped at floor {floor}");
		}
		public void MoveUp(int floor)
        {
			elevator.Status = ElevatorStatus.MovingUp;
			Console.WriteLine($"Going up to: {floor}");
            while (floor != elevator.CurrentFloor)
            {
				Console.WriteLine($"	Elevator is moving: Current floor {elevator.CurrentFloor}");
				Thread.Sleep(1000);
				elevator.CurrentFloor++;
			}
			OpenDoor();
			CloseDoor();
		}
		public void MoveDown(int floor)
		{
			elevator.Status = ElevatorStatus.MovingDown;
			Console.WriteLine($"Going down to: {floor}");
			while (floor != elevator.CurrentFloor)
			{
				Console.WriteLine($"	Elevator is moving: Current floor {elevator.CurrentFloor}");
				Thread.Sleep(1000);
				elevator.CurrentFloor--;
			}
			OpenDoor();
			CloseDoor();
		}
		private void OpenDoor()
		{
			elevator.ElevatorDoorStatus = DoorStatus.Opening;
			Console.WriteLine("Door opening");
			Thread.Sleep(1500);
			elevator.ElevatorDoorStatus = DoorStatus.Open;
			Console.WriteLine("Door is open");
			Thread.Sleep(500);
			elevator.Status = ElevatorStatus.Stoped;
		}
		private void CloseDoor()
		{
			elevator.ElevatorDoorStatus = DoorStatus.Closing;
			Console.WriteLine("Door closing");
			Thread.Sleep(1500);
			elevator.ElevatorDoorStatus = DoorStatus.Closed;
			Console.WriteLine("Door is closed");
			Thread.Sleep(500);
		}
	}
}
