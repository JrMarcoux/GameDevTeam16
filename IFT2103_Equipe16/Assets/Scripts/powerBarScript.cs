using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerBarScript : MonoBehaviour {

    public bool powerBarMoving = false;

    public Image powerBar;
    public float speed = 1f;
    public float addSpeed = 0.5f;

	// Use this for initialization
	void Start () {
        powerBar = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (powerBarMoving == true)
        {
            powerBar.fillAmount = Mathf.PingPong(Time.time*speed, 1);
        }
	}

    public void ToggleOn()
    {
        powerBarMoving = true;
    }

    public void ToggleOff()
    {
        powerBarMoving = false;
    }

    public float GetAmount()
    {
        return powerBar.fillAmount;
    }

    public void IncreaseSpeed()
    {
        speed += addSpeed;
    }
}
