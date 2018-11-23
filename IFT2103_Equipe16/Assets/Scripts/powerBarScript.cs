using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerBarScript : MonoBehaviour {

    public bool powerBarMoving = false;

    public Image powerBar;
    public float speed = 1f;
    private float maxDistance;


    void Start () {
        powerBar = GetComponent<Image>();
        maxDistance = PlayerPrefs.GetInt("levelWidth");
    }
	
	//change la longeur de la barre en fonction du temps
	void Update () {
        if (powerBarMoving == true)
        {
            powerBar.fillAmount = Mathf.PingPong(Time.time*speed, 1);
        }
	}
    //activer ou désactiver le mouvement si tour commencé ou en attente du prochain
    public void ToggleOn()
    {
        powerBarMoving = true;
    }

    public void ToggleOff()
    {
        powerBarMoving = false;
    }
    //obtenir la valeur de la barre pour la distance de la balle
    public float GetAmount()
    {
        return powerBar.fillAmount;
    }
    //la vitesse augmente à chaque tour
    public void ChangeSpeed(float distance)
    {
        if (distance >= (0.8*maxDistance) && speed != 3)
        {
            speed = 3;
        }
        else if ((0.8 * maxDistance) > distance && distance >= (0.4 * maxDistance) && speed != 2)
        {
            speed = 2;
        }
        else if (distance < (0.4 * maxDistance) && speed != 1)
        {
            speed = 1;
        }

    }
}
