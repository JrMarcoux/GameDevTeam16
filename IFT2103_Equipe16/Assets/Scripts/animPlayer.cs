using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animPlayer : MonoBehaviour {

	public Animator body;
	public Animator bodyPart;
	bool isDead = false;
	bool stopCheck = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!stopCheck)
		{
			isDead = GetComponent<Launcher>().isDead;
			if (isDead)
			{
				stopCheck = true;
				body.SetBool("isDead",true);
				bodyPart.SetBool("isDead", true);
			}

		}
	}
}
