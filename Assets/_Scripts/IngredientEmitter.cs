using System.Collections;
using UnityEngine;

public class IngredientEmitter : MonoBehaviour
{
    public GameObject[] emitters;
    public GameObject[] ingredientGroups;
    public GameObject itemContainer;
    public float emitDelay = 0.5f;

    int currentIngredientIndex;

    IEnumerator Start ()
    {
        if (itemContainer == null)
            itemContainer = new GameObject ("Items");

        while (GameManager.current.isPlaying)
        {
            yield return new WaitForSeconds (GameManager.current.newItemDelay);

            int i = Random.Range (0, ingredientGroups.Length);
            var group = Instantiate (ingredientGroups[i], transform.position, Quaternion.identity, itemContainer.transform);

            foreach (Transform child in group.transform)
            {
                child.gameObject.SetActive (false);
            }

            StartCoroutine (DropGroupItems (group));

            yield return null;
        }

        yield return null;
    }

    IEnumerator DropGroupItems (GameObject group)
    {
        var i = 0;

        while (i < group.transform.childCount)
        {
            yield return new WaitForSeconds (emitDelay);

            DropItem (group.transform.GetChild (i).gameObject);

            i++;

            yield return null;
        }

        Destroy (group);
    }

    void DropItem (GameObject item)
    {
        GameObject randomEmitter = emitters[Random.Range (0, emitters.Length)];

        item.SetActive (true);
        item.transform.position = randomEmitter.transform.position;
        item.transform.parent = itemContainer.transform;

        var itemParams = item.GetComponent<ItemParams> () ?? item.AddComponent<ItemParams> ();
        var grabbale = item.GetComponent<Grabbable> () ?? item.AddComponent<Grabbable> ();
        var rb = item.GetComponent<Rigidbody> () ?? item.AddComponent<Rigidbody> ();

        itemParams.RollNewParams ();

        rb.AddForce (randomEmitter.transform.forward * Random.Range (150, 200), ForceMode.Acceleration);
        rb.AddTorque (Random.rotation.eulerAngles * Random.Range (50, 100), ForceMode.Impulse);
    }

}