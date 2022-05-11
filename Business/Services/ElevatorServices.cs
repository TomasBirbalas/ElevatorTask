
using Repository;
using Repository.DataAccess;
using Repository.Models;
using System;
using System.Threading;

namespace Business
{
    public class ElevatorServices
    {
		private static Elevator Elevator = new Elevator(1,1);

		public void Stop(int floor)
		{
			Elevator.Status = ElevatorStatus.Stoped;
			Elevator.CurrentFloor = floor;
			Console.WriteLine($"Stopped at floor {floor}");
		}
		public void MoveUp(int floor)
        {
			Elevator.Status = ElevatorStatus.MovingUp;
			Console.WriteLine($"Going up to: {floor}");
            while (floor != Elevator.CurrentFloor)
            {
				Console.WriteLine($"	Elevator is moving: Current floor {Elevator.CurrentFloor}");
				Thread.Sleep(1000);
				Elevator.CurrentFloor++;
			}
			OpenDoor();
			CloseDoor();
		}
		public void MoveDown(int floor)
		{
			Elevator.Status = ElevatorStatus.MovingDown;
			Console.WriteLine($"Going up down: {floor}");
			while (floor != Elevator.CurrentFloor)
			{
				Console.WriteLine($"	Elevator is moving: Current floor {Elevator.CurrentFloor}");
				Thread.Sleep(1000);
				Elevator.CurrentFloor--;
			}
			OpenDoor();
			CloseDoor();
		}
		private void OpenDoor()
		{
			Elevator.ElevatorDoorStatus = DoorStatus.Opening;
			Console.WriteLine("Door opening");
			Thread.Sleep(1500);
			Elevator.ElevatorDoorStatus = DoorStatus.Open;
			Console.WriteLine("Door is open");
			Thread.Sleep(500);
		}
		private void CloseDoor()
		{
			Elevator.ElevatorDoorStatus = DoorStatus.Closing;
			Console.WriteLine("Door closing");
			Thread.Sleep(1500);
			Elevator.ElevatorDoorStatus = DoorStatus.Closed;
			Console.WriteLine("Door is closed");
			Thread.Sleep(500);
		}
	}
}
