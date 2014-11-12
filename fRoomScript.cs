using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class fRoomScript : baseRoomScript {


	public GameObject hwRoom;
	public GameObject hwRoomDoor;

	public GameObject kitchenRoom;
	public GameObject kitchenRoomDoor;

	public GameObject lrRoom;
	public GameObject lrRoomDoor;
	public void Start() {
		roomMaps.Add(hwRoom, hwRoomDoor);
		roomMaps.Add(kitchenRoom, kitchenRoomDoor);
		roomMaps.Add(lrRoom, lrRoomDoor);
	}
}
