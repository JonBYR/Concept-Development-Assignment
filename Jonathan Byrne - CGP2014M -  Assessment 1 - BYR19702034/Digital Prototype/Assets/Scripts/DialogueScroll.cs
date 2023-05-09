using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueScroll : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    string textToWrite;
    int index;
    float timer, timeToNextChar;
    bool invisible;
    // Start is called before the first frame update
    void Start()
    {
        AddWriter("In the distant future, high above the sky, a battle rages between our gravity bound hero 'Upa-Upa' and the evil sky empire...", 0.05f);
    }
    public void AddWriter(string theText, float time)
    {
        this.textToWrite = theText;
        this.timeToNextChar = time;
        index = 0;
        invisible = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (textMesh != null)
        {
            timer -= Time.deltaTime;
            while (timer <= 0f) //ensures that it works even on a low framerate
            {
                timer += timeToNextChar;
                index++;
                string currentText = textToWrite.Substring(0, index);
                if (invisible)
                {
                    currentText += "<color=#00000000>" + textToWrite.Substring(index) + "</color>"; //allows all text to stay on the same line
                }
                textMesh.text = currentText;
                if (index >= textToWrite.Length)
                {
                    textMesh = null;
                    return;
                }
            }
        }
    }
}
