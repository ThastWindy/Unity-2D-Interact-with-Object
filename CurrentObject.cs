using UnityEngine;

public class CurrentObject : MonoBehaviour
{   

    [SerializeField] private Object currentObject;

    void OnTriggerEnter2D(Collider2D other)
    {
        Object obj = other.GetComponent<Object>();
        if (obj != null)
        {
            currentObject = obj;
            Debug.Log("Current object set to: " + currentObject.objectName);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Object obj = other.GetComponent<Object>();
        if (obj != null && obj == currentObject)
        {
            Debug.Log("Current object cleared: " + currentObject.objectName);
            currentObject = null;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentObject != null)
        {
            currentObject.Interact();
        }
    }
}
