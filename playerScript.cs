using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {
	public bool facingLeft;
	public Transform graphic;

	public int dirtiness;
	public float suitHP;
	public float suitMaxHP;
	public float hurtDamage;
	public float messiness;
	public bool naked;

	public float moveSpeed;
	public int numberTimesHit;
	public GameObject camera;

	public Vector3 velocity;
	public int direction;

	public bool onAction;
	public bool onDoor;
	public bool onStair;
	public bool traveling;
	public bool stairTraveling;

	public bool haveUmbrella;
	public GameObject currentRoom;

	public Transform shieldU;
	public Transform shieldR;
	public Transform shieldL;
	public Transform shieldD;

	public Transform lastXShield;
	public Transform lastYShield;

	public float timer;

	public Animator animator;

	public doorTriggerScript.Rooms lastRoom;
	public Vector3 lastPosition;

	public Vector3 travelTarget;
	//public enum Rooms{MBR, KBR, HW, BR, F, K, LR};
	public doorTriggerScript.Rooms targetRoom = doorTriggerScript.Rooms.MBR;
	public actionTriggerScript.TriggerName triggerName;
	public GameObject targetDoor;

	public GameObject gameLogicGO;

	public bool repeat = false;
	public bool repeat2 = false;

	public bool attacking = false;	//.4286
	public bool defending = false;

	// Use this for initialization
	void Start () {
		naked = true;
		timer = Time.time;
		haveUmbrella = true;

		suitMaxHP = 100f;
		suitHP = 0f;
		hurtDamage = 1f;
		messiness = 0f;



		shieldU = transform.FindChild("ShieldU");
		shieldR = transform.FindChild("ShieldR");
		shieldD = transform.FindChild("ShieldD");
		shieldL = transform.FindChild("ShieldL");

		lastXShield = shieldL;
		lastYShield = shieldU;

		shieldU.gameObject.SetActive(false);
		shieldR.gameObject.SetActive(false);
		shieldD.gameObject.SetActive(false);
		shieldL.gameObject.SetActive(false);

		dirtiness = 0;

		direction = 1;
		graphic = transform.FindChild("ManRun1_b_0");
		animator = graphic.GetComponent<Animator>();
		facingLeft = true;
		numberTimesHit = 0;
		traveling = false;
		stairTraveling = false;
		camera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Graphics and Input
	void Update () {

		//attacking = false;
		defending = false;

		if (suitHP < 0)
		{
			suitHP = 0;
			if(!naked)
			{
				naked = true;
				gameLogicGO.GetComponent<gameLogic>().naked = true;
				gameLogicGO.GetComponent<gameLogic>().ripAnimate();
			}
		}

		if (messiness >= 100)
		{
			messiness = 100;
			dirtiness = 3;
			gameLogicGO.GetComponent<gameLogic>().dirty = true;
		}

		else if(messiness < 100 && messiness >= 66)
		{
			dirtiness = 2;
		}

		else if (messiness < 66 && messiness >= 33)
		{
			dirtiness = 1;
		}

		else
			dirtiness = 0;

		if(Input.GetKeyDown(KeyCode.L) && haveUmbrella && !traveling && !stairTraveling && !repeat2)
		{
			timer = Time.time;
			repeat2 = true;
			attacking = true;
			rigidbody.isKinematic = true;
		}
		else if (Time.time - timer >.4286f && !traveling && !stairTraveling)
		{
			attacking = false;
			repeat2 = false;
			rigidbody.isKinematic = false;
		}
			//StartCoroutine(attackSeconds(1));

		if (Input.GetKey(KeyCode.K) && haveUmbrella && !traveling && !stairTraveling)
		{
			defending = true;
		}

//		if (!Input.GetKeyDown(KeyCode.L))
//		{
//			attacking = false;
//		}
		if (dirtiness < 0)
			dirtiness = 0;

		if (dirtiness > 3)
			dirtiness = 3;

		animator.SetInteger("Dirty", dirtiness);

		//animator.SetFloat("Velocity", Mathf.Abs(velocity.x));

		graphic.GetComponent<SpriteRenderer>().renderer.sortingOrder = (int)(transform.position.y * 100 * -1);

		if (velocity.x > 0){
			direction = 1;
		}
		else if (velocity.x < 0)
		{
			direction = -1;
		}

		if (velocity.y > 0)
			animator.SetInteger("DefendingDirection", 1);

		else if (velocity.y < 0)
			animator.SetInteger("DefendingDirection", 0);



		if (!defending && !attacking)
		{

			if (((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical")) != 0) || traveling)
			{
				animator.SetBool("Running", true);
			}

			else
				animator.SetBool("Running", false);
			               
		}

		else if (defending)
		{
			animator.SetBool("Running", false);
			animator.SetBool("Defending", true);

		}

		else if (attacking)
		{
			animator.SetBool("Running", false);
			animator.SetBool("Attacking", true);
		}

		if(!defending)
		{
			animator.SetBool("Defending", false);
		}
		
		if (!attacking)
		{
			animator.SetBool("Attacking", false);
		}

		if(facingLeft)
		{
			graphic.transform.localScale = new Vector3(Mathf.Abs(graphic.transform.localScale.x) * direction, graphic.transform.localScale.y, graphic.transform.localScale.z); 
		}


		else
		{

		}

		if(Input.GetKeyDown(KeyCode.Space) && !attacking && !defending)
		{	
			if(onAction)
			{
				switch(triggerName)
				{
				case actionTriggerScript.TriggerName.brushTeeth:
					brushTeeth();
					break;

				case actionTriggerScript.TriggerName.takeShower:
					takeShower();
					break;

				case actionTriggerScript.TriggerName.getDressed:
					if (gameLogicGO.GetComponent<gameLogic>().naked)
						getDressed();
					break;

				case actionTriggerScript.TriggerName.eatFood:
					eatFood();
					break;

				case actionTriggerScript.TriggerName.goToWork:
					goToWork();
					break;

				case actionTriggerScript.TriggerName.getUmbrella:
					getUmbrella();
					break;

				case actionTriggerScript.TriggerName.getKeys:
					getKeys();
					break;

				default:
					break;

				}
			}

			if (onDoor)
			{
				travelTarget = targetDoor.transform.position;
				traveling = true;
				rigidbody.isKinematic = true;
				camera.GetComponent<cameraScript>().changeRoom(targetRoom);
				currentRoom = targetDoor.GetComponent<doorTriggerScript>().ownerRoom;
			}
			else if (onStair)
			{
				travelTarget = targetDoor.transform.position;
				traveling = true;
				stairTraveling = true;
				rigidbody.isKinematic = true;
				camera.GetComponent<cameraScript>().changeRoom(targetRoom);
				camera.GetComponent<cameraScript>().fadeInF();
				repeat = false;
				currentRoom = targetDoor.GetComponent<doorTriggerScript>().ownerRoom;

			}

		}
	}
	// Physics
	void FixedUpdate(){

		shieldU.gameObject.SetActive(false);
		shieldR.gameObject.SetActive(false);
		shieldD.gameObject.SetActive(false);
		shieldL.gameObject.SetActive(false);

		if(defending || attacking)
		{
			if(velocity.x > 0)
			{
				shieldR.gameObject.SetActive(true);
				lastXShield = shieldR;
			}	
			else if(velocity.x < 0)
			{
				shieldL.gameObject.SetActive(true);
				lastXShield = shieldL;
			}
			else
				lastXShield.gameObject.SetActive(true);

			if(velocity.y > 0)
			{
				shieldU.gameObject.SetActive(true);
				lastYShield = shieldU;
			}	
			else if(velocity.y < 0)
			{
				shieldD.gameObject.SetActive(true);
				lastYShield = shieldD;
			}
			else
				lastYShield.gameObject.SetActive(true);
		}

//		if (defending)
//		{
//			shieldU.GetComponent<BoxCollider>().isTrigger = true;
//			shieldR.GetComponent<BoxCollider>().isTrigger = true;
//			shieldD.GetComponent<BoxCollider>().isTrigger = true;
//			shieldL.GetComponent<BoxCollider>().isTrigger = true;
//		}		

		if (attacking)
		{
			shieldU.GetComponent<BoxCollider>().isTrigger = false;
			shieldR.GetComponent<BoxCollider>().isTrigger = false;
			shieldD.GetComponent<BoxCollider>().isTrigger = false;
			shieldL.GetComponent<BoxCollider>().isTrigger = false;
		}

		velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime, 0);

		if (!defending && !attacking)
			{
			if (!traveling)
				transform.Translate(velocity);

			else if(traveling){
				transform.position = Vector3.MoveTowards(transform.position,travelTarget, moveSpeed * Time.deltaTime);	
			}
		}

		if (transform.position == travelTarget)
		{
			rigidbody.isKinematic = false;
			traveling = false;
			stairTraveling = false;
		}

		if (!stairTraveling && !repeat)
		{
			StartCoroutine(waitSeconds(1));

			repeat = true;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "DoorTrigger")
		{
			onDoor = true;
			targetRoom = col.GetComponent<doorTriggerScript>().targetRoom;
			targetDoor = col.GetComponent<doorTriggerScript>().targetDoor;
		}

		if(col.tag == "StairTrigger")
		{
			onStair = true;
			targetRoom = col.GetComponent<doorTriggerScript>().targetRoom;
			targetDoor = col.GetComponent<doorTriggerScript>().targetDoor;
		}

		if (col.tag == "ActionTrigger")
		{
			onAction = true;
			triggerName = col.GetComponent<actionTriggerScript>().triggerName;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if(col.tag == "DoorTrigger")
		{
			onDoor = false;
		}

		if(col.tag == "StairTrigger")
		{
			onStair = false;
		}

		if(col.tag == "ActionTrigger")
		{
			onAction = false;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Bullet")
			dirtiness++;


			//gameLogicGO.GetComponent<gameLogic>().dirty++;

	}

	void OnCollisionStay(Collision col)
	{
		if(col.gameObject.tag == "Clown")
		{
			suitHP -= hurtDamage * Time.deltaTime;
		}
	}

	public void brushTeeth()
	{
		Debug.Log("BUSHED TEETH!");
		gameLogicGO.GetComponent<gameLogic>().badBreath = false;

	}

	public void takeShower()
	{
		Debug.Log ("TOOK SHOWER!");
		gameLogicGO.GetComponent<gameLogic>().dirty = false;
		dirtiness = 0;
		messiness = 0;
		gameLogicGO.GetComponent<gameLogic>().showerAnimate();
	}

	public void eatFood()
	{
		Debug.Log("ATE FOOD!");
		gameLogicGO.GetComponent<gameLogic>().hungry = false;
		gameLogicGO.GetComponent<gameLogic>().badBreath = true;
		if (naked)
		{
			gameLogicGO.GetComponent<gameLogic>().eatAnimate();
		}
		else
		{
			gameLogicGO.GetComponent<gameLogic>().eatSuitAnimate();
		}
	}

	public void getDressed()
	{
		Debug.Log("GOT DRESSED!");
		gameLogicGO.GetComponent<gameLogic>().naked = false;
		gameLogicGO.GetComponent<gameLogic>().dressAnimate();
		//gameLogicGO.GetComponent<gameLogic>().dressAnimation.GetComponent<dressAnimationScript>().animate();
		suitHP = suitMaxHP;
		naked = false;
	}

	public void getUmbrella()
	{
		Debug.Log ("GOT UMBRELLA!");
		haveUmbrella = true;
		gameLogicGO.GetComponent<gameLogic>().getUmbrella();
	}

	public void getKeys()
	{
		gameLogicGO.GetComponent<gameLogic>().keyItem = true;
		Destroy(gameLogicGO.GetComponent<gameLogic>().keys.gameObject);
	}

	public void goToWork()
	{
		if (gameLogicGO.GetComponent<gameLogic>().ready)
		{
			Debug.Log ("WENT TO WORK SUCCESSFULLY");
			gameLogicGO.GetComponent<gameLogic>().gotoWorkSuccess();
		}
		else
		{
			Debug.Log ("WENT TO WORK AS A FAILURE");
			gameLogicGO.GetComponent<gameLogic>().gotoWorkFail();
		}
	}

	IEnumerator attackSeconds (float seconds)
	{
		yield return new WaitForSeconds(seconds);
		repeat2 = false;
		attacking = false;

	}

	IEnumerator waitSeconds (float seconds)
	{
		yield return new WaitForSeconds(seconds);
		camera.GetComponent<cameraScript>().fadeoutF();
	}
}
