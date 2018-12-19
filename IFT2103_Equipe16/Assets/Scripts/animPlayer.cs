using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animPlayer : MonoBehaviour {

	public Transform avatarTransform;
	public Animator body;
	public Animator bodyPart;
	bool isDead = false;
	bool stopCheck = false;
	public SpriteRenderer bodyRenderer;
	public SpriteRenderer bodyPartRenderer;
	public GameObject[] eyes;
	public GameObject[] mouths;
	private int eyeIndex;
	private int mouthIndex;
	public GameObject glasses;
	public GameObject mustache;

	// Use this for initialization
	void Start() {

		bodyRenderer.color = HexToColor(PlayerPrefs.GetString("PrimaryColor"));
		bodyPartRenderer.color = HexToColor(PlayerPrefs.GetString("SecondaryColor"));
		eyeIndex = PlayerPrefs.GetInt("eyeIndex");
		mouthIndex = PlayerPrefs.GetInt("mouthIndex");
		eyes[eyeIndex].SetActive(true);
		mouths[mouthIndex].SetActive(true);
		if (PlayerPrefs.GetInt("isGlasses") == 1)
			glasses.SetActive(true);
		if (PlayerPrefs.GetInt("isMustache") == 1)
			mustache.SetActive(true);
		avatarTransform.localScale += new Vector3(PlayerPrefs.GetFloat("avatarWidth"), PlayerPrefs.GetFloat("avatarHeight"), 0);
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
				eyes[eyeIndex].GetComponent<Animator>().SetBool("isDead",true);
				mouths[mouthIndex].GetComponent<Animator>().SetBool("isDead", true);
				mustache.GetComponent<Animator>().SetBool("isDead", true);
				glasses.GetComponent<Animator>().SetBool("isDead", true);

			}

		}
	}
}
