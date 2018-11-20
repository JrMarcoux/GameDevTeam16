using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerBarScript : MonoBehaviour {

    public bool powerBarMoving = false;

    public Image powerBar;
    public float speed = 1f;
    private float addSpeed;
    private float maxSpeed;

	
	void Start () {
        powerBar = GetComponent<Image>();
        maxSpeed = 3/PlayerPrefs.GetInt("levelWidth");
    }
	
	//change la longeur de la barre en fonction du temps
	void Update () {
        if (powerBarMoving == true)
        {
            powerBar.fillAmount = Mathf.PingPong(Time.time*speed*addSpeed, 1);
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
        addSpeed = distance * maxSpeed;
    }
}
