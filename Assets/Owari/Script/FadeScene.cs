using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScene : MonoBehaviour
{
    private float step_time;

    [SerializeField] private string loadScene;
    [SerializeField] private Color fadeColor = Color.white;
    [SerializeField] private float fadeSpeedMultiplier = 0.5f;

    void Start()
    {
        step_time = 0.0f;       // Œo‰ßŽžŠÔ‰Šú‰»
    }
    private void Update()
    {
        step_time += Time.deltaTime;

        if (step_time >= 6.0f)
        {
            Initiate.Fade("TopScene", fadeColor, fadeSpeedMultiplier);
        }
    }
}


