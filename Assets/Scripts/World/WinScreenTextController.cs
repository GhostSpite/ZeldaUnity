using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenTextController : MonoBehaviour
{
    public string message;
    public GameObject textObject;
    public float delay;
    public string messageSoFar = "";

    bool started;
    public bool go;

    public AudioClip beep;

    AudioSource audioSource;
    Rigidbody2D rigidbody2d;

    void Start()
    {
        started = false;
        go = false;
        rigidbody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(!started && go)
        {
            textObject.gameObject.SetActive(true);
            StartCoroutine(Type());
        }
    }

    IEnumerator Type()
    {
        started = true;
        int i = 0;
        while (i <= message.Length && started)
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
