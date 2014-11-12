using UnityEngine;
using System.Collections;

public class doorTriggerScript : MonoBehaviour {

	public enum Rooms {MBR, KBR, HW, BR, F, K, LR};
	public Rooms targetRoom;
	public GameObject targetDoor;
	public GameObject realTargetRoom;
	public GameObject ownerRoom;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
