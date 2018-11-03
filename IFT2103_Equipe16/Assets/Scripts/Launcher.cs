using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {
	public GameObject projectile;
	public Transform shotPoint;
	public string enemyTag;
    public powerBarScript powerBar;
    public GameObject currentProj;

    public bool isShooting;
    public bool isProj;
    public bool isDead;


    private void Start()
    {
        powerBar = GameObject.Find("powerBar").GetComponent<powerBarScript>();
        isShooting = false;
        isProj = false;
    }

    public IEnumerator launch()
	{

        //attendre que le joueur appuie sur Espace pour enclancher le lancer
        if (powerBar == null)
        {
            Start();
        }
        powerBar.ToggleOn();
		while (!isShooting)
		{     
			yield return null;
		}
        isShooting = !isShooting;
        powerBar.ToggleOff();
        currentProj = Instantiate(projectile, shotPoint.position, shotPoint.rotation) as GameObject; //instancier le projectile
        isProj = true;
    }
}
	