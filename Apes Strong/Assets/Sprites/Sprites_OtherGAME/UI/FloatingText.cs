using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public TMPro.TextMeshProUGUI myText;
    private Vector3 location;

    // Start is called before the first frame update
    void Start()
    {
        location = new Vector3(transform.position.x, transform.position.y + 2, transform.position.y);
    }
    void Update(){
        transform.position = Vector3.MoveTowards(transform.position, location, 3 * Time.deltaTime);
    }
    public void SetText(string input)
    {
        myText.text = input;
        gameObject.SetActive(true);   
    }

    IEnumerator ReactivateAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnEnable()
    {

        StartCoroutine(ReactivateAfterDelay());
        // Voer hier de actie uit die je wilt doen wanneer het GameObject actief wordt.
    }
}
