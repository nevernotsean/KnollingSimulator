using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    public NeedPromptBehavior needPrompt;
    public IngredientEmitter emitter;

    public bool isPlaying = true;
    [SerializeField] private float timePlaying = 0;

    [Header ("Ask Params")]
    public float newAskWait = 5.0f;
    public Vector2 askTimeLimitMinMax = Vector2.zero;
    public Ask currentGoal;

    [Header ("Item Emitter Params")]
    public float newItemDelay = 5.0f;

    // Slots
    UnityAtoms.GameObjectVariable Slot1Object;
    UnityAtoms.GameObjectVariable Slot2Object;
    UnityAtoms.GameObjectVariable Slot3Object;

    // Variables
    UnityAtoms.FloatVariable askTimeleftPercent;

    bool hasGoal = false;

    private void Awake ()
    {
        if (current == null)
            current = this;
        else
            Destroy (gameObject);

        DontDestroyOnLoad (gameObject);
    }

    void Start ()
    {
        Slot1Object = Resources.Load<UnityAtoms.GameObjectVariable> ("Variables/Slot1Object");
        Slot2Object = Resources.Load<UnityAtoms.GameObjectVariable> ("Variables/Slot2Object");
        Slot3Object = Resources.Load<UnityAtoms.GameObjectVariable> ("Variables/Slot3Object");
        askTimeleftPercent = Resources.Load<UnityAtoms.FloatVariable> ("Variables/askTimeleftPercent");

        askTimeleftPercent.SetValue (0);
    }

    void Update ()
    {

        if (!isPlaying) return;

        timePlaying += Time.deltaTime;

        if (hasGoal == false && Mathf.Floor (timePlaying) > 1 && Mathf.Floor (timePlaying) % (newAskWait) == 0)
            GetNewGoal ();
    }

    IEnumerator StartTimer ()
    {
        while (hasGoal && askTimeleftPercent.Value <= 1.0f)
        {
            askTimeleftPercent.SetValue (timePlaying / currentGoal.timeEnd);
            yield return new WaitForEndOfFrame ();
        }

        askTimeleftPercent.SetValue (0);
        hasGoal = false;

        Damage ();

        yield return new WaitForEndOfFrame ();
    }

    void Damage ()
    {
        print ("DAMAGE!");
    }

    public void CheckItemSubmission () // TODO refactor this with CSV
    {
        // UnityAtoms.GameObjectVariable[] slots = {
        //     Slot1Object,
        //     Slot2Object,
        //     Slot3Object
        // };

        // string[] qualities = {
        //     currentGoal.quality1,
        //     currentGoal.quality2,
        //     currentGoal.quality3
        // };

        // int[] qualityValues = {
        //     currentGoal.quality1Value,
        //     currentGoal.quality2Value,
        //     currentGoal.quality3Value
        // };

        // foreach (var slot in slots)
        // {
        //     if (slot.Value == null) continue;

        //     int[] itemParams = slot.Value.GetComponent<ItemParams> ().ReturnParams ();
        //     for (int i = 0; i < itemParams.Length; i++)
        //     {
        //         for (int ii = 0; ii < qualities.Length; ii++)
        //         {
        //             if (itemParams[i] == qualityValues[ii])
        //             { // 1. beauty 2. quality 3. harm 4. heal

        //             }
        //         }
        //     }
        // }
    }

    public void ResetGame ()
    {
        timePlaying = 0;
        isPlaying = false;
    }

    #region GOALS

    string getNeedBodyText (Ask ask) // TODO refactor this with CSV
    {
        print ("getting new need description");

        int randomType = Random.Range (0, 2);
        int randomQuality = Random.Range (0, 2);

        // var name = "";
        // var one = "";
        // var two = "";
        // var three = "";

        // name = string.Format ("a {0}", ask.objectName);

        // if (ask.difficulty >= 1)
        //     one = string.Format (" that {0}", getDescriptionFromConstant (ask.quality1, ask.quality1Value));
        // if (ask.difficulty >= 2)
        // {
        //     if (ask.difficulty == 2) one = one + " and";
        //     two = string.Format (" {0}", getDescriptionFromConstant (ask.quality2, ask.quality2Value));
        // }
        // if (ask.difficulty >= 3)
        // {
        //     one = one + ", ";
        //     three = string.Format ("and {0}", getDescriptionFromConstant (ask.quality3, ask.quality3Value));
        // }

        // var message = string.Format ("I need {0}{1}{2}{3}!", name, one, two, three);

        return "";
    }

    public void GetNewGoal ()
    {
        // print ("getting new goal");

        var newAsk = new Ask ();

        newAsk.timeStart = timePlaying;

        var expireTime = Random.Range (askTimeLimitMinMax.x, askTimeLimitMinMax.y) * (4 - newAsk.difficulty);

        newAsk.timeEnd = newAsk.timeStart + expireTime;

        currentGoal = newAsk;

        needPrompt.TriggerNewNeed (newAsk.description);

        hasGoal = true;
        StartCoroutine ("StartTimer");
    }

    #endregion

}

public struct Ask
{
    public int type { get; set; }
    public int color { get; set; }
    public int quality { get; set; }

    public int difficulty { get; set; }
    public string description { get; set; }
    public float timeStart { get; set; }
    public float timeEnd { get; set; }
}