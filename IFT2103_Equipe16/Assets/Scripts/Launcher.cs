using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {
	public GameObject projectile;
	public Transform shotPoint;
	public bool isDead = false;
	public string enemyTag;
  public powerBarScript powerBar;

    private void Start()
    {
        powerBar = GameObject.Find("powerBar").GetComponent<powerBarScript>();
    }

    public IEnumerator launch()
	{

        //attendre que le joueur appuie sur Espace pour enclancher le lancer
        if (powerBar == null)
        {
            Start();
        }
        powerBar.ToggleOn();
		while (!Input.GetKeyDown(GetKeyPrefs("Fire")))
		{     
			yield return null;
		}
        powerBar.ToggleOff();
        GameObject currentProj = Instantiate(projectile, shotPoint.position, shotPoint.rotation); //instancier le projectile
		currentProj.GetComponent<RangeAttack>().nameOfTargets = enemyTag; //assigner le nom de la cible
    }

    public KeyCode GetKeyPrefs(string keyName)
    {
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(keyName));
    }
}
	