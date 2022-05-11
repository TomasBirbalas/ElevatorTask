
using Repository;
using Repository.DataAccess;
using Repository.Models;
using System;
using System.Threading;

namespace Business
{
    public class ElevatorServices
    {
		Elevator Elevator = new Elevator(1, 1);
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
			Console.WriteLine($"Going up to: {floor}");
			Elevator.CurrentFloor = floor;
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
			Elevator.ElevatorDoorStatus = DoorStatus.Open;
			Console.WriteLine("Door is open");
		}
		private void CloseDoor()
		{
			Elevator.ElevatorDoorStatus = DoorStatus.Closing;
			Console.WriteLine("Door closing");
			Elevator.ElevatorDoorStatus = DoorStatus.Closed;
			Console.WriteLine("Door is closed");
		}
	}
}
