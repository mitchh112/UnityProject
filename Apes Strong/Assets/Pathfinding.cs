using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    public float speed;
    //VAR PRIV
    private Node[] allNodes;
    private Vector3 currentDestination;
    public PlayerController playerController;

    public Transform playerTransform; // The transform of the player
    //public List<GameObject> monsters; // The list of monsters
    //private GameObject closestMonster; // The closest monster

    private Vector3 previousPosition;
    private Vector3 currentPosition;

    //private bool moveToTarget = false;
    //public float distanceX;
    /// <summary>
    /// Het begin van het script uitwerpen
    /// </summary>	
    private void Start()
    {
        //Verkrijg alle knooppunten in de scène
        GameObject[] auxAllNodes = GameObject.FindGameObjectsWithTag("Node");
        allNodes = new Node[auxAllNodes.Length];
        for (int i = 0; i < auxAllNodes.Length; i++)
        {
            allNodes[i] = auxAllNodes[i].GetComponent<Node>();
        }
    }

    /// <summary>
    /// In de update is bereikt dat het object waaraan dit script is gekoppeld, moet worden verplaatst
    /// </summary>
    void Update()
    {           

    }



    //------------------------------------------------------------>

    /// <summary>
    /// Met behulp van het A*-algoritme wordt het meest optimale pad gezocht totdat het knooppunt wordt bereikt dat zich het dichtst bij de bestemmingspositie bevindt.
    /// 
    /// Werking:
    /// Op basis van de analyse van het knooppunt dat zich het dichtst bij de oorspronkelijke positie bevindt, wordt voor elk van de naburige knooppunten een score bepaald op basis van de afstand van deze naar het bestemmingsknooppunt,
    /// Op deze manier wordt degene met de meest optimale score (het dichtst bij de doelstelling) geselecteerd en geanalyseerd. Als u dit proces continu herhaalt, bereikt u uiteindelijk het doel- of bestemmingsknooppunt.
    /// Zodra het einde is bereikt, kan het te volgen pad worden gereconstrueerd, aangezien elk knooppunt de ouder zal opslaan van waaruit het is geopend.
    /// </summary>
    /// <param name="destination"></param>
    public Stack<Node> NavigateTo(Vector2 destination)
    {

        //1. initialisatie
        Stack<Node> path = new Stack<Node>();
        Node currentNode = FindClosestNode(transform.position); //Aanvankelijk het knooppunt dat zich het dichtst bij de start bevindt
        Node endNode = FindClosestNode(destination);

        //Controleer het bestaan ​​van begin, einde en verschil daartussen
        if (currentNode == null || endNode == null || currentNode == endNode)
            return path;

        //Open lijst, slaat knooppunten op om te analyseren(beste buren zoeken)
        SortedList<float, Node> openList = new SortedList<float, Node>();
        //Gesloten lijst, slaat reeds geanalyseerde knooppunten op
        List<Node> closedList = new List<Node>();

        //Parameters van het eerste knooppunt
        openList.Add(0, currentNode);
        currentNode.SetParent(null);
        currentNode.SetDistance(0f);

        //2. Analyse van knooppunten tot aan het einde
        while (openList.Count > 0)
        {

            //Haal de dichtstbijzijnde waarde uit de geordende lijst
            currentNode = openList.Values[0];
            openList.RemoveAt(0);
            float dist = currentNode.GetDistance();
            closedList.Add(currentNode);

            //Eindig als je het doel hebt bereikt
            if (currentNode == endNode)
                break;

            //naburige knooppunten doorkruisen
            foreach (Node neighbor in currentNode.GetNeighbors())
            {

                //Scans negeren als ze al zijn gescand (gesloten lijst) of in behandeling zijn (open lijst)
                if (closedList.Contains(neighbor) || openList.ContainsValue(neighbor))
                    continue;

                //Bewaar buurknooppunt in open lijst gesorteerd op afstand
                neighbor.SetParent(currentNode);
                neighbor.SetDistance(dist + Vector2.Distance(neighbor.transform.position, currentNode.transform.position));
                float distanceToTarget = Vector2.Distance(neighbor.transform.position, endNode.transform.position);
                openList.Add(neighbor.GetDistance() + distanceToTarget, neighbor);
            }
        }

        //3. Se ha encontrado el camino hasta el nodo final. 
        if (currentNode == endNode)
        {
            //Recorrer hacia atrás los nodos seleccionados a traves de su parametro previus y formar la pila de recorrido a seguir
            while (currentNode.GetParent() != null)
            {
                path.Push(currentNode);
                currentNode = currentNode.GetParent();
            }
            //path.Push(endNode);
        }
        return path;
    }

    //------------------------------------------------------------>



    //------------------------------------------------------------>

    /// <summary>
    /// Encuentra el waypoint más cercano respecto a la posicion pasada por parametro
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private Node FindClosestNode(Vector2 targetPosition)
    {
        Node closest = null;
        float minDist = float.MaxValue;

        for (int i = 0; i < allNodes.Length; i++)
        {
            float dist = Vector2.Distance(allNodes[i].transform.position, targetPosition);
            if (dist < minDist)
            {
                minDist = dist;
                closest = allNodes[i];
            }
        }

        return closest;
    }



    public void MovePlayer(Vector3 position)
    {
        Stack<Node> stack = NavigateTo(position);

        if (stack.Count > 0 && currentDestination == new Vector3(0, 0, 0))
        {
            Node node = stack.Pop();
            currentDestination = node.transform.position;
        }

        previousPosition = playerTransform.position;
        playerTransform.position = Vector3.MoveTowards(playerTransform.position, currentDestination, speed * Time.deltaTime);
        currentPosition = playerTransform.position;


        //Check if the player has reached the current destination
        if (playerTransform.position == currentDestination)
        {
            //Check if there are more locations in the stack
            if (stack.Count > 0)
            {        
                //Get the next destination from the stack
                Node node = stack.Pop();
                //node.IsWay(true);
                currentDestination = node.transform.position;
                
            } else {
                Debug.Log("Stop walking");
                playerController.StopRunningAnimation();
            }
        }

        foreach (Node node in allNodes)
        {
            //node.IsWay(false);
        }

        while (stack.Count > 0)
        {
            Node node = stack.Pop();
            //node.IsWay(true);
        }
    }
}
