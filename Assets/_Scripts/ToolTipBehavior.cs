using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ToolTipBehavior : MonoBehaviour
{
    // TODO make tooltip background change height with text length
    public GameObject Background;
    public TeleType TitleType;
    public TeleType BodyType;

    public string Title;
    public string Body;

    public void TriggerTooltip (string titleText, string bodyText = null)
    {
        Title = titleText;
        Body = bodyText;

        TitleType.gameObject.SetActive (false);
        BodyType.gameObject.SetActive (false);
        Background.transform.localScale = Vector3.zero;

        var rect = Background.GetComponent<RectTransform> ().rect;

        if (bodyText == null)
            rect.height = 120;
        else
            rect.height = 200;

        Invoke ("triggerBG", 0.1f);
        Invoke ("triggerTitle", 0.2f);

        if (bodyText != null)
            Invoke ("triggerBody", 0.3f);
        else
            BodyType.gameObject.SetActive (false);

    }

    public void HideToolTip ()
    {
        CancelInvoke ();
        Background.transform.localScale = Vector3.zero;
        TitleType.gameObject.SetActive (false);
        BodyType.gameObject.SetActive (false);

    }

    void triggerBG ()
    {
        Background.transform.DOScale (Vector3.one, 0.2f);
    }

    void triggerTitle ()
    {
        TitleType.gameObject.SetActive (true);
        TitleType.AnimateTextIn (Title);
    }

    void triggerBody ()
    {
        BodyType.gameObject.SetActive (true);
        BodyType.AnimateTextIn (Body);
    }
}