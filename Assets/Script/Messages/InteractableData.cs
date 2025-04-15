using UnityEngine;

[CreateAssetMenu(fileName = "InteractableData", menuName = "Scriptable Objects/InteractableData")]
public class InteractableData : ScriptableObject
{
    public string itemName;
    public string level1;
    public string level2;
    public string level3;
    public SoundData sfx;

}
