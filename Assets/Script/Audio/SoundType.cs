using UnityEngine;

[CreateAssetMenu(fileName = "SoundType", menuName = "Scriptable Objects/SoundType")]
public class SoundType : ScriptableObject
{
    public enum soundCategory
    {
        bgm,
        sfx,
    }
    
}
