using UnityEngine;
using System.Collections;

public class eatScript : MonoBehaviour {
	public Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void eat()
	{
		//transform.position = new Vector3 (pos.x, pos.y + .3f, 0f);
		animator.SetTrigger("Eat");
	}

	public void eatSuit()
	{
		animator.SetTrigger("EatSuit");
	}
}
