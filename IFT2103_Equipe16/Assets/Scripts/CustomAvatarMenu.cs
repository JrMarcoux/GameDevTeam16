using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	public Transform transformBody;
	public Animator bodyAnim;
	public Animator bodyPartAnim;
	public GameObject[] eyes;
	public GameObject[] mouths;
	public GameObject glasses;
	public GameObject mustache;
	private int indexMouth;
	private int indexEye;
	private int isMustache;
	private int isGlasses;
	private Vector3 initialTransform;
	private float heightValue = 0;
	private float widthValue = 0;
	public Toggle mustacheBtn;
	public Toggle glassesBtn;
	public Slider sliderHeight;
	public Slider sliderWidth;



	// Use this for initialization
	void Start () {
		indexMouth = 0;
		indexEye = 0;
		isGlasses = 0;
		isMustache = 0;
		initialTransform = transformBody.localScale;
	

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
		rewindAnim();
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
		rewindAnim();
	}
	public void toggleGlasses(bool value)
	{
		
		if (value)
		{
			isGlasses = 1;
			glasses.SetActive(true);
			rewindAnim();
		}
		else{
			isGlasses = 0;
			glasses.SetActive(false);
		}
	}
	public void toggleMustache(bool value)
	{
		
		if (value)
		{
			isMustache = 1;
			mustache.SetActive(true);
			rewindAnim();
		}
		else
		{
			isMustache = 0;
			mustache.SetActive(false);
		}
	}
	public void changeHeigth(float value)
	{
		heightValue = value; 
		transformBody.localScale =  initialTransform + new Vector3(widthValue*30, heightValue * 45, 0); 
	}
	public void changeWidth(float value)
	{
		widthValue = value;
		transformBody.localScale = initialTransform + new Vector3(widthValue * 30, heightValue*45, 0);
	}
	public void rewindAnim()
	{
		bodyAnim.Play("body", -1, 0f);
		bodyPartAnim.Play("bodyPart", -1, 0f);
		eyes[indexEye].GetComponent<Animator>().Play("eye" + (indexEye + 1), -1, 0f);
		mouths[indexMouth].GetComponent<Animator>().Play("mouth" + (indexMouth + 1), -1, 0f);
		glasses.GetComponent<Animator>().Play("glasses", -1, 0f);
		mustache.GetComponent<Animator>().Play("mustache", -1, 0f);
	}
	public void presetAvatar1()
	{
		changePrimaryBlue();
		changeSecondaryYellow();
		eyes[indexEye].SetActive(false);
		mouths[indexMouth].SetActive(false);
		indexEye = 1;
		indexMouth = 0;
		eyes[indexEye].SetActive(true);
		mouths[indexMouth].SetActive(true);
		mustacheBtn.isOn = false;
		glassesBtn.isOn = false;
		sliderHeight.value = 0;
		sliderWidth.value = 0;
		rewindAnim();
	}
	public void presetAvatar2()
	{
		changePrimaryOrange();
		changeSecondaryPink();
		eyes[indexEye].SetActive(false);
		mouths[indexMouth].SetActive(false);
		indexEye = 0;
		indexMouth = 2;
		eyes[indexEye].SetActive(true);
		mouths[indexMouth].SetActive(true);
		mustacheBtn.isOn = true;
		glassesBtn.isOn = false;
		sliderHeight.value = 0;
		sliderWidth.value = 0;
		rewindAnim();
	}
	public void presetAvatar3()
	{
		changePrimaryGray();
		changeSecondaryGreen();
		eyes[indexEye].SetActive(false);
		mouths[indexMouth].SetActive(false);
		indexEye = 2;
		indexMouth = 1;
		eyes[indexEye].SetActive(true);
		mouths[indexMouth].SetActive(true);
		mustacheBtn.isOn = false;
		glassesBtn.isOn = true;
		sliderHeight.value = 0;
		sliderWidth.value = 0;
		rewindAnim();
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
		PlayerPrefs.SetInt("eyeIndex", indexEye);
		PlayerPrefs.SetInt("mouthIndex", indexMouth);
		PlayerPrefs.SetInt("isGlasses",isGlasses);
		PlayerPrefs.SetInt("isMustache", isMustache);
		PlayerPrefs.SetFloat("avatarWidth", widthValue);
		PlayerPrefs.SetFloat("avatarHeight", heightValue);

	}

	string ColorToHex(Color32 color)
	{
		string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		return hex;
	}
}
