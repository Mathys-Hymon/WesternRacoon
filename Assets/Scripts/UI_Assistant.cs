using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Assistant : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    private TextMeshProUGUI messageTMP;
    void Awake()
    {
        messageTMP = transform.Find("message").Find("messageTMP").GetComponent<TextMeshProUGUI>();
        Application.targetFrameRate = 50;
    }

    private void Start()
    {
        //messageTMP.text = "Hello World!";
        textWriter.AddWriter(messageTMP, "To be, or not to be, that is the question:\r\nWhether 'tis nobler in the mind to suffer\r\nThe slings and arrows of outrageous fortune...", .1f);
    }
}
