using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInfo : MonoBehaviour {

	public static playerInfo PI;
	

	private void OnEnable()
	{
		if (playerInfo.PI == null)
		{
			playerInfo.PI = this;
		}
		else
		{
			if (playerInfo.PI != this)
			{
				Destroy(playerInfo.PI.gameObject);
				playerInfo.PI = this;
			}
			DontDestroyOnLoad(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	

}
