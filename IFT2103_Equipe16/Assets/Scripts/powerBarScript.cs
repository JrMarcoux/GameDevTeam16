using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerBarScript : MonoBehaviour {

    public float fullWidth = 256f;
    public bool powerBarMoving = false;

    public Image powerBar;
    private float timeFill = 20f;
    private float direction=1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (powerBarMoving == true)
        {
            if (powerBar.fillAmount <= 0)
            {
                direction = 1;
            }
            else if (powerBar.fillAmount >= 1)
            {
                direction = -1;
            }
            powerBar.fillAmount += direction / (fullWidth * timeFill * Time.deltaTime);

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
}
