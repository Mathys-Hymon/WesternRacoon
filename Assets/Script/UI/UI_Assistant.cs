using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class UI_Assistant : MonoBehaviour
{
    private TextMeshProUGUI messageTMP;
    private TextWriter.TextWriterSingle textWriterSingle;
    private AudioSource talkingAudioSource;

    [SerializeField] string[] messageArray;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            HandleButtonPress();
        }
    }

    private void HandleButtonPress()
    {
        messageTMP = transform.Find("message").Find("messageTMP").GetComponent<TextMeshProUGUI>();
        talkingAudioSource = transform.Find("Speech").GetComponent<AudioSource>();
        if (textWriterSingle != null && textWriterSingle.IsActive())
        {
            // Currently active TextWriter
            textWriterSingle.WriteAllAndDestroy();
        }
        else
        {
            if(messageArray.Length !=0)
            {
                string message = messageArray[Random.Range(0, messageArray.Length)];
                StartTalkingSound();
                textWriterSingle = TextWriter.AddWriter_Static(messageTMP, message, 0.05f, true, StopTalkingSound);
            }
            //string[] messageArray = new string[]
            //{
            //"HI THERE",
            //"YO",
            //"HEHEHEHEHEHEHEHEHEHEHE",
            //"MUAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHA",
            //"HEY! HOW ARE YOU?",
            //};
            //string message = messageArray[Random.Range(0, messageArray.Length)];
            //textWriterSingle = TextWriter.AddWriter_Static(messageTMP, message, 0.05f, true);

        }
    }
    private void StartTalkingSound()
    {
        talkingAudioSource.Play();
    }
    private void StopTalkingSound()
    {
        talkingAudioSource.Stop();
    }
    //private void Awake()
    //{
    //    messageTMP = transform.Find("message").Find("messageTMP").GetComponent<TextMeshProUGUI>();

    //    transform.Find("message")Input.GetKey(KeyCode.Joystick1Button6) = () => 
    //    { 
    //        string[] messageArray = new string[] 
    //        {
    //            "HI",
    //            "YO",
    //            "HEHEHEHEHE",
    //            "MUAHAHAHAHA",
    //            "HEY",
    //        }; 
    //        string message = messageArray[Random.Range(0, messageArray.Length)];
    //        TextWriter.AddWriter_Static(messageTMP, message, 0.05f);
    //    };
    //}

    private void Start()
    {
        //messageTMP.text = "Hello World!";
        //TextWriter.AddWriter_Static(messageTMP, "To be, or not to be, that is the question:\r\nWhether 'tis nobler in the mind to suffer\r\nThe slings and arrows of outrageous fortune...", .1f);
    }
}
