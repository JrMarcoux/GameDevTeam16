using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {
	public GameObject projectile;
	public Transform shotPoint;
	public string enemyTag;
    public powerBarScript powerBar;

    public bool isShooting;
    public bool isDead;


    private void Start()
    {
        powerBar = GameObject.Find("powerBar").GetComponent<powerBarScript>();
        isShooting = false;
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
        Instantiate(projectile, shotPoint.position, shotPoint.rotation); //instancier le projectile
    }
}
	