using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public GameObject targetMouseWorldPos;
    private PathManager pathManager;
    public bool uiBarClicked = false;

    void Start(){
        GameObject player = GameObject.Find("Player");
        pathManager =  player.GetComponent<PathManager>();
    }
    
    void Update()
    {
        if(!pathManager.auto)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);


                if (hit.collider != null && hit.collider.CompareTag("Monster"))
                {
                    // If the raycast hits a monster GameObject, set the player's target to the position of the monster.
                    pathManager.SetTarget(hit.collider.gameObject);

                }
                else if (hit.collider != null && hit.collider.CompareTag("UIbar"))
                {
                }
                else
                {
                    // If the raycast doesn't hit a monster GameObject, set the player's target to the mouse position.
                    Transform transformTarget = targetMouseWorldPos.GetComponent<Transform>();
                    transformTarget.position = new Vector3(worldPoint.x, worldPoint.y, 0);
                    pathManager.SetTarget(transformTarget.gameObject);
                }

            }


            if (Input.GetMouseButtonDown(0) && pathManager.targetMonster != null)
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                if (hit.collider != null && hit.collider.CompareTag("Monster"))
                {
                    // If the raycast hits a different monster GameObject, set the player's target to the new monster's position.
                    if (hit.collider.gameObject != pathManager.targetMonster.gameObject)
                    {
                        pathManager.SetTarget(hit.collider.transform.gameObject);
                    }
                }
            }
        }    
    }
}
