using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRay : MonoBehaviour
{
    Ray mouseRay;
    RaycastHit hit;
    Camera cam;

    public LayerMask layerHit;
    public UnityAtoms.Vector3Variable hitPoint;
    public UnityAtoms.GameObjectVariable hitObject;

    UnityAtoms.Vector3Variable mouseCursorWorld;
    UnityAtoms.Vector2Variable mousePositionCanvas;

    void Start ()
    {
        cam = Camera.main;
        mouseRay = cam.ScreenPointToRay (Input.mousePosition);
        mouseCursorWorld = Resources.Load<UnityAtoms.Vector3Variable> ("Variables/mouseCursorWorld");
        mousePositionCanvas = Resources.Load<UnityAtoms.Vector2Variable> ("Variables/mousePositionCanvas");
    }

    void Update ()
    {
        var mx = Input.mousePosition.x;
        var my = cam.pixelHeight - Input.mousePosition.y; //for perspective cam
        var mz = cam.nearClipPlane;

        if (cam.orthographic) // for ortho
        {
            my = Input.mousePosition.y;
            mz = 0;
        }

        var screenPoint = cam.ScreenToWorldPoint (new Vector3 (mx, my, mz));

        mouseCursorWorld.SetValue (screenPoint);
        mousePositionCanvas.SetValue (Input.mousePosition);

        mouseRay = cam.ScreenPointToRay (Input.mousePosition);

        if (Physics.Raycast (mouseRay, out hit, Mathf.Infinity, layerHit))
        {
            if (hitPoint)
                hitPoint.SetValue (hit.point);
            if (hitObject)
                hitObject.SetValue (hit.collider.gameObject);

            // print ("hitpoint: " + hitPoint.Value);
            // if (hitObject && hitObject.Value.tag != "Item") print ("hitObject: " + hitObject.Value.name);
        }
        else
        {
            if (hitObject) hitObject.SetValue (null as GameObject);
        }
    }

    private void OnApplicationQuit ()
    {
        if (hitObject) hitObject.SetValue (null as GameObject);
        if (hitPoint) hitPoint.SetValue (null);
    }

    private void OnGUI ()
    {
        Debug.DrawRay (mouseRay.origin, mouseRay.direction, Color.red);
    }
}