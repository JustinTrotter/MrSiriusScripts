using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour {

	public GameObject masterBedRoom;
	public GameObject kidBedRoom;
	public GameObject hallway;
	public GameObject bathRoom;
	public GameObject foyer;
	public GameObject kitchen;
	public GameObject livingRoom;

	public GameObject currentRoom;
	public GameObject targetRoom;

	public Transform doorTrigger;

	public float minimum = 0.0f;
	public float maximum = 1f;
	public float duration = .0625f;
	public bool fadeIn;
	private float startTime;

	public GameObject curtainGO;
	public SpriteRenderer curtain;

	//public enum Rooms {MBR, KBR, HW, BR, F, K, LR};
	// Use this for initialization
	void Start () {
		//currentRoom = masterBedRoom;
		//targetRoom = masterBedRoom;
		doorTrigger = targetRoom.transform.GetChild (2);
		curtainGO.SetActive(true);
		curtain = curtainGO.GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = (Vector3.Lerp(transform.position, targetRoom.transform.position,1.5f * Time.deltaTime));
		transform.position = new Vector3 (transform.position.x, transform.position.y, -10);



		if(fadeIn)
		{
			//StartCoroutine(waitSeconds(1));
			float t = (Time.time - startTime) / duration;
			curtain.color = new Color(0f,0f,0f,Mathf.SmoothStep(minimum,maximum, t));
//			fade = Mathf.SmoothDamp(0f,1f,ref fadeSpeed,fadeTime);
//			curtain.color = new Color(0f,0f,0f,fade);
		}

		if(!fadeIn)
		{
//			StartCoroutine(waitSeconds(1));
			float t = (Time.time - startTime) / duration;
			curtain.color = new Color(0f,0f,0f,Mathf.SmoothStep(maximum,minimum, t));
//			fade = Mathf.SmoothDamp(1f,0f,ref fadeSpeed,fadeTime);
//			curtain.color = new Color(0f,0f,0f,fade);

		}

//		if (Input.GetKeyDown(KeyCode.KeypadEnter))
//		{
//			if(fadeIn)
//			{
//				fadeoutF();
//			}
//			else{
//
//				fadeInF();
//			}
//		}

	}

	public void fadeInF()
	{
		fadeIn = true;
		startTime = Time.time;
		//StartCoroutine(waitSeconds(3));
	}

	public void fadeoutF()
	{
		fadeIn = false;
		startTime = Time.time;
	}

	public void changeRoom(doorTriggerScript.Rooms target)
	{
		switch(target)
		{
		case doorTriggerScript.Rooms.MBR:
			targetRoom = masterBedRoom;
			doorTrigger = targetRoom.transform.Find("DoorTrigger");

			break;

		case doorTriggerScript.Rooms.KBR:
			targetRoom = kidBedRoom;
			doorTrigger = targetRoom.transform.Find("DoorTrigger");
			break;

		case doorTriggerScript.Rooms.HW:
			targetRoom = hallway;
			//doorTrigger = targetRoom.transform.GetChild (1);
			doorTrigger = targetRoom.transform.Find("DoorTrigger");
			break;

		case doorTriggerScript.Rooms.BR:
			targetRoom = bathRoom;
			doorTrigger = targetRoom.transform.Find("DoorTrigger");
			break;

		case doorTriggerScript.Rooms.F:
			targetRoom = foyer;
			doorTrigger = targetRoom.transform.Find("DoorTrigger");
			break;

		case doorTriggerScript.Rooms.K:
			targetRoom = kitchen;
			doorTrigger = targetRoom.transform.Find("DoorTrigger");
			break;

		case doorTriggerScript.Rooms.LR:
			targetRoom = livingRoom;
			doorTrigger = targetRoom.transform.Find("DoorTrigger");
			break;

		default:
			Debug.LogError("INVALID ROOM!");
			break;
		}
	}

}
