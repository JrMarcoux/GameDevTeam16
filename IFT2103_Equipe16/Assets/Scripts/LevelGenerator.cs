using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    //Regroupement
    private Transform parent;

    //Paramètres du terrain
    public GameObject[] Tiles;

    public int TileAmount;
    public int TileSize;

    public float WaitTime;

    //Paramètres du périmètre
    public GameObject Wall;
    private GameObject outBoundaries;

    public float XAmount;
    public float ZAmount;


    //Paramètres des avatars
    public int PlayerAvatarsAmount;
    public int EnemyAvatarsAmount;
    public int DecorAmount;
    public float SpawnHeight;
    public GameObject[] PlayerAvatars;
    public GameObject[] EnemyAvatars;
    public GameObject[] DecorObjects;

    void Start()
    {
        parent = new GameObject().transform;
        parent.name = "LevelParent";
        outBoundaries = GameObject.Find("AABB");
        outBoundaries.transform.localScale = (XAmount + 5) * TileSize * new Vector3(1,1,1);
        outBoundaries.transform.position = XAmount/2 * TileSize * new Vector3(1, 0, 1);
        StartCoroutine(GenerateLevel());
    }

    IEnumerator GenerateLevel()
    {
        ZAmount = TileAmount / XAmount;

        for (int i = 1; i < TileAmount + 1; i++)
        {
            float GenPos = i;
            int Tile = Random.Range(0, Tiles.Length);

            CreateTile(Tile);
            CallMoveGen(GenPos);

            //yield return new WaitForSeconds(WaitTime);

            if (i == TileAmount)
            {
                CreateWall();
                SpawnPlayerAvatars();
                SpawnEnemyAvatars();
                SpawnDecorObjects();
            }

        }
        GetComponent<GameManager>().enabled = true;

        yield return 0;
    }

    //Mouvement du générateur pour le terrain
    void CallMoveGen(float GenPos)
    {
        if (GenPos == 0)
        {
            MoveGen(0);
        }
        else if (GenPos % XAmount == 0)
        {
            MoveGen(1);
        }
        else
        {
            MoveGen(2);
        }

    }

    void MoveGen(int dir)
    {
        switch (dir)
        {
            case 0:
                
                break;

            case 1:
                transform.position = new Vector3(0, 0, transform.position.z + TileSize);
                break;
            case 2:
                transform.position = new Vector3(transform.position.x + TileSize, 0, transform.position.z);
                break;

        }

    }

    //Génération d'un bloc de terrain
    void CreateTile(int TileIndex)
    {
        GameObject TileObject;

        TileObject = Instantiate(Tiles[TileIndex], transform.position, transform.rotation) as GameObject;

        TileObject.transform.parent = parent;
    }

    //Génération des Murs du périmètre
    void CreateWall()
    {
        GameObject WallObject;

        transform.position = new Vector3(-TileSize, 0, -TileSize);

        for (int i = 0; i < XAmount + 1; i++)
        {
            transform.position = new Vector3(transform.position.x + TileSize, 0, transform.position.z);
            WallObject = Instantiate(Wall, transform.position, transform.rotation) as GameObject;
            WallObject.transform.parent = parent;
        }

        for (int i = 0; i < ZAmount + 1; i++)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z + TileSize);
            WallObject = Instantiate(Wall, transform.position, transform.rotation) as GameObject;
            WallObject.transform.parent = parent;
        }

        for (int i = 0; i < XAmount + 1; i++)
        {
            transform.position = new Vector3(transform.position.x - TileSize, 0, transform.position.z);
            WallObject = Instantiate(Wall, transform.position, transform.rotation) as GameObject;
            WallObject.transform.parent = parent;
        }

        for (int i = 0; i < ZAmount + 1; i++)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z - TileSize);
            WallObject = Instantiate(Wall, transform.position, transform.rotation) as GameObject;
            WallObject.transform.parent = parent;
        }
    }


    //Génération les avatars du joueur
    void SpawnPlayerAvatars()
    {
        for(int i = 0; i < PlayerAvatarsAmount; i++)
        {
            int TargetIndex = Random.Range(0, PlayerAvatars.Length);
            transform.position = new Vector3(Random.Range(0, XAmount * TileSize), SpawnHeight, Random.Range(0, ZAmount * TileSize));
            Instantiate(PlayerAvatars[TargetIndex], transform.position, transform.rotation);
        }
    }

    //Génération des avatars de l'enemi
    void SpawnEnemyAvatars()
    {
        for (int i = 0; i < EnemyAvatarsAmount; i++)
        {
            int TargetIndex = Random.Range(0, EnemyAvatars.Length);
            transform.position = new Vector3(Random.Range(0, XAmount * TileSize), SpawnHeight, Random.Range(0, ZAmount * TileSize));
            Instantiate(EnemyAvatars[TargetIndex], transform.position, transform.rotation);
        }
    }

    //Génération des décors
    void SpawnDecorObjects()
    {
        for (int i = 0; i < DecorAmount; i++)
        {
            int TargetIndex = Random.Range(0, DecorObjects.Length);
            transform.position = new Vector3(Random.Range(0, XAmount * TileSize), SpawnHeight, Random.Range(0, ZAmount * TileSize));
            Instantiate(DecorObjects[TargetIndex], transform.position, transform.rotation);
        }
    }
}
