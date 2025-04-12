using Unity.Cinemachine;
using UnityEngine;

namespace Script.CameraSystem
{
    public class CameraDirector : MonoBehaviour
    {
        [SerializeField] CinemachineConfiner2D cameraConfiner;

        public void ChangeCameraBound(PolygonCollider2D p)
        {
            cameraConfiner.BoundingShape2D = p;
            cameraConfiner.InvalidateBoundingShapeCache();
        }
    }
}
