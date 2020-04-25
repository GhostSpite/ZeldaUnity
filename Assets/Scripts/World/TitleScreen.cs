using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public KeyCode start;
    public KeyCode quit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(start)) SceneManager.LoadScene("Dungeon");
        if (Input.GetKeyDown(quit)) EditorApplication.ExecuteMenuItem("Edit/Play");
        if (Input.GetMouseButton(0)) SceneManager.LoadScene("Dungeon");
    }
}
