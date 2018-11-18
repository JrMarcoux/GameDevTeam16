using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEnemyScript : MonoBehaviour
{

    private GameObject gameManager;
    private MapElement[,] map;
    public float speed = 1f;
    public float jumpSpeed = 3f;
    private float delayJump = 0;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game manager");
        map = gameManager.GetComponent<LevelGenerator>().GetMapArray();
        /*Debug.Log("x : "+map.GetLength(0));
        Debug.Log("z : "+map.GetLength(1));
        for (int z = 0; z < map.GetLength(1); z++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                Debug.Log("position : " + map[x,z].Position);
                Debug.Log("height : " + map[x, z].Height);
                Debug.Log("has Decor : " + map[x, z].HasDecor);
                Debug.Log("--------------------");
            }
        }*/
    }

    void Update()
    {
        //pour controler l'enemie avec un autre script (multijoueur), changer le getComponent<script> dans le gamemanager
        //les appels de fonctions devraient ressembler à ceux de controlPlayer pour plus de réalisme
        if (gameManager.GetComponent<GameManager>().teamTurn == gameManager.GetComponent<GameManager>().enemiesTag)
        {
            //ici ajouter la logique pour le choix de  la cible et le tir (on veux éviter de tirer sur un avatar qui bouge,
            //cacher au joueur qui on veux cibler et diminuer la distance de tir)
            //GetComponent<Launcher>().isShooting = true;
            AttackBehaviour();

        }
        else
        {
            //ici ajouter la logique pour éviter les balles (essayer de deviner qui le joueur vas tirer, 
            //sélectionner cette cible, éviter les balles, augmenter la distance)
        }

    }

    private void AttackBehaviour()
    {
        Shoot();
    }

    private void Down()
    {
        transform.Translate(0f, 0f, -speed * Time.deltaTime);
    }

    private void Up()
    {
        transform.Translate(0f, 0f, speed * Time.deltaTime);
    }

    private void Left()
    {
        transform.Translate(-speed * Time.deltaTime, 0f, 0f);
    }

    private void Right()
    {
        transform.Translate(speed * Time.deltaTime, 0f, 0f);
    }

    private void Jump()
    {
        if (Time.time > delayJump)
        {
            speed = speed * -1;
            GetComponent<Rigidbody>().velocity += jumpSpeed * Vector3.up;
            delayJump = Time.time + 0.8f;
        }
        else
        {
            speed = speed * -1;
        }
    }

    private void Shoot()
    {
        GetComponent<Launcher>().isShooting = true;
    }

    private void ChgTarget()
    {
        gameManager.GetComponent<GameManager>().changeSelectTarget(gameManager.GetComponent<GameManager>().enemiesTag);
    }

    private void ChgCharacter()
    {
        gameManager.GetComponent<GameManager>().changeSelectCharacter(gameManager.GetComponent<GameManager>().enemiesTag);
    }

    private GameObject GetClosestPlayer()
    {
        List<GameObject> playersAlive = gameManager.GetComponent<GameManager>().playerAvatarsAlive;
        GameObject closestPlayer = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (var player in playersAlive)
        {
            Vector3 directionToTarget = player.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestPlayer = player;
            }
        }
        return closestPlayer;
    }

    private void GetTallObstacle()
    {

    }

    void OnTriggerStay(Collider col)
    {

    }
}
