using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTextCall : MonoBehaviour
{
    [SerializeField] GameObject canvasRef;
    [SerializeField] TextWriter textWriter;
    [SerializeField] UI_Assistant ui_Assistant;

    private int newMessage;

    void Start()
    {
        
    }

    void Update()
    {
        //newMessage = UI_Assistant.Instance.countMessage;

        if (newMessage >= 3)
        {
            canvasRef.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        canvasRef.SetActive(true);
        Time.timeScale = 0f;
        //if (textWriter != null)
        //{
        //    canvasRef.SetActive(true);
        //}
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (textWriter != null)
        {
            canvasRef.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
