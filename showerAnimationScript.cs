using UnityEngine;
using System.Collections;

public class showerAnimationScript : MonoBehaviour {
	public Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void shower()
	{
		animator.SetTrigger("Shower");
	}
}
