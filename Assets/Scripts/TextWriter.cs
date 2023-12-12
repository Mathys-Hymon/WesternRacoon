using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWriter : MonoBehaviour
{
    private TextMeshProUGUI uiTMP;
    private string textToWrite;
    private int characterIndex;
    private float timePerCharacter;
    private float timer;
    public void AddWriter(TextMeshProUGUI uiTMP, string textToWrite, float timePerCharacter)
    {
        this.uiTMP = uiTMP;
        this.textToWrite = textToWrite; 
        this.timePerCharacter = timePerCharacter;
        characterIndex = 0;
    }

    private void Update()
    {
        if (uiTMP != null)
        {
            timer -= Time.deltaTime;
            while(timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;
                uiTMP.text = textToWrite.Substring(0, characterIndex);

                if(characterIndex >= textToWrite.Length) 
                {
                    uiTMP = null;
                    return;
                }
            }
        }
    }
}
