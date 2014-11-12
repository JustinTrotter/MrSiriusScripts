using UnityEngine;
using System.Collections;

public class clownSpawnerScript : MonoBehaviour {

	public Transform prefab;
	public Transform prefabClone;
	public GameObject startingRoom;
	public float spawnSpeed;

	// Use this for initialization
	void Start () {
		//spawnSpeed = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<gameLogic>().spawnSpeed;
		InvokeRepeating("spawnClown", 1,spawnSpeed);
		if (startingRoom == null)
			Debug.LogError("SET UP A ROOM IDIOT!");
	}
	
	// Update is called once per frame
	void Update () {


		//StartCoroutine(waitSeconds(1f));

	}

	void spawnClown()
	{
		prefabClone = (Transform)Instantiate (prefab, transform.position, Quaternion.identity);
		prefabClone.GetComponent<baseClownScript>().currentRoom = startingRoom;
	}

	IEnumerator waitSeconds (float seconds)
	{
		yield return new WaitForSeconds(seconds);


	}
}
