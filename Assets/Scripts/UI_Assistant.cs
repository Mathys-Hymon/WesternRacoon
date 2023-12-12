using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Assistant : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    private TextMeshProUGUI messageTMP;
    void Awake()
    {
        messageTMP = transform.Find("message").Find("messageTMP").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        //messageTMP.text = "Hello World!";
        textWriter.AddWriter(messageTMP, "Hello My Darling!", 1f);
    }
}
