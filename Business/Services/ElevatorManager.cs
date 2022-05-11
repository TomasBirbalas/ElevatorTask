﻿using Repository;
using Repository.DataAccess;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Services
{
    public class ElevatorManager
    {
		private ElevatorServices elevatorServices = new ElevatorServices();
		private Queue<RequestsOfElevator> downRequests = new Queue<RequestsOfElevator>();
		private Queue<RequestsOfElevator> upRequests = new Queue<RequestsOfElevator>();

		public void ElevatorCall(Building currentBuilding, int requestElevator, int elevatorId)
		{
			if (requestElevator > currentBuilding.Floors || requestElevator < 1) throw new Exception("Calling position is not valid");

			bool isElevatorExist = currentBuilding.Elevators.Exists(elevator => elevator.Id == elevatorId);
			if (isElevatorExist)
			{
				Elevator elevator = currentBuilding.Elevators.Find(elevator => elevator.Id == elevatorId);
				if (requestElevator > elevator.CurrentFloor)
				{
					upRequests.Enqueue(new RequestsOfElevator(requestElevator));
                }
                else
                {
					downRequests.Enqueue(new RequestsOfElevator(requestElevator));
                }
				Move(currentBuilding, elevatorId, requestElevator);
			}
		}
		public void Move(Building currentBuilding, int elevatorId, int floorRequest)
		{
			Elevator currentElevator = currentBuilding.Elevators.Find(elevator => elevator.Id == elevatorId);
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
                    }else if(floorRequest <= currentElevator.CurrentFloor)
                    {
						currentElevator.Status = ElevatorStatus.MovingDown;
                    }
					Move(currentBuilding, currentElevator.Id, floorRequest);
					break;
				default:
					break;
			}
		}
		public int GetClosesedElevator(Building currentBuilding, int floorRequest)
        {
			Elevator elevator = currentBuilding.Elevators.OrderBy(elevator => Math.Abs(elevator.CurrentFloor - floorRequest)).First();
			return elevator.Id;
        }
	}
}
