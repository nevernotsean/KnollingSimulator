using UnityEngine;

public class ItemParams : MonoBehaviour
{
  public int type;
  public int quality;
  public int color;

  public string Name;
  public string Description;

  string[] objectTypeContants = { "fish", "coin", "potion", "flask", "apple", "???" };
  string[] qualityConstants = { "beauty", "durability", "harm", "healing" };
  string[] colorConstants = { "ruby", "emerald", "sapphire", "citrine" };

  string[] beautyStrings = { "is ugly", "is rough looking", "is pretty", "is gorgeous", "uh, well, I would marry it" };
  string[] qualityStrings = { "was made by an ametuer", "was made by a novice", ", well, isn't falling apart at least", "is of fine quality", "is masterfully crafted" };
  string[] harmStrings = { "is completely harmless", "is mostly harmless", "is papercut-causing", "is poisonous", "is deadly" };
  string[] healStrings = { "could cure a cold", "is nutritious", "is healthy", "is life-giving", "could raise the dead" };

  Color[] colorValues = { Color.red, Color.green, Color.blue, Color.yellow };

  public Renderer applyColorTo;
  public bool addMaterial = false;

  private void Awake ()
  {
    if (applyColorTo == null) applyColorTo = GetComponent<Renderer> ();
  }

  public void RollNewParams ()
  {
    if (Name != null && Name != "")
      name = Name;

    type = getIndexOfType (Name);
    quality = Random.Range (0, qualityConstants.Length);
    color = Random.Range (0, colorConstants.Length);

    setMaterial (color);

    Name = color + " " + Name + "of " + quality;
    Description = getDescription (qualityConstants[quality]);
  }

  int getIndexOfType (string Name)
  {
    var i = 0;
    foreach (var type in objectTypeContants)
    {
      if (type == Name) return i;
      i++;
    }

    return 5;
  }

  string getDescription (string quality)
  {
    string desc = "";
    string[] pres = { "Wow, it ", "Huh, it ", "Hmm, it " };
    string[] posts = { "Neat.", "Dope.", "Cool.", "What a find." };

    string pre = pres[Random.Range (0, pres.Length)];
    string post = posts[Random.Range (0, posts.Length)];
    print (quality);
    switch (quality)
    {
      case "beauty":
        desc = desc + beautyStrings[Random.Range (0, beautyStrings.Length)];
        break;
      case "durability":
        desc = desc + qualityStrings[Random.Range (0, qualityStrings.Length)];
        break;
      case "harm":
        desc = desc + harmStrings[Random.Range (0, harmStrings.Length)];
        break;
      case "healing":
        desc = desc + healStrings[Random.Range (0, healStrings.Length)];
        break;
      default:
        return "This is a really nice item." + post;
    }

    var message = pre + desc + ". " + post;
    message = message.Replace ("t is", "ts ");

    return message;
  }

  void setMaterial (int colorIndex)
  {
    // applyColorTo.
  }
}