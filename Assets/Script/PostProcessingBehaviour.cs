using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingBehaviour : MonoBehaviour
{


    public Volume volume;

    public float anxiety = 0f;

    public Color passiveColor = Color.black;
    public Color panicColor = Color.red;
    public float panicScaleFactor = 1f;
    public float panicMaxDuration = 10f;
    public Vector2 panicRange = new Vector2(0.2f, 0.8f);
    private Vignette vignette;

    private bool panicMode = false;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vignette tmp;
        if (volume.profile.TryGet<Vignette>(out tmp))
        {
            vignette = tmp;
        }

        // StartCoroutine(HeartBeatEffect());
    }

    // Update is called once per frame
    void Update()
    {
        if (panicMode == false) {
            vignette.intensity.value = anxiety;
        }
    }



    public IEnumerator HeartBeatEffect() {

        panicMode = true;

        vignette.color.value = panicColor;

        float timer = 0f;
        float beatSpeed = 1f;

        while (timer <= panicMaxDuration) {

            float intensity = 1f - (Time.time * beatSpeed - Mathf.Floor(Time.time * beatSpeed));
            vignette.intensity.value = Mathf.Lerp(panicRange.x, panicRange.y, intensity);

            beatSpeed = Mathf.Lerp(1f, 3f, timer / panicMaxDuration);

            timer += Time.deltaTime;

            // Set panic mode to false to end
            if (panicMode == false) {
                break;
            }

            yield return null;
        }

        vignette.color.value = passiveColor;
        vignette.intensity.value = anxiety;
        panicMode = false;

    }
}
