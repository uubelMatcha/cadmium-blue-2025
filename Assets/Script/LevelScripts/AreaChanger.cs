using System;
using Script.CameraSystem;
using Unity.VisualScripting;
using UnityEngine;

public class AreaChanger : MonoBehaviour
{
   private enum Direction {VERTICAL, HORIZONTAL}

   [SerializeField] private Direction directionToMovePlayer;
   [SerializeField] private PolygonCollider2D newAnchorPoint;
   [SerializeField] private float moveBump;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GameObject().CompareTag("Player"))
        {
            GameObject.FindWithTag("Director").GetComponent<CameraDirector>().ChangeCameraBound(newAnchorPoint);
            Debug.Log("ssss");
            switch (directionToMovePlayer)
            {
                case Direction.VERTICAL:
                    other.transform.position =
                        new Vector3(other.transform.position.x, other.transform.position.y + moveBump, other.transform.position.z);
                    break;
                case Direction.HORIZONTAL:
                    other.transform.position =
                        new Vector3(other.transform.position.x + moveBump, other.transform.position.y, other.transform.position.z);
                    break;
            }
        }
    }
}
