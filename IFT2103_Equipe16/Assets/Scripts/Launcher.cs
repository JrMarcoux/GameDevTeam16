using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {
	public GameObject projectile;
	public Transform shotPoint;
	public bool isDead = false;
	public string enemyTag;

    public IEnumerator launch()
	{
        powerBarScript powerBar = GameObject.Find("powerBar").GetComponent<powerBarScript>();
        //attendre que le joueur appuie sur Espace pour enclancher le lancer
        powerBar.ToggleOn();
		while (!Input.GetKeyDown(KeyCode.Space))
		{     
			yield return null;
		}
        powerBar.ToggleOff();
        GameObject currentProj = Instantiate(projectile, shotPoint.position, shotPoint.rotation); //instancier le projectile
		currentProj.GetComponent<RangeAttack>().nameOfTargets = enemyTag; //assigner le nom de la cible

	}
}
	