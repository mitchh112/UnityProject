using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Node : MonoBehaviour
{
    //VAR PUB
    [SerializeField]
    private List<Node> neighbors;

    //VAR PRIV
    private Node parent;
    private float distance;

    //------------------------------------------------------------>
    // GETs + SETs
    //------------------------------------------------------------>
    public void SetParent(Node parent) => this.parent = parent;
    public void SetDistance(float distance) => this.distance = distance;
    public void SetNeighbors(List<Node> neighbors) => this.neighbors = neighbors;
    public Node GetParent() => parent;
    public float GetDistance() => distance;
    public List<Node> GetNeighbors() => neighbors;

    private List<Node> closedList = new List<Node>();
    // private bool stop = true;
    private Node node;

    void Update()
    {
        CastLeft();
        CastRight();
    }

    void CastLeft()
    {
        Vector2 start = transform.position;
        start.x -= 0.09f;
        Vector2 direction = Vector2.left;
        float maxDistance = 0.2f;
        RaycastHit2D hit = Physics2D.Raycast(start, direction, maxDistance);
        Debug.DrawRay(start, direction * maxDistance, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.name.Contains("Node"))
            {
                SetNeighbors(hit.collider.gameObject.GetComponent<Node>());
            }

        }
    }

    private void SetNeighbors(Node node)
    {
        if (!neighbors.Contains(node))
        {
            neighbors.Add(node);
        }
    }


    void CastRight()
    {
        Vector2 start = transform.position;
        start.x += 0.09f;
        Vector2 direction = Vector2.right;
        float maxDistance = 0.2f;
        RaycastHit2D hit = Physics2D.Raycast(start, direction, maxDistance);
        Debug.DrawRay(start, direction * maxDistance, Color.red);

        if (hit.collider != null && hit.collider.gameObject != gameObject)
        {
            if (hit.collider.gameObject.name.Contains("Node"))
            {
                SetNeighbors(hit.collider.gameObject.GetComponent<Node>());
            }

        }
    }


	//------------------------------------------------------------>
	// DEBUG
	//------------------------------------------------------------>
#if UNITY_EDITOR


	private LineRenderer line;

    /// <summary>
    /// Markeer of demarkeer een knooppunt als onderdeel van de stream
    /// </summary>
    /// <param name="status"></param>
    public void IsWay(bool status) {

		GetComponent<SpriteRenderer>().color = Color.gray;
		if (line == null)
			line = gameObject.AddComponent<LineRenderer>();
		line.enabled = false;

		if (status) {
			gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;

			line.enabled = true;
			line.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
			line.startColor = Color.yellow;
			line.endColor = Color.yellow;
			line.startWidth = 0.1f;
			line.endWidth = 0.1f;
			line.positionCount = 2;
			line.useWorldSpace = true;
			line.SetPosition(0, transform.position);
			line.SetPosition(1, parent.transform.position);

		} 
	}


	void OnDrawGizmos(){

			Handles.Label(transform.position, gameObject.name);

			if (neighbors == null)
				return;
			foreach(var neighbor in neighbors)
			{
				if (neighbor != null) {
					Vector2 auxOrigin = transform.position;
					Vector2 auxTarget = neighbor.transform.position;


					if (auxOrigin.y >= auxTarget.y) {
						Gizmos.color = Color.blue;
					} else {
						Gizmos.color = Color.cyan;
						auxTarget.x += 0.1f;
						auxTarget.y += 0.1f;
						auxOrigin.x += 0.1f;
						auxOrigin.y += 0.1f;
					}
					Gizmos.DrawLine(auxOrigin, auxTarget);

				}
			}
		}
	#endif

}