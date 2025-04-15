using Script.Player;
using UnityEngine.SceneManagement;
using UnityEngine;

public class wasdUIBehaviour : MonoBehaviour
{


    private float timer = 0f;
    private bool small = true;

    private Vector3 baseScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (timer >= 0.75f) {

            if (small) {

                transform.localScale = new Vector3(baseScale.x * 1.2f, baseScale.y * 1.2f, 1f);
                small = false;
            }
            else {
                transform.localScale = new Vector3(baseScale.x, baseScale.y, 1f);
                small = true;
            }
            timer = 0f;

        }

        timer += Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            Destroy(gameObject);
        }

    }
}
