using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Scriptable Objects/SoundData")]
public class SoundData : ScriptableObject
{
    public AudioClip audioClip;
    public string musicTitle;
    public SoundType.soundCategory sType;
}
