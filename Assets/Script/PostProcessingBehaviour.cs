using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingBehaviour : MonoBehaviour
{


    public Volume volume;

    public AnxietySystem anxietySystem;

    public float anxiety = 0f;

    public Color passiveColor = Color.black;
    public Color panicColor = Color.red;
    public float panicScaleFactor = 1f;
    public float panicMaxDuration = 10f;

    public Vector2 passiveVignetteRange = new Vector2(0f, 0.6f);
    public Vector2 panicVignetteRange = new Vector2(0.2f, 0.7f);
    // public Vector2 panicSpeedRange = new Vector2(1f, 2f);
    // public float beatSpeed = 2f;
    private Vignette vignette;

    public bool panicMode = false;




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

        anxiety = Mathf.Lerp(passiveVignetteRange.x, passiveVignetteRange.y, anxietySystem.anxietyLevel);

        if (panicMode == false) {
            vignette.intensity.value = anxiety;
        }
    }



    public IEnumerator HeartBeatEffect() {

        panicMode = true;

        vignette.color.value = panicColor;

        float timer = 0f;
        // float beatSpeed = panicSpeedRange.x;

        // while (timer <= panicMaxDuration) {
        while(true) {

            float intensity = 1f - (Time.time - Mathf.Floor(Time.time));
            vignette.intensity.value = Mathf.Lerp(panicVignetteRange.x, panicVignetteRange.y, intensity);

            // beatSpeed = Mathf.Lerp(panicSpeedRange.x, panicSpeedRange.y, timer / panicMaxDuration);

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
