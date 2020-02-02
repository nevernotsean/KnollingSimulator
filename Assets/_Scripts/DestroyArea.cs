using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArea : MonoBehaviour
{
    private void OnTriggerExit (Collider other)
    {
        var layer = LayerMask.LayerToName (other.gameObject.layer);
        print (other.name);

        if (layer == "Item") Destroy (other.gameObject);
    }
}