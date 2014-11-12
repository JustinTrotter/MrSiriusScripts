using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mBRScript : baseRoomScript {

	public GameObject hallway;
	public GameObject hallwayDoor;


	// Use this for initialization
	void Start () {
		roomMaps.Add(hallway, hallwayDoor);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
