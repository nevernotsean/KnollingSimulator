using UnityEngine;

public class PositionFromV3Variable : MonoBehaviour
{
  public UnityAtoms.Vector3Variable position;
  public UnityAtoms.BoolVariable enable;

  public bool lockZPosition = false;

  private void Update ()
  {
    if (enable && enable.Value == false) return;

    if (lockZPosition)
      transform.position = new Vector3 (position.Value.x, position.Value.y, transform.position.z);
    else
      transform.position = position.Value;
  }
}