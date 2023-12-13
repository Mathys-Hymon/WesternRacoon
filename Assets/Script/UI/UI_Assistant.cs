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
        string[] messageArray = new string[]
        {
        "HI",
        "YO",
        "HEHEHEHEHE",
        "MUAHAHAHAHA",
        "HEY",
        };
        string message = messageArray[Random.Range(0, messageArray.Length)];
        TextWriter.AddWriter_Static(messageTMP, message, 0.05f);
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
