using UnityEngine;

public class ObjectMovement : Photon.MonoBehaviour
{
    public PhotonView photonView;
    [SerializeField]public static int sortingLayer = 0;
    // De snelheid waarmee het object beweegt (in eenheden per seconde)
    public SpriteRenderer spriteRenderer;
    // Het object waar we naar toe bewegen
    private Vector3 startingPosition;
    private int hitCount;

    private void Awake()
    {
        float random = Random.Range(2f, 3f);
        transform.localScale = new Vector3(random, random, random);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
        // Sla de huidige positie van het GameObject op
        startingPosition = transform.position;
        spriteRenderer.sortingOrder = sortingLayer;
        sortingLayer++;
        
    }
    private void Update()
    {
        if (photonView.isMine)
        {
            if(hitCount>=3){
                PhotonNetwork.Destroy(gameObject);
            }
            if (sortingLayer >= 10)
            {
                sortingLayer = 0;
            }

            // Controleer of het gameobject actief is
            if (gameObject.activeSelf)
            {
                Quaternion currentRotation = transform.rotation; // Get the current rotation of the object
                if (currentRotation.eulerAngles.y == 190f)
                {
                    transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);

                }
                else
                {
                    transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other2D)
    {
        hitCount++;
    }
}
