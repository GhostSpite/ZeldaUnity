using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public KeyCode next;
    public KeyCode prev;
    GameObject current;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        current = gameObject.transform.GetChild(index).gameObject;
        current.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(next))
        {
            current.SetActive(false);
            index++;
            if (index >= gameObject.transform.childCount) index = 0;
            current = gameObject.transform.GetChild(index).gameObject;
            current.SetActive(true);
        }
        
        if (Input.GetKeyDown(prev))
        {
            current.SetActive(false);
            index--;
            if (index < 0) index = gameObject.transform.childCount - 1;
            current = gameObject.transform.GetChild(index).gameObject;
            current.SetActive(true);
        }
    }
}
