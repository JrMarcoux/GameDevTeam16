using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animPlayer : MonoBehaviour {

	public Animator body;
	public Animator bodyPart;
	bool isDead = false;
	bool stopCheck = false;
	public SpriteRenderer bodyRenderer;
	public SpriteRenderer bodyPartRenderer;



	// Use this for initialization
	void Start () {

		//bodyRenderer.color = HexToColor(PlayerPrefs.GetString("PrimaryColor"));
		//bodyPartRenderer.color = HexToColor(PlayerPrefs.GetString("SecondaryColor"));


	}
	Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r, g, b, 255);
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
