using UnityEngine;
using DG.Tweening;

public class SlotBehavior : MonoBehaviour
{
  public UnityAtoms.GameObjectVariable slotItem;
  Rigidbody slotItemRb;

  private void Update ()
  {
    if (slotItem.Value)
    {
      slotItem.Value.transform.RotateAround (slotItem.Value.transform.position, Vector3.up, 1);
    }
  }

  public void AddToSlot (GameObject o)
  {
    if (slotItem.Value)
    {
      var oldItem = slotItem.Value;
      var oldRb = slotItemRb;
      oldItem.transform.position = new Vector3 (0, 5, 0);
      oldRb.isKinematic = false;
      ReleaseSlot ();
    }

    slotItem.SetValue (o);
    slotItemRb = o.GetComponent<Rigidbody> ();
    slotItemRb.isKinematic = true;

    slotItemRb.transform.DOScale (Vector3.one / 2, 0.3f);
    slotItemRb.transform.position = transform.position;
  }

  public void ReleaseSlot ()
  {
    if (slotItemRb)
    {
      slotItemRb.transform.DOScale (Vector3.one, 0.1f);
    }

    // slotItemRb.transform.parent = oldParent;
    slotItem.SetValue (null as GameObject);
    slotItemRb = null;
  }
}