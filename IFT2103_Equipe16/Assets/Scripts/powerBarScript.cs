using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerBarScript : MonoBehaviour {

    public bool powerBarMoving = false;

    public Image powerBar;
    public float speed = 1f;
    public float addSpeed = 0.5f;

	
	void Start () {
        powerBar = GetComponent<Image>();
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
    public void IncreaseSpeed()
    {
        speed += addSpeed;
    }
}
