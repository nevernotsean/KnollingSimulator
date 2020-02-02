using UnityEngine;

public class MouseRay : MonoBehaviour
{
    Ray mouseRay;
    RaycastHit hitpoint1;
    RaycastHit hitpoint2;
    Camera cam;

    public bool debug = false;
    public LayerMask layerHit;
    public LayerMask mouseCursorLayer;

    UnityAtoms.GameObjectVariable mouseRayHitObject;
    UnityAtoms.Vector3Variable mouseCursorWorld;
    UnityAtoms.Vector3Variable mouseRayHitPoint;
    UnityAtoms.Vector2Variable mousePositionCanvas;

    void Start ()
    {
        cam = Camera.main;
        mouseRay = cam.ScreenPointToRay (Input.mousePosition);
        mouseCursorWorld = Resources.Load<UnityAtoms.Vector3Variable> ("Variables/mouseCursorWorld");
        mouseRayHitObject = Resources.Load<UnityAtoms.GameObjectVariable> ("Variables/mouseRayHitObject");
        mouseRayHitPoint = Resources.Load<UnityAtoms.Vector3Variable> ("Variables/mouseRayHitPoint");
    }

    void Update ()
    {
        mouseRay = cam.ScreenPointToRay (Input.mousePosition);

        if (Physics.Raycast (mouseRay, out hitpoint1, Mathf.Infinity, mouseCursorLayer))
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
        }

        if (Physics.Raycast (mouseRay, out hitpoint2, Mathf.Infinity, layerHit))
        {
            if (mouseRayHitPoint)
                mouseRayHitPoint.SetValue (hitpoint2.point);
            if (mouseRayHitObject)
                mouseRayHitObject.SetValue (hitpoint2.collider.gameObject);
        }
        else
        {
            if (mouseRayHitObject) mouseRayHitObject.Reset (true);
        }
    }

    private void OnApplicationQuit ()
    {
        if (mouseRayHitObject) mouseRayHitObject.Reset (true);
        if (mouseRayHitPoint) mouseRayHitPoint.SetValue (null);
    }

    private void OnGUI ()
    {
        if (!debug) return;

        if (mouseRayHitObject.Value != null)
            GUI.TextArea (new Rect (20, 20, 150, 30), $"hitObject: {mouseRayHitObject.Value.name}");
        else
            GUI.TextArea (new Rect (20, 20, 150, 30), $"hitObject: {mouseRayHitObject.Value}");

        if (mouseCursorWorld.Value != null)
            GUI.TextArea (new Rect (20, 60, 150, 30), $"hitPoint: {mouseCursorWorld.Value}");
        else
            GUI.TextArea (new Rect (20, 60, 150, 30), $"hitObject: --");

        Debug.DrawRay (mouseRay.origin, mouseRay.direction, Color.red);
    }
}