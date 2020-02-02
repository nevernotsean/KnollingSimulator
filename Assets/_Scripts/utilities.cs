using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
  public class Array
  {
    string[] getNonRepeatingStrings (string[] deck, int count)
    {
      string[] tempDeck = deck;
      List<string> results = new List<string> ();

      for (int i = 0; i < count; i++)
      {
        int randomIndex = Random.Range (0, tempDeck.Length);
        results.Add (tempDeck[randomIndex]);
        tempDeck = removeArrayEntry (tempDeck, randomIndex);
      }

      return results.ToArray ();
    }

    string[] removeArrayEntry (string[] deck, int index)
    {
      List<string> results = new List<string> ();

      for (int i = 0; i < deck.Length; i++)
      {
        if (i != index)
          results.Add (deck[i]);
      }

      return results.ToArray ();
    }
  }
}