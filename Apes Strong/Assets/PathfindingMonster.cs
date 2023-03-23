using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingMonster : MonoBehaviour
{
    public float speed;
    //VAR PRIV
    private Node[] allNodes;
    private Vector3 currentDestination;
    public MonsterController monsterController;
    public Transform monsterTransform; // The transform of the player
    private Vector3 previousPosition;
    private Vector3 currentPosition;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] auxAllNodes = GameObject.FindGameObjectsWithTag("Node");
        allNodes = new Node[auxAllNodes.Length];
        for (int i = 0; i < auxAllNodes.Length; i++)
        {
            allNodes[i] = auxAllNodes[i].GetComponent<Node>();
        }
    }

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

        if (currentNode == endNode)
        {
            while (currentNode.GetParent() != null)
            {
                path.Push(currentNode);
                currentNode = currentNode.GetParent();
            }
        }
        return path;
    }

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

    public void MoveMonster(Vector3 position)
    {
        Stack<Node> stack = NavigateTo(position);

        if (stack.Count > 0 && currentDestination == new Vector3(0, 0, 0))
        {
            Node node = stack.Pop();
            currentDestination = node.transform.position;
        }

        previousPosition = monsterTransform.position;
        monsterTransform.position = Vector3.MoveTowards(monsterTransform.position, currentDestination, speed * Time.deltaTime);
        currentPosition = monsterTransform.position;


        //Check if the player has reached the current destination
        if (monsterTransform.position == currentDestination)
        {
            //Check if there are more locations in the stack
            if (stack.Count > 0)
            {
                //Get the next destination from the stack
                Node node = stack.Pop();
                currentDestination = node.transform.position;
            }
            else
            {
                monsterController.StopRunningAnimation();
            }
        }

        foreach (Node node in allNodes)
        {
            //node.IsWay(false);
        }

        while (stack.Count > 0)
        {
            Node node = stack.Pop();
        }
    }
}
