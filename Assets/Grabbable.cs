using UnityEngine;
using DG.Tweening;

public class Grabbable : MonoBehaviour
{
    Ray mouseRay;
    RaycastHit hit;
    Camera cam;

    UnityAtoms.GameObjectVariable grabbableObject;
    bool mouseDown = false;

    public bool debug = false;
    public UnityAtoms.GameObjectVariable mouseRayHitObject;
    public UnityAtoms.Vector3Variable mouseWorldPosition;

    public float grabZDistance = 5.0f;
    public float dropZDistance = 0.0f;
    public float grabSpeed = 0.25f;

    void Start ()
    {
        cam = Camera.main;
        grabbableObject = Resources.Load<UnityAtoms.GameObjectVariable> ("Variables/grabbableObject");
    }

    void Update ()
    {
        if (Input.GetMouseButtonDown (0) && mouseDown == false)
        {
            mouseDown = true;

            grabItem ();
        }
        else if (Input.GetMouseButtonUp (0) && mouseDown == true)
        {
            mouseDown = false;

            dropItem ();
        }

        if (Input.GetMouseButton (1))
        {
            rotateGrabbableItem ();
        }

        if (mouseDown && grabbableObject &&
            mouseWorldPosition && grabbableObject.Value != null && mouseWorldPosition.Value != null)
            grabbableObject.Value.transform.position = new Vector3 (mouseWorldPosition.Value.x, mouseWorldPosition.Value.y, grabbableObject.Value.transform.position.z);
    }

    void grabItem ()
    {
        var item = mouseRayHitObject.Value;
        if (item == null) return;

        grabbableObject.SetValue (item);

        item.TryGetComponent<Rigidbody> (out Rigidbody rb);
        if (rb)
            rb.isKinematic = true;

        item.transform.DOMoveZ (grabZDistance, grabSpeed, false);
    }

    void dropItem ()
    {
        var item = grabbableObject.Value;
        if (item == null) return;

        item.TryGetComponent<Rigidbody> (out Rigidbody rb);
        if (rb)
            rb.isKinematic = false;

        item.transform.DOMoveZ (dropZDistance, grabSpeed, false).OnComplete (() => rb.isKinematic = false);

        grabbableObject.Reset (true);
    }

    void rotateGrabbableItem ()
    {
        var zRot = grabbableObject.Value.transform.rotation.z + 5;

        grabbableObject.Value.transform.DORotate (new Vector3 (0, 0, zRot), 0.5f);
    }
}