
using Repository;
using Repository.DataAccess;
using Repository.Models;
using System;
using System.Threading;

namespace Business
{
    public class ElevatorServices
    {
		public void Stop(int floor, ref Elevator elevator)
		{
			elevator.Status = ElevatorStatus.Stoped;
			elevator.CurrentFloor = floor;
			Console.WriteLine($"Stopped at floor {floor}");
		}
		public void MoveUp(int floor, ref Elevator elevator)
        {
			elevator.Status = ElevatorStatus.MovingUp;
			Console.WriteLine($"Going up to: {floor}");
            while (floor != elevator.CurrentFloor)
            {
				Console.WriteLine($"	Elevator is moving: Current floor {elevator.CurrentFloor}");
				Thread.Sleep(1000);
				elevator.CurrentFloor++;
			}
			Stop(floor, ref elevator);
			OpenDoor(ref elevator);
			CloseDoor(ref elevator);
		}
		public void MoveDown(int floor, ref Elevator elevator)
		{
			elevator.Status = ElevatorStatus.MovingDown;
			Console.WriteLine($"Going down to: {floor}");
			while (floor != elevator.CurrentFloor)
			{
				Console.WriteLine($"	Elevator is moving: Current floor {elevator.CurrentFloor}");
				Thread.Sleep(1000);
				elevator.CurrentFloor--;
			}
			Stop(floor, ref elevator);
			OpenDoor(ref elevator);
			CloseDoor(ref elevator);
		}
		private void OpenDoor(ref Elevator elevator)
		{
			elevator.ElevatorDoorStatus = DoorStatus.Opening;
			Console.WriteLine("Door opening");
			Thread.Sleep(1500);
			elevator.ElevatorDoorStatus = DoorStatus.Open;
			Console.WriteLine("Door is open");
			Thread.Sleep(500);
		}
		private void CloseDoor(ref Elevator elevator)
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
