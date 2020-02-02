using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        print ("ENTER: " + other.name);
    }

    private void OnTriggerExit (Collider other)
    {
        print ("EXIT: " + other.name);
    }
}