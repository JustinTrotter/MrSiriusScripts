using UnityEngine;
using System.Collections;

public class blockerClownScript : baseClownScript {

	public bool wandering;
	public float timer;
	public int health;
	public int hitByPlayerDamage;
	public float doorTimer;
	public float hitTimer;
	public GameObject prevDoor;
	public GameObject gameLogicGO;
	// Use this for initialization
	void Start () {
		base.Start();
		gameLogicGO = GameObject.FindGameObjectWithTag("GameLogic");
		wandering = false;
		pathing = false;
		travelling = false;
		timer = 0;
		health = 2;
		hitByPlayerDamage = 1;
		hitTimer = 0;
		doorTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();

	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Shield" && Time.time - hitTimer > 0.4286f)
		{
			player.GetComponent<playerScript>().messiness += player.GetComponent<playerScript>().hurtDamage; 	
			health -= hitByPlayerDamage;
			hitTimer = Time.time;
			//Destroy(gameObject);
		}
	}

	void FixedUpdate()
	{
		base.FixedUpdate();
		if (health <= 0)
		{
			Destroy(gameObject);
			return;
		}

		if (player.GetComponent<playerScript>().gameLogicGO.GetComponent<gameLogic>().naked)
		{
			wandering = true;
			gameObject.layer = 16;
		}

		else
		{
			wandering = false;
		}

		if (player.GetComponent<playerScript>().gameLogicGO.GetComponent<gameLogic>().aggression)
		{
			if (!travelling && !wandering && currentRoom != playerComp.currentRoom)
			{
				pathing = true;
				gameObject.layer = 15;
			}
			else if (currentRoom == playerComp.currentRoom)
			{
				pathing = false;
			}
		}

		else {

			pathing = false;
			wandering = true;
		}


//		if (player.GetComponent<playerScript>().currentRoom != path[path.Count -1])
//		{
//			getPath();
//
//		}

		if (currentRoom != playerComp.currentRoom)
		{
			if (path.Count <= 0)
				getPath ();
			else if (path.Count > 0 && playerComp.currentRoom != path[path.Count - 1])
				getPath ();
		}

//		if (player.GetComponent<playerScript>().gameLogicGO.GetComponent<gameLogic>().naked) {
//			//wandering = true;
//		}
//		else 
//			wandering = false;

		if (pathing) {
			rigidbody.isKinematic = true;
			transform.position = Vector3.MoveTowards(transform.position, targetDoor.transform.position, moveSpeed * Time.deltaTime);
			float distance = Vector3.Distance(transform.position, targetDoor.transform.position);
			if (distance < .5f)
			{
				//rigidbody.isKinematic = true;
				pathing = false;
				travelling = true;
				travelTargetDoor = targetDoor.GetComponent<doorTriggerScript>().targetDoor;
			}
		}

		else if(travelling)
		{
			transform.position = Vector3.MoveTowards(transform.position,travelTargetDoor.transform.position, moveSpeed * Time.deltaTime);
			if(transform.position == travelTargetDoor.transform.position)
			{
				travelling = false;
				rigidbody.isKinematic = false;
				nextRoomIndex++;
				currentRoom = travelTargetDoor.GetComponent<doorTriggerScript>().ownerRoom;
				if (nextRoomIndex < path.Count) {
					Debug.Log (path[nextRoomIndex]);
					//currentRoom = travelTargetDoor.GetComponent<doorTriggerScript>().ownerRoom;
					targetDoor = currentRoom.GetComponent<baseRoomScript>().roomMaps[path[nextRoomIndex]];
					pathing = true;
					gameObject.layer = 15;
				}
			}
		}
		else {
			rigidbody.isKinematic = false;
			if (!wandering)
				transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
			else
			{
				if (Time.time - timer > 2f)
				{
					timer = Time.time;
					velocity = new Vector3(Random.Range(-1f, 1f),Random.Range (-1f, 1f), 0);
					velocity.Normalize();

				}
				transform.Translate(velocity * moveSpeed * Time.deltaTime);
			}

		}

	}
}
