using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class kRoomScript : baseRoomScript {


	public GameObject fRoom;
	public GameObject fRoomDoor;
	public void Start() {
		roomMaps.Add(fRoom, fRoomDoor);
	}
}
