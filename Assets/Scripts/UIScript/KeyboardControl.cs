using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControl : MonoBehaviour
{
    
    // Audio for opening and closing notebook
    private SFXManager sfxMan;
    public AudioClip openNotebook;
    public AudioClip closeNotebook;
    public GameObject Text1;
    public GameObject Text2;
    
    public GameObject notebook;
    // Start is called before the first frame update
    void Start()
    {
        sfxMan = FindObjectOfType<SFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
        }
    }

    public void SwitchNotebook()
    {
        notebook.SetActive(!notebook.activeSelf);
        if (notebook.activeSelf)
        {
            Text1.SetActive(false);
            Text2.SetActive(true);
            sfxMan.playSoundEffect(openNotebook);
        }
        else
        {
            Text1.SetActive(true);
            Text2.SetActive(false);
            sfxMan.playSoundEffect(closeNotebook);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
