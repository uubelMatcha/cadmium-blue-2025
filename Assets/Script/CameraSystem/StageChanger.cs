using UnityEngine;

[CreateAssetMenu(fileName = "AreaTransitioner", menuName = "Scriptable Objects/AreaTransitioner")]
public class AreaTransitioner : ScriptableObject
{
    public Transform anchorPoint;
    public Transform exitPoint;
    public AreaTransitioner exit;
}


