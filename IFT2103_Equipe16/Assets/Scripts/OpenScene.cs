using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("return") || Input.GetKey("enter"))
		{
			SceneManager.LoadScene(1);
		}
	}
}
