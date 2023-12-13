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

    public static TextWriterSingle AddWriter_Static(TextMeshProUGUI uiTMP, string textToWrite, float timePerCharacter, bool removeWriterBeforeAdd)
    {
        if(removeWriterBeforeAdd)
        {
            instance.RemoveWriter(uiTMP);
        }
        return instance.AddWriter(uiTMP, textToWrite, timePerCharacter);
    }
    private TextWriterSingle AddWriter(TextMeshProUGUI uiTMP, string textToWrite, float timePerCharacter)
    {
        TextWriterSingle textWriterSingle = new TextWriterSingle(uiTMP, textToWrite, timePerCharacter);
        textWriterSingleList.Add(textWriterSingle);
        return textWriterSingle;
    }

    public static void RemoveWriter_Static(TextMeshProUGUI uiTMP)
    {
        instance.RemoveWriter(uiTMP);
    }
    private void RemoveWriter(TextMeshProUGUI uiTMP)
    {
        for (int i = 0; i < textWriterSingleList.Count; ++i)
        {
            if (textWriterSingleList[i].GetUIText() == uiTMP)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
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
            this.characterIndex = 0;
        }
        public bool Update()
        {
            if (uiTMP == null || string.IsNullOrEmpty(textToWrite))
            {
                return true;
            }
            timer -= Time.deltaTime;
                while (timer <= 0f)
                {
                    timer += timePerCharacter;
                    characterIndex++;
                    uiTMP.text = textToWrite.Substring(0, Mathf.Min(characterIndex, textToWrite.Length));


                    if (characterIndex >= textToWrite.Length)
                    {
                        uiTMP = null;
                        return true;
                    }
                }
                return false;
        }
        public TextMeshProUGUI GetUIText()
        {
            return uiTMP;
        }

        public bool IsActive()
        {
            return characterIndex < textToWrite.Length;
        }

        public void WriteAllAndDestroy()
        {
            uiTMP.text = textToWrite;
            characterIndex = textToWrite.Length;
            TextWriter.RemoveWriter_Static(uiTMP);
        }
    }
}
