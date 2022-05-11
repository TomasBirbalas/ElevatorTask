
using System.Collections.Generic;

namespace Repository.Models
{
    public class Elevator
    {
        public int Id { get; }
        public int CurrentFloor { get; set; }
        public Queue<int> Requests { get; set; }
        public ElevatorStatus Status { get; set; }
        public DoorStatus ElevatorDoorStatus { get; set; }

        public Elevator(int id, int currentFloor)
        {
            Id = id;
            CurrentFloor = currentFloor;

            Requests = new Queue<int>();
            Status = ElevatorStatus.Stoped;
            ElevatorDoorStatus = DoorStatus.Closed;
        }
    }
}
