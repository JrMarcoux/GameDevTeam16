using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class LevelGenerator : MonoBehaviour
{
    //Regroupement
    private Transform levelParent;
    private Transform playerAvatarsParent;
    private Transform enemiesParent;

    //Paramètres du terrain
    public GameObject[] Tiles;

    private int TileAmount;
    public int TileSize;

    public float WaitTime;

    //Paramètres du périmètre
    public GameObject Wall;
    private GameObject outBoundaries;

    private int XAmount;
    private int ZAmount;


    //Paramètres des avatars
    private int PlayerAvatarsAmount;
    private int EnemyAvatarsAmount;
    private int DecorAmount;
    public float SpawnHeight;
    public GameObject[] PlayerAvatars;
    public GameObject[] EnemyAvatars;
    public GameObject[] DecorObjects;

    //paramètres tableau Map
    private MapGraph.Node<MapElement>[,] map;
    private MapGraph graphStruct;
    private enum height {blue, green, yellow, black};
    private int indexX;
    private int indexZ;

    void Start()
    {
        //params du menu
        XAmount = PlayerPrefs.GetInt("levelWidth");
        ZAmount = PlayerPrefs.GetInt("levelDepth");
        TileAmount = XAmount * ZAmount;
        PlayerAvatarsAmount = PlayerPrefs.GetInt("nbCharacters");
        EnemyAvatarsAmount = PlayerPrefs.GetInt("nbEnemies");
        DecorAmount = PlayerPrefs.GetInt("nbDecors");

        //init
        levelParent = new GameObject().transform;
        levelParent.name = "LevelParent";
        playerAvatarsParent = new GameObject().transform;
        playerAvatarsParent.name = "playerAvatarsParent";
        enemiesParent = new GameObject().transform;
        enemiesParent.name = "enemiesParent";
        outBoundaries = GameObject.Find("AABB");
        outBoundaries.transform.localScale = (XAmount + 5) * TileSize * new Vector3(1,1,1);
        outBoundaries.transform.position = XAmount/2 * TileSize * new Vector3(1, 0, 1);
        map = new MapGraph.Node<MapElement>[XAmount, ZAmount];
        graphStruct = new MapGraph();
        indexX = 0;
        indexZ = 0;

        //generation du level
        StartCoroutine(GenerateLevel());
    }

    IEnumerator GenerateLevel()
    {
        for (int i = 1; i < TileAmount + 1; i++)
        {
            float GenPos = i;
            int Tile = Random.Range(0, Tiles.Length);
            //int Tile = Random.Range(0, 2) == 0 ? 1 : 3;
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
            //passe à la prochaine tuile en Z
            case 1:
                transform.position = new Vector3(0, 0, transform.position.z + TileSize);
                indexZ++;
                indexX = 0;
                break;
            //passe à la prochaine tuile en X
            case 2:
                transform.position = new Vector3(transform.position.x + TileSize, 0, transform.position.z);
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

        TileObject = Instantiate(Tiles[TileIndex], transform.position, transform.rotation) as GameObject;

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
        mapElement = new MapElement(TileObject.transform.position, heightnbr, TileSize);
        map[indexX, indexZ] = new MapGraph.Node<MapElement>(mapElement, 1);
    }

    //Génération des Murs du périmètre
    void CreateWall()
    {
        GameObject WallObject;

        transform.position = new Vector3(-TileSize, 0, -TileSize);
        
        //création mur bas
        for (int i = 0; i < XAmount + 1; i++)
        {
            transform.position = new Vector3(transform.position.x + TileSize, 0, transform.position.z);
            WallObject = Instantiate(Wall, transform.position, transform.rotation) as GameObject;
            WallObject.transform.parent = levelParent;
        }

        //création mur droit
        for (int i = 0; i < ZAmount + 1; i++)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z + TileSize);
            WallObject = Instantiate(Wall, transform.position, transform.rotation) as GameObject;
            WallObject.transform.parent = levelParent;
        }

        //création mur haut
        for (int i = 0; i < XAmount + 1; i++)
        {
            transform.position = new Vector3(transform.position.x - TileSize, 0, transform.position.z);
            WallObject = Instantiate(Wall, transform.position, transform.rotation) as GameObject;
            WallObject.transform.parent = levelParent;
        }

        //création mur gauche
        for (int i = 0; i < ZAmount + 1; i++)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z - TileSize);
            WallObject = Instantiate(Wall, transform.position, transform.rotation) as GameObject;
            WallObject.transform.parent = levelParent;
        }
    }


    //Génération les avatars du joueur
    void SpawnPlayerAvatars()
    {
        for(int i = 0; i < PlayerAvatarsAmount; i++)
        {
            GameObject TileObject;
            int TargetIndex = Random.Range(0, PlayerAvatars.Length);
            transform.position = new Vector3(Random.Range(0, XAmount * TileSize), SpawnHeight, Random.Range(0, ZAmount * TileSize));
            TileObject = Instantiate(PlayerAvatars[TargetIndex], transform.position, transform.rotation) as GameObject;
            TileObject.transform.parent = playerAvatarsParent;
        }
    }

    //Génération des avatars de l'enemi
    void SpawnEnemyAvatars()
    {
        for (int i = 0; i < EnemyAvatarsAmount; i++)
        {
            GameObject TileObject;
            int TargetIndex = Random.Range(0, EnemyAvatars.Length);
            transform.position = new Vector3(Random.Range(1, XAmount-1 * TileSize), SpawnHeight, Random.Range(0, ZAmount * TileSize));
            TileObject = Instantiate(EnemyAvatars[TargetIndex], transform.position, transform.rotation) as GameObject;
            TileObject.transform.parent = enemiesParent;
        }
    }

    //Génération des décors
    void SpawnDecorObjects()
    {
        for (int i = 0; i < DecorAmount; i++)
        {
            GameObject TileObject;
            int TargetIndex = Random.Range(0, DecorObjects.Length);
            transform.position = new Vector3(Random.Range(0, XAmount * TileSize), SpawnHeight, Random.Range(0, ZAmount * TileSize));
            map[(int)transform.position.x, (int)transform.position.z].Value.HasDecor = true;
            TileObject = Instantiate(DecorObjects[TargetIndex], transform.position, transform.rotation) as GameObject;
            TileObject.transform.parent = levelParent;
        }
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
        if (x < XAmount && x > 0 && z < ZAmount && z > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
