using UnityEngine;

public class PositionFromV2 : MonoBehaviour
{
    public UnityAtoms.Vector2Variable position;
    public UnityAtoms.BoolVariable enable;

    private void Update ()
    {
        if (enable && enable.Value == false) return;

        transform.position = new Vector3 (position.Value.x, position.Value.y, transform.position.z);
    }
}