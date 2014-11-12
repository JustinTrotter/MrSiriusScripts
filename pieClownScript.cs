using UnityEngine;
using System.Collections;

public class pieClownScript : MonoBehaviour {

	public float distance;
	public Vector3 direction;
	public float shootForce;
	public Transform pie;
	public bool throwingPie;
	public float timer;
	public int health;
	public int hitByPlayerDamage;
	public float hitTimer;
	public GameObject player;
	public float moveSpeed;
	public Transform graphic;
	public playerScript playerComp;
	public int directionFace;

	// Use this for initialization
	void Start () {
		//base.Start();
		player = GameObject.FindGameObjectWithTag("Player");
		playerComp = player.GetComponent<playerScript>();
		graphic = transform.GetChild(0);

		throwingPie = false;
		timer = 0;
		health = 3;
		hitByPlayerDamage = 1;
		hitTimer = 0;
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Shield")
		{
			player.GetComponent<playerScript>().messiness += player.GetComponent<playerScript>().hurtDamage;
			health -= hitByPlayerDamage;
		}

	}


	void Update(){
		//if (graphic.GetComponent<Animator>().GetBool("Throwing"))
		//	
		if (player.transform.position.x > transform.position.x)
			directionFace = -1;
		else
			directionFace = 1;
		
		graphic.transform.localScale = new Vector3(Mathf.Abs(graphic.transform.localScale.x) * directionFace, graphic.transform.localScale.y, graphic.transform.localScale.z); 
		graphic.GetComponent<SpriteRenderer>().renderer.sortingOrder = (int)(transform.position.y * 100 * -1);
	
		if (player.GetComponent<playerScript>().dirtiness != 3)
		{
			if (Time.time - timer > 1f)
				throwPie ();
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		//base.Update();
		if (health <= 0)
		{
			Destroy(gameObject);
			return;
		}
		distance = Vector3.Distance(transform.position, player.transform.position);
		
		if(throwingPie)
		{
			throwingPie = false;
			waitSeconds(.1429f);
		}

		//if(graphic.animation.IsPlaying("pieth row"))
		//{
			if (distance > 2.1f)
			{
				transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
			}

			else if (distance < 2f)
			{

	//			direction = transform.position - player.transform.position;
	//			direction.Normalize();

				transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -moveSpeed * Time.deltaTime);
			}
		//}
	}

	void throwPie()
	{
		timer = Time.time;
		throwingPie = true;


		graphic.GetComponent<Animator>().SetTrigger("Throw");
		graphic.GetComponent<Animator>().SetBool("Throwing", true);
		StartCoroutine(waitSeconds(.1429f * 2));
	}

	void pieCoolDown()
	{
		graphic.GetComponent<Animator>().SetBool("Throwing", false);

	}

	IEnumerator waitSeconds (float seconds)
	{
		yield return new WaitForSeconds(seconds);
		Instantiate(pie, transform.position, transform.rotation);
		
		
	}
}
