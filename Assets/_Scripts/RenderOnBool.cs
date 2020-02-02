using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Renderer))]
public class RenderOnBool : MonoBehaviour
{
    public UnityAtoms.BoolVariable Bool;
    public bool Inverse = false;
    Renderer render => GetComponent<Renderer> ();

    void Update ()
    {
        if (Bool.Value)
        {
            if (Inverse)
                disable ();
            else
                enable ();
        }
        else
        {
            if (Inverse)
                enable ();
            else
                disable ();
        }
    }

    void enable ()
    {
        if (!render.enabled)
            render.enabled = true;
    }
    void disable ()
    {
        if (render.enabled)
            render.enabled = false;
    }
}