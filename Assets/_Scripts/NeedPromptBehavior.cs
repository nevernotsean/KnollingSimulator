using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NeedPromptBehavior : MonoBehaviour
{
    public GameObject Background;
    public TeleType TitleType;
    public TeleType BodyType;

    public string Title;
    public string Body;

    public void TriggerNewNeed (string bodyText)
    {
        Body = bodyText;
        Background.transform.localScale = Vector3.zero;

        BodyType.gameObject.SetActive (false);
        Background.transform.localScale = Vector3.zero;

        Invoke ("triggerBG", 0.1f);
        Invoke ("triggerBody", 0.8f);
    }

    public void HideToolTip ()
    {
        CancelInvoke ();
        Background.transform.DOScale (Vector3.zero, 0.1f);
        BodyType.gameObject.SetActive (false);
    }

    void triggerBG ()
    {
        Background.transform.DOScale (Vector3.one, 0.2f);
    }

    void triggerBody ()
    {
        BodyType.gameObject.SetActive (true);
        BodyType.AnimateTextIn (Body);
    }
}