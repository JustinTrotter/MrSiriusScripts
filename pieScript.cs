using UnityEngine;
using System.Collections;

public class pieScript : MonoBehaviour {

	public Vector3 target;
	public float moveSpeed;
	public GameObject player;
	public Transform graphic;
	public float offset;
	public bool stopped = false;
	public bool reflected = false;

	public bool pieFacing = false;
	//public float timer = Time.time;

	Animator animator;

	// Use this for initialization
	void Start () {
		stopped = false;
		reflected = false;
		moveSpeed = 2f;
		player = GameObject.FindGameObjectWithTag("Player");
		graphic = transform.GetChild(0);
		target = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 0);
		animator = GetComponentInChildren<Animator>();

		if (transform.position.x - player.transform.position.x > 0)
			graphic.transform.localScale = new Vector3(Mathf.Abs(graphic.transform.localScale.x) * 1, graphic.transform.localScale.y, graphic.transform.localScale.z); 

		else
			graphic.transform.localScale = new Vector3(Mathf.Abs(graphic.transform.localScale.x) * -1, graphic.transform.localScale.y, graphic.transform.localScale.z); 

		if (transform.position.y - player.transform.position.y < 0)
			pieFacing = true;
			//graphic.GetComponent<Animator>().SetTrigger("Up");
		else
			pieFacing = false;
			//graphic.GetComponent<Animator>().SetTrigger("Down");
	}
	
	// Update is called once per frame
	void Update () {




		if (pieFacing)
			graphic.GetComponent<Animator>().SetTrigger("Up");
		else
			graphic.GetComponent<Animator>().SetTrigger("Down");
	}

	void FixedUpdate()
	{

		if (!stopped)
		{
			transform.Translate(target * moveSpeed* Time.deltaTime);
			
		}

		else {
			
			transform.GetChild(0).GetComponent<SpriteRenderer>().renderer.sortingOrder = -98;
			Destroy(gameObject,5f);
			
		}
//		if (Time.time - timer > 10f && stopped)
//		{
//			Destroy(gameObject);
//		}
	}

	void OnCollisionEnter(Collision col){
		if ((col.gameObject.tag == "Wall" || col.gameObject.tag == "Player") && !stopped)
		{
			if (col.gameObject.tag == "Player") {
				player.GetComponent<playerScript>().messiness += 33f;
			}
			die ();
		}

	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Shield")
		{
			if(!reflected)
			{
				Debug.Log("REFLECTED!");
				target = new Vector3(target.x * -1, target.y * -1, target.z);
				//Vector3 normal = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 0);
				//target = Vector3.Reflect(normal, target);



				if (transform.position.x - player.transform.position.x > 0)
					graphic.transform.localScale = new Vector3(Mathf.Abs(graphic.transform.localScale.x) * -1, graphic.transform.localScale.y, graphic.transform.localScale.z); 
				
				else
					graphic.transform.localScale = new Vector3(Mathf.Abs(graphic.transform.localScale.x) * 1, graphic.transform.localScale.y, graphic.transform.localScale.z); 
				if (transform.position.y - player.transform.position.y <= 0)
					pieFacing = false;
				//	
				else if (transform.position.y - player.transform.position.y > 0)
					pieFacing = true;

				reflected = true;
			}

		}
	}

	void die()
	{
		//timer = Time.deltaTime;
		stopped = true;
		target = transform.position;
		GetComponent<SphereCollider>().isTrigger = true;
		//animator.SetTrigger("Die");

	}
}
