using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class baseClownScript : MonoBehaviour {
	
	public float moveSpeed;
	public GameObject player;
	public playerScript playerComp;
	public Transform graphic;
	public Vector3 velocity;
	public int directionFace;

	public int nextRoomIndex;
	public GameObject targetDoor;
	public GameObject travelTargetDoor;
	public GameObject currentRoom;
	public List<GameObject> path;
	public GameObject test;
	public bool pathing = false;
	public bool travelling = false;

	// Use this for initialization
	protected void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		playerComp = player.GetComponent<playerScript>();
		graphic = transform.GetChild(0);
		if (currentRoom != playerComp.currentRoom) {
			getPath();
			//pathing = true;
		}
//		foreach (GameObject i in path) {
//			Debug.Log(i);
//		}
	}
	
	// Update is called once per frame
	protected void Update () {
//		if (!travelling && currentRoom != playerComp.currentRoom) {
//			pathing = true;
//		}
//
//		if (currentRoom == playerComp.currentRoom) {
//			pathing = false;
//		}
//		else if (path[path.Count - 1] != playerComp.currentRoom)
//		{
//			//Debug.Log("Path changed!");
//			//getPath();
//		}

		if (player.transform.position.x > transform.position.x)
			directionFace = -1;
		else
			directionFace = 1;

		graphic.transform.localScale = new Vector3(Mathf.Abs(graphic.transform.localScale.x) * directionFace, graphic.transform.localScale.y, graphic.transform.localScale.z); 
		graphic.GetComponent<SpriteRenderer>().renderer.sortingOrder = (int)(transform.position.y * 100 * -1);
	}
	
	protected void FixedUpdate(){


	}
		
	public void getPath(){
		path = new List<GameObject>(7);
		path.Add(currentRoom);

//		for (int i = 0; i < path.Count; i++) {
//			GameObject room = (GameObject)path[i];
//			if(room == player.GetComponent<playerScript>().currentRoom)
//				break;
//
//			foreach (GameObject room2 in room.GetComponent<baseRoomScript>().roomMaps.Keys) {
//				if (room2 == player.GetComponent<playerScript>().currentRoom)
//				{
//					path.Add (room2);
//				}
//
//				else if(!path.Contains(room2) && room2.GetComponent<baseRoomScript>().roomMaps.Count > 1)
//				{
//					path.Add (room2);
//				}
//			}
//		}

		if (currentRoom.GetComponent<baseRoomScript>().roomMaps.ContainsKey(playerComp.currentRoom)) {
			path.Add(playerComp.currentRoom);
		}
		else {
			for (int i = 0; i < path.Count; i++) {
				int n = 0;
				GameObject room = (GameObject)path[i];
				foreach (GameObject r in room.GetComponent<baseRoomScript>().roomMaps.Keys) {
					if (r == playerComp.currentRoom) {
						path.Add(r);
					}
					else if (r.GetComponent<baseRoomScript>().roomMaps.Count > 1 && !path.Contains(r)) {
						path.Add(r);
					}
					else {
						n++;
					}
				}

//				Debug.Log(n);
				if (n == room.GetComponent<baseRoomScript>().roomMaps.Count && room != playerComp.currentRoom) {
					path.Remove(room);
				}
			}
		}

		nextRoomIndex = 1;
		targetDoor = currentRoom.GetComponent<baseRoomScript>().roomMaps[path[nextRoomIndex]];
	}
}
