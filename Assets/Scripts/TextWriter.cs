using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWriter : MonoBehaviour
{
    private static TextWriter instance;

    private List<TextWriterSingle> textWriterSingleList;

    private void Awake()
    {
        instance = this;
        textWriterSingleList = new List<TextWriterSingle>();
    }

    public static void AddWriter_Static(TextMeshProUGUI uiTMP, string textToWrite, float timePerCharacter)
    {
        instance.AddWriter(uiTMP, textToWrite, timePerCharacter);
    }
    private void AddWriter(TextMeshProUGUI uiTMP, string textToWrite, float timePerCharacter)
    {
        textWriterSingleList.Add(new TextWriterSingle(uiTMP, textToWrite, timePerCharacter));
    }

    private void Update()
    {
        Debug.Log(textWriterSingleList.Count);
        for (int i=0; i < textWriterSingleList.Count; ++i) 
        {
            bool destroyInstance = textWriterSingleList[i].Update();
            if (destroyInstance) 
            { 
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    public class TextWriterSingle
    {
        private TextMeshProUGUI uiTMP;
        private string textToWrite;
        private int characterIndex;
        private float timePerCharacter;
        private float timer;
        public TextWriterSingle(TextMeshProUGUI uiTMP, string textToWrite, float timePerCharacter)
        {
            this.uiTMP = uiTMP;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            characterIndex = 0;
        }
        public bool Update()
        {
                timer -= Time.deltaTime;
                while (timer <= 0f)
                {
                    timer += timePerCharacter;
                    characterIndex++;
                    uiTMP.text = textToWrite.Substring(0, characterIndex);


                    if (characterIndex >= textToWrite.Length)
                    {
                        uiTMP = null;
                        return true;
                    }
                }
                return false;
        }
    }
}
