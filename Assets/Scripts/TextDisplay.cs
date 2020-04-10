using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public string _textFile;
    private List<string> texts;

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        texts = new List<string>();
        //Load in the files into a list of strings.
        FileInfo _sourceFile = new FileInfo(_textFile);
        StreamReader reader = _sourceFile.OpenText(); 

        
        while(!reader.EndOfStream){
            //Read the next line.
            string text = reader.ReadLine();
            if(!text.Equals("\n")){
                texts.Add(text);
            }
            reader.ReadLine();
        }

        //There.
        reader.Close();

        foreach(string s in texts){
            Debug.Log(s);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayText(){
        //Pick a random line.
        text.enabled = true;
        int r = Random.Range(0, texts.Count);
        text.text = texts[r];
    }

    public void DisplayText(string s){
        text.enabled = true;
        text.text = s;
    }

    public void HideText(){
        text.enabled = false;
    }


}
