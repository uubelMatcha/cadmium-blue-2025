using Script.CameraSystem;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject director;

    public CameraDirector GetDirector()
    {
        return director.GetComponent<CameraDirector>();
    }

}
