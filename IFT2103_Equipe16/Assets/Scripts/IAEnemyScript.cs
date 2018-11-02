using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEnemyScript : MonoBehaviour {

    private GameObject gameManager;

    void Start () {
        gameManager = GameObject.FindGameObjectWithTag("Game manager");
    }
	
	void Update () {
        //les appels de fonctions devraient ressembler à ceux de controlPlayer pour plus de réalisme
        if (gameManager.GetComponent<GameManager>().teamTurn == gameManager.GetComponent<GameManager>().enemiesTag)
        {
            //ici ajouter la logique pour le choix de  la cible et le tir (on veux éviter de tirer sur un avatar qui bouge,
            //cacher au joueur qui on veux cibler et diminuer la distance de tir)
            GetComponent<Launcher>().isShooting = true;
        }
        else
        {
            //ici ajouter la logique pour éviter les balles (essayer de deviner qui le joueur vas tirer, 
            //sélectionner cette cible, éviter les balles, augmenter la distance)
        }

    }
}
