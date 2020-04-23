using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldManTextController : MonoBehaviour
{
    public string message;
    public GameObject textObject;
    public float delay;
    public string messageSoFar = "";

    bool inRoom = false;

    public AudioClip beep;

    AudioSource audioSource;
    Rigidbody2D rigidbody2d;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Link"))
        {
            if (!inRoom)
            {
                textObject.gameObject.SetActive(true);
                StartCoroutine(Type());
            }
            else
            {
                inRoom = false;
                textObject.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator Type()
    {
        inRoom = true;
        int i = 0;
        while (i <= message.Length && inRoom )
        {
            messageSoFar = message.Substring(0, i);
            if (!messageSoFar.EndsWith(" "))
            {
                PlaySound(beep);
            }
            textObject.GetComponent<Text>().text = messageSoFar;
            
            yield return new WaitForSeconds(delay);
            i++;
        }
        if (i < message.Length)
        {
            yield break;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
