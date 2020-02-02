using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (MouseRay))]
public class MouseGrab : MonoBehaviour
{
    UnityAtoms.BoolVariable isGrabbing;
    UnityAtoms.GameObjectVariable mouseCursorHoverItem;
    MouseRay mouseRaycaster;
    Grabbable grabbableItem;

    void Start ()
    {
        mouseCursorHoverItem = Resources.Load<UnityAtoms.GameObjectVariable> ("Variables/mouseCursorHoverItem");
        isGrabbing = Resources.Load<UnityAtoms.BoolVariable> ("Variables/isGrabbing");

        mouseRaycaster = GetComponent<MouseRay> ();
    }

    void Update ()
    {
        if (Input.GetMouseButtonDown (0))
        {
            if (mouseCursorHoverItem.Value)
            {
                if (grabbableItem == null)
                    grabbableItem = mouseCursorHoverItem.Value.GetComponent<Grabbable> ();

                if (grabbableItem == null) return;
                if (isGrabbing.Value == true) return;

                grabbableItem.GrabThis ();
            }
        }

        if (Input.GetMouseButtonUp (0))
        {
            if (grabbableItem)
            {
                grabbableItem.LetGo ();
                grabbableItem = null;
            }
        }

        if (Input.GetMouseButtonDown (1))
            if (grabbableItem)
                grabbableItem.RotateStep ();
    }
}