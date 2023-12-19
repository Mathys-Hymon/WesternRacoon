using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Assistant : MonoBehaviour
{
    public static UI_Assistant Instance;

    private TextMeshProUGUI messageTMP;
    private TextWriter.TextWriterSingle textWriterSingle;
    private AudioSource talkingAudioSource;
    public int countMessage = 0;

    [SerializeField] string[] messageArray;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            HandleButtonPress();
        }
    }

    public void HandleButtonPress()
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
                countMessage = countMessage + 1;
                StartTalkingSound();
                textWriterSingle = TextWriter.AddWriter_Static(messageTMP, message, 0.05f, true, StopTalkingSound);
            }
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
    
    private void Start()
    {
        //messageTMP.text = "Hello World!";
        //TextWriter.AddWriter_Static(messageTMP, "To be, or not to be, that is the question:\r\nWhether 'tis nobler in the mind to suffer\r\nThe slings and arrows of outrageous fortune...", .1f);
    }
}
