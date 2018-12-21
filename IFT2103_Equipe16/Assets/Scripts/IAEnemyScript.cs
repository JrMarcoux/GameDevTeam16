using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IAEnemyScript : MonoBehaviour
{

    private GameObject gameManager;
    private MapGraph graphStruct;
    private List<MapGraph.Node<MapElement>> graph;
    private powerBarScript powerBar;
    private bool hasShot = false;
    private bool isMoving;
    private const float timeBeforeShoot = 5.0f;
    private float timer = 0.0f;
    private bool timerDone;
    private bool firstAttack = false;
    private bool firstDefense = false;
    private bool moveOnce = false;
    private bool moveOnceDef = false;
    private bool difficultyIsHard;
    private GameObject firstCheckAtt;
    private Vector3 firstCheckPosAtt;
    private GameObject firstCheckDef;
    private Vector3 firstCheckPosDef;
    private Coroutine attack = null;
    private Coroutine defense = null;
    private int maxDistance;
    private bool continueScript = false;
    IEnumerator easyShot = null;
    

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game manager");
        graphStruct = gameManager.GetComponent<LevelGenerator>().getGraph();
        graph = graphStruct.NodeSet;
        difficultyIsHard = System.Convert.ToBoolean(PlayerPrefs.GetInt("difficulty"));
        maxDistance = PlayerPrefs.GetInt("levelWidth");
    }

    void Update()
    {
        // vérifie s'il reste des joueur
        if (GetClosestPlayer() != null)
        {
            if (gameManager.GetComponent<GameManager>().teamTurn == gameManager.GetComponent<GameManager>().enemiesTag)
            {
                if(!hasShot)
                {
                    hasShot = true;
                    Debug.Log("son tour");
                    firstDefense = false;
                    easyShot = EasyShot();
                    StartCoroutine(easyShot);
                }
                //if (!firstAttack)
                //{
                //    firstAttack = true;
                //    if (attack != null)
                //    {
                //        StopCoroutine(attack);
                //        attack = null;
                //    }
                //    firstDefense = false;
                //    moveOnce = false;
                //    isMoving = true;
                //    timerDone = false;
                //    hasShot = false;
                //    timer = 0.0f;

                //    GameObject bestCharacter = GetCharacter();
                //    if (this.gameObject != bestCharacter)
                //    {
                //        ChgCharacter(bestCharacter);
                //    }
                //    else
                //    {
                //        continueScript = true;
                //    }

                //    // met le joueur le plus proche comme target
                //    firstCheckAtt = GetClosestPlayer();
                //    firstCheckPosAtt = ConvertPositionToIndex(firstCheckAtt.transform.position);
                //    ChgTarget(firstCheckAtt);

                //    // évite que l'image de la caméra tremble
                //    GetComponent<BoxCollider>().enabled = false;
                //    GetComponent<Rigidbody>().useGravity = false;
                //}
                //if (continueScript)
                //{
                //    // démarre le behaviour d'attaque
                //    StartCoroutine(AttackBehaviour());
                //}
            }
            else
            {
                if(!firstDefense)
                {
                    firstDefense = true;
                    Debug.Log("pas ton tour");
                    hasShot = false;
                    if (easyShot != null)
                    {
                        StopCoroutine(easyShot);
                    }
                }
                
                //if (!firstDefense)
                //{
                //    firstDefense = true;
                //    if (defense != null)
                //    {
                //        StopCoroutine(defense);
                //        defense = null;
                //    }
                //    firstAttack = false;
                //    moveOnceDef = false;
                //    continueScript = false;
                //    // prend la position du joueur qui peut bouger/tirer
                //    firstCheckDef = gameManager.GetComponent<GameManager>().GetSelectedCharacter(gameManager.GetComponent<GameManager>().playerTag);
                //    firstCheckPosDef = ConvertPositionToIndex(firstCheckDef.transform.position);
                //}
                //// démarre le behaviour de défense
                //StartCoroutine(DefenseBehaviour());
            }
        }
    }

    private IEnumerator EasyShot()
    {
        yield return StartCoroutine(WaitShot(true));
    }

    private GameObject GetCharacter()
    {
        List<GameObject> playersAlive = gameManager.GetComponent<GameManager>().playerAvatarsAlive;
        List<GameObject> enemiesAlive = gameManager.GetComponent<GameManager>().enemiesAlive;
        float distance = int.MaxValue;
        GameObject character = this.gameObject;

        foreach (var player in playersAlive)
        {
            foreach (var enemy in enemiesAlive)
            {
                if (Mathf.Abs(Vector3.Distance(player.transform.position, enemy.transform.position)) < distance)
                {
                    character = enemy;
                    distance = Mathf.Abs(Vector3.Distance(player.transform.position, enemy.transform.position));
                }
            }
        }

        return character;
    }

    private IEnumerator AttackBehaviour()
    {
        GameObject closest = GetClosestPlayer();
        Vector3 closestPos = ConvertPositionToIndex(closest.transform.position);

        // si le joueur le plus proche bouge
        if (closestPos != firstCheckPosAtt)
        {
            // si le joueur le plus proche devient un autre joueur
            if (closest != firstCheckAtt)
            {
                ChgTarget(closest);
                firstCheckAtt = closest;
            }
            firstCheckPosAtt = closestPos;
            moveOnce = false;
        }
        if (!moveOnce)
        {
            // bouger l'IA
            moveOnce = true;
            if (attack != null)
            {
                StopCoroutine(attack);
                attack = null;
            }
            yield return attack = StartCoroutine(MovePathAttack(firstCheckAtt));
        }
        if (!timerDone)
        {
            //le temps imparti à l'IA est écoulé
            if (timer >= timeBeforeShoot)
            {
                if (attack != null)
                {
                    StopCoroutine(attack);
                    attack = null;
                }
                isMoving = false;
                timerDone = true;

            }
            else
            {
                timer += Time.deltaTime;
            }
        }

        //si l'IA est prête à tirer
        if (!isMoving && !hasShot)
        {
            hasShot = true;
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;
            yield return StartCoroutine(Shoot(firstCheckAtt));
        }
        yield return null;
    }

    private IEnumerator DefenseBehaviour()
    {
        // si le joueur change de cible
        GameObject targeted = gameManager.GetComponent<GameManager>().GetTargetedCharacter(gameManager.GetComponent<GameManager>().playerTag);
        if (targeted != this.gameObject)
        {
            if (defense != null)
            {
                StopCoroutine(defense);
                defense = null;
            }
            ChgCharacter();
        }
        else
        {
            // récupère les informations du joueur qui peut bouger/tirer
            GameObject selectedPlayer = gameManager.GetComponent<GameManager>().GetSelectedCharacter(gameManager.GetComponent<GameManager>().playerTag);
            Vector3 playerPos = ConvertPositionToIndex(selectedPlayer.transform.position);

            // si le joueur qui target l'IA bouge
            if (firstCheckPosDef != playerPos)
            {
                if (firstCheckDef != selectedPlayer)
                {
                    firstCheckDef = selectedPlayer;
                }
                firstCheckPosDef = playerPos;
                moveOnceDef = false;
            }
            if (!moveOnceDef)
            {
                moveOnceDef = true;
                if (defense != null)
                {
                    StopCoroutine(defense);
                    defense = null;
                }

                // obtenir les info concernant l'objet décor le plus proche de l'IA
                GameObject closestDecor = GetClosestDecor();
                Vector3 decorPos = ConvertPositionToIndex(closestDecor.transform.position);
                int x = (int)decorPos.x;
                int z = (int)decorPos.z;

                // ajouter les positions possibles autour d'un objet décor
                List<Vector3> decorPositions = new List<Vector3>();
                decorPositions = AddVectorToList(x + 1, z, decorPositions);
                decorPositions = AddVectorToList(x - 1, z, decorPositions);
                decorPositions = AddVectorToList(x, z + 1, decorPositions);
                decorPositions = AddVectorToList(x, z - 1, decorPositions);
                decorPositions = AddVectorToList(x + 1, z + 1, decorPositions);
                decorPositions = AddVectorToList(x + 1, z - 1, decorPositions);
                decorPositions = AddVectorToList(x - 1, z + 1, decorPositions);
                decorPositions = AddVectorToList(x + 1, z - 1, decorPositions);


                // cherche la position autour d'un objet de décor la plus loin du joueur (ce place derrière l'objet)
                Vector3 farthestPos = firstCheckPosDef;
                foreach (var position in decorPositions)
                {
                    if (Vector3.Distance(firstCheckPosDef, position) > Vector3.Distance(firstCheckPosDef, farthestPos))
                    {
                        farthestPos = position;
                    }
                }
                decorPos = farthestPos;

                yield return defense = StartCoroutine(MovePathDefense(decorPos));
            }
        }

        yield return null;
    }

    /// <summary>
    /// ajoute un vecteur à la liste
    /// </summary>
    /// <param name="x">la position en x</param>
    /// <param name="z">la position en z</param>
    /// <param name="list">la liste à ajouter un élément</param>
    /// <returns></returns>
    private List<Vector3> AddVectorToList(int x, int z, List<Vector3> list)
    {
        if (gameManager.GetComponent<LevelGenerator>().PositionInMapExist(x, z))
        {
            list.Add(new Vector3(x, 0, z));
        }
        return list;
    }

    /// <summary>
    /// crée un chemin et le fais suivre par l'IA
    /// </summary>
    /// <param name="closest">le joueur le plus proche à approcher</param>
    /// <returns></returns>
    private IEnumerator MovePathAttack(GameObject closest)
    {
        // récupère les positions du joueur et de l'IA
        Vector3 posPlayerIndex = ConvertPositionToIndex(closest.transform.position);
        Vector3 posIndex = ConvertPositionToIndex(transform.position);

        // recherche le meilleur chemin pour ce rapprocher du joueur
        List<int> bestPath = SearchPath(posIndex, posPlayerIndex);

        if (bestPath.Count > 6)
        {
            bestPath.RemoveRange(bestPath.Count - 6, 6);
        }
        else if (bestPath.Count > 2)
        {
            bestPath.RemoveRange(bestPath.Count - 3, 3);
        }
        else
        {
            try
            {
                bestPath.RemoveRange(bestPath.Count - 1, 1);
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }

        // effectue les déplacements case par case
        for (int i = 0; i < bestPath.Count - 1; i++)
        {
            Vector3 pos = ConvertPositionWithHeight(graph[bestPath[i]].Value.Position, graph[bestPath[i]].Value.Height);
            Vector3 nextPos = ConvertPositionWithHeight(graph[bestPath[i + 1]].Value.Position, graph[bestPath[i + 1]].Value.Height);

            yield return StartCoroutine(Move(pos, nextPos));
        }
    }

    /// <summary>
    /// crée un chemin et le fais suivre à l'IA
    /// </summary>
    /// <param name="closest">l'objet de décor le plus proche à atteindre</param>
    /// <returns></returns>
    private IEnumerator MovePathDefense(Vector3 closest)
    {
        // récupère les infos de l'objet décor et de l'IA
        Vector3 posDecorIndex = ConvertPositionToIndex(closest);
        Vector3 posIndex = ConvertPositionToIndex(transform.position);

        //recherche le meilleur chemin pour ce rapprocher de l'objet de décor
        List<int> bestPath = SearchPath(posIndex, posDecorIndex);

        // effetue les déplacements case par case
        for (int i = 0; i < bestPath.Count - 1; i++)
        {
            Vector3 pos = ConvertPositionWithHeight(graph[bestPath[i]].Value.Position, graph[bestPath[i]].Value.Height);
            Vector3 nextPos = ConvertPositionWithHeight(graph[bestPath[i + 1]].Value.Position, graph[bestPath[i + 1]].Value.Height);

            yield return StartCoroutine(Move(pos, nextPos));
        }
    }

    /// <summary>
    /// Permet à l'IA de bouger d'une case
    /// </summary>
    /// <param name="begin">la position d'origine</param>
    /// <param name="end">la position à atteindre</param>
    /// <returns></returns>
    private IEnumerator Move(Vector3 begin, Vector3 end)
    {
        float duration = 1.0f;

        for (float t = 0.0f; t <= duration; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(begin, end, t / duration);
            yield return null;
        }
        transform.position = end;
    }

    /// <summary>
    /// permet d'effectuer une probabilité de tir
    /// </summary>
    /// <returns></returns>
    private IEnumerator Shoot(GameObject target)
    {
        bool isBest = false;
        float distance = 0;

        distance = Mathf.Abs(Vector3.Distance(target.transform.position, transform.position));

        // si la difficulté est à difficile
        if (difficultyIsHard)
        {
            // augmenter la probabilité en fonction de la distance et la cible
            if (distance >= (0.8 * maxDistance))
            {
                isBest = Random.Range(1, 4) == 3 ? true : false;
            }
            else if ((0.8 * maxDistance) > distance && distance >= (0.4 * maxDistance))
            {
                isBest = Random.Range(1, 3) == 2 ? true : false;
            }
            else if (distance < (0.4 * maxDistance))
            {
                isBest = Random.Range(1, 2) == 1 ? true : false;
            }
        }
        else
        {
            // augmenter la probabilité en fonction de la distance et la cible
            if (distance >= (0.8 * maxDistance))
            {
                isBest = Random.Range(1, 7) == 4 ? true : false;
            }
            else if ((0.8 * maxDistance) > distance && distance >= (0.4 * maxDistance))
            {
                isBest = Random.Range(1, 6) == 3 ? true : false;
            }
            else if (distance < (0.4 * maxDistance))
            {
                isBest = Random.Range(1, 5) == 2 ? true : false;
            }
        }
        yield return StartCoroutine(WaitShot(isBest));
    }

    /// <summary>
    /// attend avant de tirer pour soit atteindre la cible (best) ou attendre x seconde pour rajouter du random
    /// </summary>
    /// <param name="isBest">Si le tir sera réussis</param>
    /// <returns></returns>
    private IEnumerator WaitShot(bool isBest)
    {
        bool shoot = false;
        float offset = 0.0f;
        powerBar = GameObject.Find("powerBar").GetComponent<powerBarScript>();

        if (isBest)
        {
            // attend pour que le tir soit parfait en fonction de la power bar
            while (!shoot)
            {
                offset = powerBar.GetAmount(); //valeur de la powerbar
                if (offset > 0.4 && offset < 0.6)
                {
                    shoot = true;
                }
                else
                {
                    yield return null;
                }
            }
        }
        else
        {
            // attend un nombre de seconde avant de tirer pour randomizer plus
            float rdm = Random.Range(1, 3);
            yield return new WaitForSeconds(rdm);
        }
        this.GetComponents<AudioSource>()[0].Play();
        GetComponent<Launcher>().isShooting = true;
    }

    /// <summary>
    /// change la target de l'IA
    /// </summary>
    /// <param name="target">pour spécifier un joueur</param>
    private void ChgTarget(GameObject target = null)
    {
        gameManager.GetComponent<GameManager>().changeSelectTarget(gameManager.GetComponent<GameManager>().enemiesTag, target);
    }

    /// <summary>
    /// pour changer l'IA controlable
    /// </summary>
    private void ChgCharacter(GameObject character = null)
    {
        gameManager.GetComponent<GameManager>().changeSelectCharacter(gameManager.GetComponent<GameManager>().enemiesTag, character);
    }

    /// <summary>
    /// obtiens le joueur le plus proche de l'IA
    /// </summary>
    /// <returns>le joueur le plus proche</returns>
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

    /// <summary>
    /// obtiens l'objet de décor le plus proche de l'IA
    /// </summary>
    /// <returns>l'objet de décor le plus proche</returns>
    private GameObject GetClosestDecor()
    {
        GameObject[] decors = GameObject.FindGameObjectsWithTag("Decor");
        GameObject closestDecor = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (var decor in decors)
        {
            Vector3 directionToTarget = decor.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestDecor = decor;
            }
        }
        return closestDecor;
    }

    /// <summary>
    /// algorithme de Dijkstra permettant de trouver le chemin le plus court
    /// </summary>
    /// <param name="start">la positition de départ</param>
    /// <param name="end">la position à atteindre</param>
    /// <returns></returns>
    private List<int> SearchPath(Vector3 start, Vector3 end)
    {
        Queue<IndexNode> queue = new Queue<IndexNode>();// la file avec les noeuds du graph
        List<int> shortestPath = new List<int>();// la liste contenant les index des cases à parcourir
        int[] distance = new int[graph.Count];
        int[] parent = new int[graph.Count];
        int startindex = graph.Find(x => x.Value.Position == start).Index;// l'index de la position de départ
        int endIndex = graph.Find(x => x.Value.Position == end).Index;// l'index de la position d'arriver

        for (int i = 0; i < graph.Count; i++)
        {
            distance[i] = int.MaxValue;
            parent[i] = int.MaxValue;
        }

        // ajouter le noeud de départ dans la file
        distance[startindex] = 0;
        queue.Enqueue(new IndexNode(startindex, distance[startindex]));

        // tant qu'on a pas visiter toutes les cases
        while (queue.Count != 0)
        {
            // obtenir le noued avec le plus petit poids
            queue = new Queue<IndexNode>(queue.OrderBy(x => x.Weight));
            IndexNode minimumNode = queue.First();
            int min = int.MaxValue;
            if (minimumNode.Weight == min)
                break;
            int uStar = minimumNode.Index;
            queue.Dequeue();

            if (uStar == endIndex) break;

            // boucle au travers des cases accessibles par la case courante
            for (int i = 0; i < graph[uStar].Childs.Count; i++)
            {
                // ajouter le noeud avec le plus petit poids
                int temp = distance[uStar] + graph[uStar].Childs[i].Weight;
                if (temp < distance[graph[uStar].Childs[i].Index])
                {
                    distance[graph[uStar].Childs[i].Index] = temp;
                    parent[graph[uStar].Childs[i].Index] = uStar;
                    queue.Enqueue(new IndexNode(graph[uStar].Childs[i].Index, temp));
                }
            }
        }

        //impossible d'atteindre la fin
        if (parent[endIndex] == int.MaxValue)
        {
            shortestPath.Add(endIndex);
            return shortestPath;
        }

        // partie qui inverse le chemin pour obtenir du début à la fin
        Stack<int> pathStack = new Stack<int>();// la pile qui contient les noeuds pour ce rendre à la fin à l'envers
        int nbr = endIndex;
        pathStack.Push(nbr);
        while (parent[nbr] != int.MaxValue)
        {
            nbr = parent[nbr];
            pathStack.Push(nbr);
        }
        while (pathStack.Count != 0)
        {
            // ajoute les noeuds dans le bon sens
            int temp = pathStack.First();
            shortestPath.Add(temp);
            pathStack.Pop();
        }

        return shortestPath;
    }

    /// <summary>
    /// change la hauteur en y en fonction de la hauteur des case
    /// </summary>
    /// <param name="position">la position de la case</param>
    /// <param name="height">son index de hauteur</param>
    /// <returns></returns>
    private Vector3 ConvertPositionWithHeight(Vector3 position, int height)
    {
        switch (height)
        {
            case 0:
                return new Vector3(position.x, 0.9f, position.z);
            case 1:
                return new Vector3(position.x, 1f, position.z);
            case 2:
                return new Vector3(position.x, 1.1f, position.z);
            case 3:
                return new Vector3(position.x, 1.4f, position.z);
            default:
                return position;
        }
    }

    /// <summary>
    /// converti une position en index de tableau
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private Vector3 ConvertPositionToIndex(Vector3 position)
    {
        return new Vector3((int)Mathf.Floor(position.x), 0, (int)Mathf.Floor(position.z));
    }

    /// <summary>
    /// classe utilisé pour la méthode de recherche (les noeuds)
    /// </summary>
    private class IndexNode
    {
        private int index;
        private int weight;

        public IndexNode(int index, int weight)
        {
            this.Index = index;
            this.Weight = weight;
        }

        public int Index
        {
            get
            {
                return index;
            }

            set
            {
                index = value;
            }
        }

        public int Weight
        {
            get
            {
                return weight;
            }

            set
            {
                weight = value;
            }
        }
    }
}
