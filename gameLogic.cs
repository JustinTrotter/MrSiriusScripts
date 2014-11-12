using UnityEngine;
using System.Collections;

public class gameLogic : MonoBehaviour {

	public GameObject player;

	public bool badBreath;
	public bool dirty;
	public bool hungry;
	public bool naked;
	public bool ready;
	public bool keyItem;

	public GameObject keys;

	public float floatTimer;
	public int timer;
	public int numShowered;
	public float minutes;
	public float seconds;
	public string minuteString;
	public string secondString;

	public GameObject dressAnimation;
	public GameObject showerAnimation;
	public GameObject ripAnimation;
	public GameObject eatAnimation;

	public GameObject umbrellaItem;

	public string currentScene;

	public int numTimesLate;

	public float spawnSpeed;

	public bool aggression = true;


	// Use this for initialization
	void Start () {
		currentScene = Application.loadedLevelName;
		Debug.Log (currentScene);
		spawnSpeed = 1000f;



		if (currentScene == "Day1")
		{
			aggression = false;
			Debug.Log ("Day1 has started");
			numShowered = 0;
			badBreath = true;
			dirty = true;
			hungry = true;
			naked = true;
			keyItem = false;

			ready = false;

			timer = 300;
		}

		else if (currentScene == "cutscene")
		{

			floatTimer = Time.time;
		
		}

		else if(currentScene == "Day1half")
		{
			aggression = false;
			Debug.Log ("Day1half has started");
			numShowered = 0;
			badBreath = true;
			dirty = false;
			hungry = false;
			naked = false;
			keyItem = false;

			ready = false;

			timer = 0;

		}

		else if (currentScene == "Day2")
		{
			spawnSpeed = 5f;
			numShowered = 0;
			badBreath = true;
			dirty = true;
			hungry = true;
			naked = true;
			keyItem = false;
			
			ready = false;
			
			timer = 180;
		}

		if (currentScene == "Day3")
		{
			badBreath = true;
			dirty = true;
			hungry = true;
			naked = true;
			keyItem = false;
			
			ready = false;
			
			timer = 150;
		}

		if (currentScene == "Day4")
		{
			badBreath = true;
			dirty = true;
			hungry = true;
			naked = true;
			keyItem = false;
			
			ready = false;
			
			timer = 150;
		}

		if (currentScene == "Day5")
		{
			badBreath = true;
			dirty = true;
			hungry = true;
			naked = true;
			keyItem = false;
			
			ready = false;
			
			timer = 120;
		}

		StartCoroutine(waitSeconds (1));
	}
	
	// Update is called once per frame
	void Update () {



		if(currentScene == "Day1")
		{
			if (!dirty && !hungry && !naked)
				ready = true;
			else
				ready = false;
		}

		else if (currentScene == "Day1half")
		{
			if (keyItem)
			{
				ready = true;
			}
		}

		else if (currentScene == "Day2")
		{
			if (!dirty && !hungry && !naked)
				ready = true;
			else
				ready = false;
		}

		else if (currentScene == "Day3")
		{
			if (!dirty && !hungry && !naked)
				ready = true;
			else
				ready = false;
		}

		else if (currentScene == "Day4")
		{
			if (!dirty && !hungry && !naked)
				ready = true;
			else
				ready = false;
		}

		else if (currentScene == "Day5")
		{
			if (!dirty && !hungry && !naked)
				ready = true;
			else
				ready = false;
		}

		else if(currentScene == "cutscene")
		{
			if (Input.GetKeyDown (KeyCode.Space))
				Application.LoadLevel("Day1half");
		}

		else if(currentScene == "cutscene2")
		{
			if (Input.GetKeyDown (KeyCode.Space))
				Application.LoadLevel("Day2");
		}

		else if(currentScene == "cutscene3")
		{
			if (Input.GetKeyDown (KeyCode.Space))
				Application.LoadLevel("Day3");
		}

		else if(currentScene == "cutscene4")
		{
			if (Input.GetKeyDown (KeyCode.Space))
				Application.LoadLevel("Day4");
		}

		else if(currentScene == "cutscene5")
		{
			if (Input.GetKeyDown (KeyCode.Space))
				Application.LoadLevel("Day5");
		}

		else if(currentScene == "gameover")
		{
			if (Input.GetKeyDown (KeyCode.Space))
				Application.LoadLevel("TitleScreen");
		}

		else if(currentScene == "TitleScreen")
		{
			if (Input.GetKeyDown (KeyCode.Space))
				Application.LoadLevel("Intro1");
		}

		else if(currentScene == "Intro1")
		{
			if (Input.GetKeyDown (KeyCode.Space))
				Application.LoadLevel("Intro2");
		}

		else if(currentScene == "Intro2")
		{
			if (Input.GetKeyDown (KeyCode.Space))
				Application.LoadLevel("tutorial");
		}

		else if(currentScene == "happyending")
		{
			if (Input.GetKeyDown (KeyCode.Space))
				Application.LoadLevel("TitleScreen");
		}	

		else if(currentScene == "tutorial")
		{
			if (Input.GetKeyDown (KeyCode.Space))
				Application.LoadLevel("Day1");
		}	
	
		else 
		{
			ready = false;

		}

		if(naked)
			player.GetComponent<playerScript>().animator.SetBool("Naked", true);
		else
			player.GetComponent<playerScript>().animator.SetBool("Naked", false);

		minutes = Mathf.Floor (timer / 60);
		seconds = Mathf.RoundToInt(timer % 60);

		if (minutes < 10)
			minuteString = "0" + minutes.ToString();
		else
			minuteString = minutes.ToString();

		if (seconds < 10)
			secondString = "0" + Mathf.RoundToInt(seconds).ToString();
		else
			secondString = Mathf.RoundToInt(seconds).ToString();

		if (timer <= 0)
		{
			timer = 0;
			if (currentScene != "Day1half")
				tooLate();
		}
	}

	public void dressAnimate(){
		player.gameObject.SetActive(false);
		dressAnimation.GetComponent<dressAnimationScript>().animate(player.transform.position);
		StartCoroutine(disablePlayerSeconds(1.48f));
	}

	public void showerAnimate(){
		player.gameObject.SetActive(false);
		numShowered++;
		showerAnimation.GetComponent<showerAnimationScript>().shower();
		showerAnimation.GetComponent<showerAnimationScript>().animator.SetInteger("numShowered", numShowered);
		if (numShowered < 4)
			StartCoroutine(disablePlayerSeconds(2.8571f));
		else
			StartCoroutine(disablePlayerSeconds(3.5f));


	}

	public void ripAnimate(){
		player.gameObject.SetActive(false);
		ripAnimation.GetComponent<clothesRipScript>().rip(player.transform.position);
		StartCoroutine(disablePlayerSeconds(1f));

	}

	public void eatAnimate()
	{
		player.gameObject.SetActive(false);
		eatAnimation.GetComponent<eatScript>().eat();
		StartCoroutine(disablePlayerSeconds(1.714f));
	}

	public void eatSuitAnimate()
	{
		player.gameObject.SetActive(false);
		eatAnimation.GetComponent<eatScript>().eatSuit();
		StartCoroutine(disablePlayerSeconds(1.714f));
	}

	public void getUmbrella()
	{
		umbrellaItem.gameObject.SetActive(false);
	}


	void FixedUpdate(){

	}
	IEnumerator disablePlayerSeconds (float seconds)
	{
		yield return new WaitForSeconds(seconds);
		player.gameObject.SetActive(true);
	}

	IEnumerator waitSeconds (float seconds)
	{
		while (timer > 0)
		{
			yield return new WaitForSeconds(seconds);
			timer--;
		}
	}

	public void gotoWorkSuccess()
	{	
		switch(currentScene)
		{
		case "Day1":
			Application.LoadLevel("cutscene");
			break;

		case "Day1half":
			Application.LoadLevel ("cutscene2");	
			break;

		case "Day2":
			Application.LoadLevel ("cutscene3");
			break;

		case "Day3":
			Application.LoadLevel ("cutscene4");
			break;

		case "Day4":
			Application.LoadLevel ("cutscene5");
			break;

		case "Day5":
			Application.LoadLevel ("happyending");
			break;

		default:
			break;
		}
	}

	public void gotoWorkFail()
	{
		switch(currentScene)
		{
		case "Day1":

			break;

		default:

			break;


		}
	}

	public void tooLate()
	{
		Application.LoadLevel("gameover");
	}

	public void clownSplosion()
	{

	}

	void OnGUI(){
		if (currentScene == "Day1" || currentScene == "Day2" || currentScene == "Day3" || currentScene == "Day4" || currentScene == "Day5") 
		{
			GUI.Label (new Rect (10, 30, 150, 100), minuteString + ":" + secondString);
			GUI.Label (new Rect (10, 50, 150, 100), "Clean: " + !dirty);
			GUI.Label (new Rect (10, 70, 150, 100), "Dressed: " + !naked);
			GUI.Label (new Rect (10, 90, 150, 100), "Feed: " + !hungry);
			GUI.Label (new Rect (10, 110, 150, 100), "Ready to go: " + ready);

		}
		if (currentScene == "Day1")
		{
			GUI.Label (new Rect (10, 10, 150, 100), "Day 1 of 5");
		}
		if (currentScene == "Day2")
		{
			GUI.Label (new Rect (10, 10, 150, 100), "Day 2 of 5");
		}
		if (currentScene == "Day3")
		{
			GUI.Label (new Rect (10, 10, 150, 100), "Day 3 of 5");
		}
		if (currentScene == "Day4")
		{
			GUI.Label (new Rect (10, 10, 150, 100), "Day 4 of 5");
		}
		if (currentScene == "Day5")
		{
			GUI.Label (new Rect (10, 10, 150, 100), "Day 5 of 5");
		}
	}

}
