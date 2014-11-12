using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class kbrRoomScript : baseRoomScript {


	public GameObject hwRoom;
	public GameObject hwRoomDoor;
	public void Start() {
		roomMaps.Add(hwRoom, hwRoomDoor);
	}
}
