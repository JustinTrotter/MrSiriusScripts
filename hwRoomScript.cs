using UnityEngine;
using System.Collections;
using System.Collections.Generic;	

public class hwRoomScript : baseRoomScript {

	public GameObject masterBedRoom;
	public GameObject masterBedRoomDoor;

	public GameObject kidBedRoom;
	public GameObject kidBedRoomDoor;

	public GameObject bathRoom;
	public GameObject bathRoomDoor;

	public GameObject foyer;
	public GameObject foyerDoor;

	// Use this for initialization
	void Start () {
		roomMaps.Add(masterBedRoom, masterBedRoomDoor);
		roomMaps.Add(kidBedRoom, kidBedRoomDoor);
		roomMaps.Add(bathRoom, bathRoomDoor);
		roomMaps.Add(foyer, foyerDoor);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
