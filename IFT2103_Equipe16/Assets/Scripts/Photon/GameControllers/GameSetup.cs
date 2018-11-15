using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour {

	public static GameSetup GS;

	public Transform[] spawnPoints;

	private void OnEnable()
	{
		if(GameSetup.GS == null)
		{
			GameSetup.GS = this;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
