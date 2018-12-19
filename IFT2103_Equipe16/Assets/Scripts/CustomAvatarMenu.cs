using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAvatarMenu : MonoBehaviour {

	public SpriteRenderer avatarBody;
	public SpriteRenderer avatarBodyPart;
	public Color blue;
	public Color red;
	public Color green;
	public Color orange;
	public Color purple;
	public Color pink;
	public Color grey;
	public Color yellow;
	public Animator bodyAnim;
	public Animator bodyPartAnim;
	public GameObject[] eyes;
	public GameObject[] mouths;
	private int indexMouth;
	private int indexEye;

	// Use this for initialization
	void Start () {
		indexMouth = 0;
		indexEye = 0;
	}

	public void changeEye()
	{
		eyes[indexEye].SetActive(false);
		if (indexEye < eyes.Length - 1)
		{
			indexEye++;
		}
		else
		{
			indexEye = 0;
		}
		eyes[indexEye].SetActive(true);
	}
	public void changeMouth()
	{
		mouths[indexMouth].SetActive(false);
		if (indexMouth < mouths.Length - 1)
		{
			indexMouth++;
		}
		else
		{
			indexMouth = 0;
		}
		mouths[indexMouth].SetActive(true);
	}

	public void changePrimaryBlue()
	{
		avatarBody.color = blue;
	}
	public void changePrimaryRed()
	{
		avatarBody.color = red;
	}
	public void changePrimaryGray()
	{
		avatarBody.color = grey;
	}
	public void changePrimaryOrange()
	{
		avatarBody.color = orange;
	}

	public void changeSecondaryPurple()
	{
		avatarBodyPart.color = purple;
	}
	public void changeSecondaryPink()
	{
		avatarBodyPart.color = pink;
	}
	public void changeSecondaryGreen()
	{
		avatarBodyPart.color = green;
	}
	public void changeSecondaryYellow()
	{
		avatarBodyPart.color = yellow;
	}

	public void saveSetting()
	{
		PlayerPrefs.SetString("PrimaryColor", ColorToHex(avatarBody.color) );
		PlayerPrefs.SetString("SecondaryColor", ColorToHex(avatarBodyPart.color));
	}

	string ColorToHex(Color32 color)
	{
		string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		return hex;
	}
}
