
using Repository;
using Repository.DataAccess;
using Repository.Models;
using System;

namespace Business
{
    public class ElevatorServices
    {
		Elevator Elevator { get; }
		BuildingRepository BuildingRepository { get; }

		Building Building;

		private void ElevatorCall( int requestElevator, int elevatorId)
        {
			Building = BuildingRepository.RetrieveBuilding();

			if (requestElevator > Building.Floors || requestElevator < 1) throw new Exception("Calling position is not valid");

			bool isElevatorExist = Building.Elevators.Exists(elevator => elevator.Id == elevatorId);
			if (isElevatorExist)
			{
				int elevatorPosition = Building.Elevators[elevatorId].CurrentFloor;
				int elevatorStatus = (int)Building.Elevators[elevatorId].Status;
				int elevatorDoorStatus = (int)Building.Elevators[elevatorId].ElevatorDoorStatus;
			}
		}
		private void Stop(int floor)
		{
			Elevator.Status = ElevatorStatus.Stoped;
			Elevator.CurrentFloor = floor;
			Console.WriteLine($"Stopped at floor {floor}");
		}

		private void MoveUp(int floor)
        {
			Elevator.Status = ElevatorStatus.MovingUp;
			Console.WriteLine($"Going up to: {floor}");
            while (floor != Elevator.CurrentFloor)
            {
				Elevator.CurrentFloor++;
			}
			OpenDoor();
			CloseDoor();
		}
		private void MoveDown(int floor)
		{
			Elevator.Status = ElevatorStatus.MovingDown;
			Console.WriteLine($"Going up to: {floor}");
			while (floor != Elevator.CurrentFloor)
			{
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
