using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleType : MonoBehaviour
{
    private TMPro.TextMeshProUGUI tmp;
    // Start is called before the first frame update

    public void AnimateTextIn (string text)
    {
        tmp = GetComponent<TMPro.TextMeshProUGUI> () ?? gameObject.AddComponent<TMPro.TextMeshProUGUI> ();
        tmp.text = text;
        StartCoroutine ("revealText");
    }

    IEnumerator revealText ()
    {
        int totalChars = tmp.textInfo.characterInfo.Length;
        int counter = 0;

        while (counter <= totalChars)
        {
            int visibleChars = counter % (totalChars + 1);
            // print (counter + " % " + (totalChars + 1) + " = " + visibleChars);
            tmp.maxVisibleCharacters = visibleChars;

            if (visibleChars >= totalChars)
                yield return new WaitForSeconds (1.0f);

            counter += 1;
            yield return new WaitForSeconds (0.02f);
        }

        yield return null;
    }
}