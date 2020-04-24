using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopOldMan : MonoBehaviour
{
    public GameObject oldMan;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OldManController controller = oldMan.GetComponent<OldManController>();
        if (other.gameObject.name.Contains("Link") && controller.hit)
        {
            controller.hit = false;
        }
    }
}
