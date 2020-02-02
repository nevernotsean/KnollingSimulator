using UnityEngine;
using DG.Tweening;
using UnityAtoms;

public class Grabbable : MonoBehaviour
{
  Rigidbody rb => GetComponent<Rigidbody> ();
  public bool isGrabbed = false;

  UnityAtoms.Vector3Variable mouseCursorWorld;
  UnityAtoms.BoolVariable isGrabbing;
  UnityAtoms.GameObjectVariable grabbedItem;
  UnityAtoms.GameObjectVariable mouseCursorHoverUI;

  Quaternion originalRotation;

  bool isRotating = false;
  float rotateDuration = 0.1f;
  Camera cam;

  ToolTipBehavior toolTip;

  private void Start ()
  {
    cam = Camera.main;
    mouseCursorWorld = Resources.Load<UnityAtoms.Vector3Variable> ("Variables/mouseCursorWorld");
    isGrabbing = Resources.Load<UnityAtoms.BoolVariable> ("Variables/isGrabbing");
    grabbedItem = Resources.Load<UnityAtoms.GameObjectVariable> ("Variables/grabbedItem");
    mouseCursorHoverUI = Resources.Load<UnityAtoms.GameObjectVariable> ("Variables/mouseCursorHoverUI");

    originalRotation = transform.rotation;

    var toolTipGo = GameObject.FindGameObjectWithTag ("toolTip");

    if (toolTipGo != null)
      toolTipGo.TryGetComponent<ToolTipBehavior> (out toolTip);

    // if (toolTip == null)
    // {
    //   Debug.LogError ("Tooltip not found!");
    //   print (toolTipGo);
    // }
  }

  private void Update ()
  {
    if (isGrabbed)
    {
      transform.position = new Vector3 (
        mouseCursorWorld.Value.x,
        mouseCursorWorld.Value.y,
        cam.nearClipPlane + 5
      );
    }
    else
    {
      // if (rb.isKinematic) LetGo ();
    }
  }

  public void GrabThis ()
  {
    transform.DORotate (originalRotation.eulerAngles, 0.3f, RotateMode.Fast);
    transform.DOScale (Vector3.one * 1.25f, 0.3f);
    isGrabbed = true;
    rb.isKinematic = true;
    isGrabbing.SetValue (true);
    grabbedItem.SetValue (gameObject);

    // TODO refactor this with CSV
    var itemParams = GetComponent<ItemParams> ();
    toolTip.TriggerTooltip (itemParams.Description);

    if (mouseCursorHoverUI.Value && mouseCursorHoverUI.Value.tag == "Slot")
    {
      var slot = mouseCursorHoverUI.Value.GetComponent<SlotBehavior> ();
      slot.ReleaseSlot ();
      return;
    }
  }

  public void LetGo (bool sendToBag = true)
  {
    toolTip.HideToolTip ();
    transform.DOScale (Vector3.one, 0.3f);
    isGrabbed = false;
    rb.isKinematic = false;
    isGrabbing.SetValue (false);
    grabbedItem.Reset (true);

    if (mouseCursorHoverUI.Value && mouseCursorHoverUI.Value.tag == "Slot")
    {
      var slot = mouseCursorHoverUI.Value.GetComponent<SlotBehavior> ();
      slot.AddToSlot (gameObject);
      return;
    }

    if (sendToBag)
      transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
  }

  public void RotateStep ()
  {
    if (isRotating) return;
    isRotating = true;
    transform.DORotate (transform.rotation.eulerAngles + new Vector3 (0, 0, 30), rotateDuration, RotateMode.Fast).OnComplete (() =>
    {
      originalRotation = transform.rotation;
      isRotating = false;
    });
  }
}