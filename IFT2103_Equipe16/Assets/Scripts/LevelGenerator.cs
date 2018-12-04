using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.Scripts;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    //Regroupement
    private Transform levelParent;
    private Transform playerAvatarsParent;
    private Transform enemiesParent;

    //Paramètres du terrain
    public GameObject[] tiles;

    private int tileAmount;
    public int tileSize;

    public float WaitTime;

    //Paramètres du périmètre
    public GameObject wall;
    private GameObject outBoundaries;

    private int xAmount;
    private int zAmount;


    //Paramètres des avatars
    private int playerAvatarsAmount;
    private int enemyAvatarsAmount;
    private int decorAmount;
    public float spawnHeight;
    public GameObject[] playerAvatars;
    public GameObject[] enemyAvatars;
    public GameObject[] decorObjects;

    //paramètres tableau Map
    private MapGraph.Node<MapElement>[,] map;
    private MapGraph graphStruct;
    private enum height {blue, green, yellow, black};
    private int indexX;
    private int indexZ;

    void Start()
    {
        //params du menu
        xAmount = PlayerPrefs.GetInt("levelWidth");
        zAmount = PlayerPrefs.GetInt("levelDepth");
        tileAmount = xAmount * zAmount;
        playerAvatarsAmount = PlayerPrefs.GetInt("nbCharacters");
        enemyAvatarsAmount = PlayerPrefs.GetInt("nbEnemies");
        decorAmount = PlayerPrefs.GetInt("nbDecors");

        //init
        levelParent = new GameObject().transform;
        levelParent.name = "LevelParent";
        playerAvatarsParent = new GameObject().transform;
        playerAvatarsParent.name = "playerAvatarsParent";
        enemiesParent = new GameObject().transform;
        enemiesParent.name = "enemiesParent";
        outBoundaries = GameObject.Find("AABB");
        outBoundaries.transform.localScale = (xAmount + 5) * tileSize * new Vector3(1,1,1);
        outBoundaries.transform.position = xAmount/2 * tileSize * new Vector3(1, 0, 1);
        map = new MapGraph.Node<MapElement>[xAmount, zAmount];
        graphStruct = new MapGraph();
        indexX = 0;
        indexZ = 0;

        //generation du level
        StartCoroutine(GenerateLevel());
    }

    IEnumerator GenerateLevel()
    {
        for (int i = 1; i < tileAmount + 1; i++)
        {
            float GenPos = i;
            int Tile = Random.Range(0, tiles.Length);
            //int Tile = Random.Range(0, 2) == 0 ? 1 : 3;
            CreateTile(Tile);
            CallMoveGen(GenPos);

            //yield return new WaitForSeconds(WaitTime);

            if (i == tileAmount)
            {
                CreateWall();
                SpawnPlayerAvatars();
                SpawnEnemyAvatars();
                SpawnDecorObjects();
            }

        }
        
        GetComponent<GameManager>().enabled = true;
        CreateGraph();

        yield return 0;
    }

    //Mouvement du générateur pour le terrain
    void CallMoveGen(float GenPos)
    {
        if (GenPos == 0)
        {
            MoveGen(0);
        }
        else if (GenPos % xAmount == 0)
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
            //passe à la prochaine tuile en Z
            case 1:
                transform.position = new Vector3(0, 0, transform.position.z + tileSize);
                indexZ++;
                indexX = 0;
                break;
            //passe à la prochaine tuile en X
            case 2:
                transform.position = new Vector3(transform.position.x + tileSize, 0, transform.position.z);
                indexX++;
                break;

        }

    }

    //Génération d'un bloc de terrain
    void CreateTile(int TileIndex)
    {
        GameObject TileObject;
        MapElement mapElement;
        int heightnbr = 0;
        string material;

        TileObject = Instantiate(tiles[TileIndex], transform.position, transform.rotation) as GameObject;

        material = TileObject.GetComponent<Renderer>().sharedMaterial.name.ToUpper();
        TileObject.transform.parent = levelParent;
        switch (material)
        {
            case "WALL":
                heightnbr = (int)height.black;
                break;
            case "BLUE":
                heightnbr = (int)height.blue;
                break;
            case "PLATFORM":
                heightnbr = (int)height.green;
                break;
            case "YELLOW":
                heightnbr = (int)height.yellow;
                break;
        }
        mapElement = new MapElement(TileObject.transform.position, heightnbr, tileSize);
        map[indexX, indexZ] = new MapGraph.Node<MapElement>(mapElement, 1);
    }

    //Génération des Murs du périmètre
    void CreateWall()
    {
        GameObject WallObject;

        transform.position = new Vector3(-tileSize, 0, -tileSize);
        
        //création mur bas
        for (int i = 0; i < xAmount + 1; i++)
        {
            transform.position = new Vector3(transform.position.x + tileSize, 0, transform.position.z);
            WallObject = Instantiate(wall, transform.position, transform.rotation) as GameObject;
            WallObject.transform.parent = levelParent;
        }

        //création mur droit
        for (int i = 0; i < zAmount + 1; i++)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z + tileSize);
            WallObject = Instantiate(wall, transform.position, transform.rotation) as GameObject;
            WallObject.transform.parent = levelParent;
        }

        //création mur haut
        for (int i = 0; i < xAmount + 1; i++)
        {
            transform.position = new Vector3(transform.position.x - tileSize, 0, transform.position.z);
            WallObject = Instantiate(wall, transform.position, transform.rotation) as GameObject;
            WallObject.transform.parent = levelParent;
        }

        //création mur gauche
        for (int i = 0; i < zAmount + 1; i++)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z - tileSize);
            WallObject = Instantiate(wall, transform.position, transform.rotation) as GameObject;
            WallObject.transform.parent = levelParent;
        }
    }


    //Génération les avatars du joueur
    void SpawnPlayerAvatars()
    {
        for(int i = 0; i < playerAvatarsAmount; i++)
        {
            GameObject TileObject;
            int TargetIndex = Random.Range(0, playerAvatars.Length);
            transform.position = new Vector3(Random.Range(0, xAmount * tileSize), spawnHeight, Random.Range(0, zAmount * tileSize));
            TileObject = Instantiate(playerAvatars[TargetIndex], transform.position, transform.rotation) as GameObject;
            TileObject.transform.parent = playerAvatarsParent;
        }
    }

    //Génération des avatars de l'enemi
    void SpawnEnemyAvatars()
    {
        for (int i = 0; i < enemyAvatarsAmount; i++)
        {
            GameObject TileObject;
            int TargetIndex = Random.Range(0, enemyAvatars.Length);
            transform.position = new Vector3(Random.Range(1, xAmount-1 * tileSize), spawnHeight, Random.Range(0, zAmount * tileSize));
            TileObject = Instantiate(enemyAvatars[TargetIndex], transform.position, transform.rotation) as GameObject;
            TileObject.transform.parent = enemiesParent;
        }
    }

    //Génération des décors
    void SpawnDecorObjects()
    {
        List<Vector3> pastDecors = new List<Vector3>();
        for (int i = 0; i < decorAmount; i++)
        {
            GameObject TileObject;
            int TargetIndex = Random.Range(0, decorObjects.Length);           
            Vector3 positionDecor = GetSeed(tileSize, pastDecors, xAmount * tileSize);
            if(positionDecor != new Vector3(0,0,0))
            {
                pastDecors.Add(positionDecor);
                transform.position = positionDecor;
                map[(int)transform.position.x, (int)transform.position.z].Value.HasDecor = true;
                TileObject = Instantiate(decorObjects[TargetIndex], transform.position, transform.rotation) as GameObject;
                TileObject.transform.parent = levelParent;
            }           
        }
    }

    private Vector3 GetSeed(float minDist, List<Vector3> pastSeed, int maxLoop)
    {
        int nbLoop = 0;
        Vector3 seed = new Vector3(Random.Range(0, xAmount * tileSize), spawnHeight, Random.Range(0, zAmount * tileSize));
        for (int i = 0; i < pastSeed.Count; i++)
        {
            nbLoop++;
            if(Vector3.Distance(seed, pastSeed[i]) <= minDist && maxLoop>nbLoop)
            {
                seed = GetSeed(minDist, pastSeed, maxLoop-nbLoop);
            }
            else if(maxLoop < nbLoop)
            {
                return new Vector3(0,0,0);
            }
        }
        return seed;
    }

    private void CreateGraph()
    {
        for (int z = 0; z < map.GetLength(1); z++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                graphStruct.AddNode(map[x, z]);

                if (x > 0)
                {
                    if (map[x, z].Value.Height >= map[x - 1, z].Value.Height || (map[x - 1, z].Value.Height - map[x, z].Value.Height) < 3)
                    {
                        if (!map[x - 1, z].Value.HasDecor)
                        {
                            graphStruct.ConnectNode(map[x, z], map[x - 1, z]);
                        }
                    }
                }
                if (x < map.GetLength(0) - 1)
                {
                    if (map[x, z].Value.Height >= map[x + 1, z].Value.Height || (map[x + 1, z].Value.Height - map[x, z].Value.Height) < 3)
                    {
                        if (!map[x + 1, z].Value.HasDecor)
                        {
                            graphStruct.ConnectNode(map[x, z], map[x + 1, z]);
                        }
                    }
                }

                if (z > 0)
                {
                    if (map[x, z].Value.Height >= map[x, z - 1].Value.Height || (map[x, z - 1].Value.Height - map[x, z].Value.Height) < 3)
                    {
                        if (!map[x, z - 1].Value.HasDecor)
                        {
                            graphStruct.ConnectNode(map[x, z], map[x, z - 1]);
                        }
                    }
                }
                if (z < map.GetLength(1) - 1)
                {
                    if (map[x, z].Value.Height >= map[x, z + 1].Value.Height || (map[x, z + 1].Value.Height - map[x, z].Value.Height) < 3)
                    {
                        if (!map[x, z + 1].Value.HasDecor)
                        {
                            graphStruct.ConnectNode(map[x, z], map[x, z + 1]);
                        }
                    }
                }

            }
        }
    }

    public MapGraph.Node<MapElement>[,] GetMapArray()
    {
        return map;
    }

    public MapGraph getGraph()
    {
        return graphStruct;
    }

    public bool PositionInMapExist(int x, int z)
    {
        if (x < xAmount && x > 0 && z < zAmount && z > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
