using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlotSystem : MonoBehaviour
{
    public UnityAtoms.GameObjectVariable Slot1Object;
    public UnityAtoms.GameObjectVariable Slot2Object;
    public UnityAtoms.GameObjectVariable Slot3Object;

    private void Start ()
    {
        Slot1Object = Resources.Load<UnityAtoms.GameObjectVariable> ("Variables/Slot1Object");
        Slot2Object = Resources.Load<UnityAtoms.GameObjectVariable> ("Variables/Slot2Object");
        Slot3Object = Resources.Load<UnityAtoms.GameObjectVariable> ("Variables/Slot3Object");
    }

    void CheckSlots ()
    {
        ItemParams itemParams;
        if (Slot1Object.Value)
        {
            itemParams = Slot1Object.Value.GetComponent<ItemParams> ();
        }

        if (Slot2Object.Value)
        {
            itemParams = Slot2Object.Value.GetComponent<ItemParams> ();
        }

        if (Slot3Object.Value)
        {
            itemParams = Slot3Object.Value.GetComponent<ItemParams> ();
        }
    }
}