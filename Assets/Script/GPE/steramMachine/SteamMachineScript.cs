using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SteamMachineScript : MonoBehaviour
{
    [Header("ObjectReferences")]
    [SerializeField] private ButtonScript[] buttons;
    [SerializeField] private ParticleSystem steamParticle;

    private int IsValidInput;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (buttons.Length > 0)
        {
            IsValidInput = 0;
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].IsActivated())
                {
                    IsValidInput++;
                }
                else
                {
                    break;
                }
            }

            if (IsValidInput == buttons.Length && collision.gameObject.tag == "Freezeable" && collision.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                steamParticle.Play();
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = (transform.up * 10) / Vector3.Distance(transform.position, collision.transform.position);
            }
            else if(IsValidInput != buttons.Length)
            {
                steamParticle.Stop();
            }
        }
    }
}
