using Repository;
using Repository.DataAccess;
using Repository.Models;
using System;
using System.Collections.Generic;

namespace Business.Services
{
    public class ElevatorManager
    {
		private BuildingRepository building = new BuildingRepository(10, 10);

		private ElevatorServices elevatorServices = new ElevatorServices();
		private Queue<RequestsOfElevator> downRequests = new Queue<RequestsOfElevator>();
		private Queue<RequestsOfElevator> upRequests = new Queue<RequestsOfElevator>();

		public void ElevatorCall(Building currentBuilding, int requestElevator, int elevatorId)
		{

			if (requestElevator > currentBuilding.Floors || requestElevator < 1) throw new Exception("Calling position is not valid");

			bool isElevatorExist = currentBuilding.Elevators.Exists(elevator => elevator.Id == elevatorId);
			if (isElevatorExist)
			{
				Elevator elevator = currentBuilding.Elevators[elevatorId];
				if (requestElevator > elevator.CurrentFloor)
				{
					upRequests.Enqueue(new RequestsOfElevator(requestElevator));
                }
                else
                {
					downRequests.Enqueue(new RequestsOfElevator(requestElevator));
                }
				Move(elevatorId, requestElevator);
			}
		}
		public void Move(int elevatorId, int floorRequest)
		{
			Elevator currentElevator = building.RetrieveElevatorById(elevatorId);
			switch (currentElevator.Status)
			{
				case ElevatorStatus.MovingDown:
					while (downRequests.Count > 0)
                    {
						elevatorServices.MoveDown(downRequests.Dequeue().RequestToFloor);
                    }
					currentElevator.Status = ElevatorStatus.Stoped;
					break;
				case ElevatorStatus.MovingUp:
					while (upRequests.Count > 0)
                    {
						elevatorServices.MoveUp(upRequests.Dequeue().RequestToFloor);
                    }
					currentElevator.Status = ElevatorStatus.Stoped;
					break;
				case ElevatorStatus.Stoped:
                    if(floorRequest > currentElevator.CurrentFloor)
					{
						currentElevator.Status = ElevatorStatus.MovingUp;
                    }else
                    {
						currentElevator.Status = ElevatorStatus.MovingDown;
                    }
					Move(currentElevator.Id, floorRequest);
					break;
				default:
					break;
			}
		}
	}
}
