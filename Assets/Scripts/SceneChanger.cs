using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    enum FadeStatus
    {
        fading_id,
        fading_out,
        none
    }

    public static SceneChanger Instance;
    public Image fadeImage;
    public float fadeDuration;

    private FadeStatus currentFadeStatus = FadeStatus.none;
    private float fadeTimer;
    private int sceneToLoad;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentFadeStatus = FadeStatus.fading_id;
    }

    public void ChangeScene(int _sceneId)
    {
        sceneToLoad = _sceneId;
        currentFadeStatus = FadeStatus.fading_out;
    }

    void Update()
    {
        if(currentFadeStatus != FadeStatus.none)
        {
            fadeTimer += Time.deltaTime;

            if(fadeTimer > fadeDuration)
            {
                fadeTimer = 0;

                if (currentFadeStatus == FadeStatus.fading_out)
                {
                    SceneManager.LoadSceneAsync(sceneToLoad);
                    fadeImage.color = Color.white;
                }
                else
                    fadeImage.color = Color.clear;

                currentFadeStatus = FadeStatus.none;
            }
            else
            {
                float alpha = 0;
                if (currentFadeStatus == FadeStatus.fading_out)
                    alpha = Mathf.Lerp(0, 1, fadeTimer / fadeDuration);
                else
                    alpha = Mathf.Lerp(1, 0, fadeTimer / fadeDuration);

                fadeImage.color = new Color(1, 1,1, alpha);
            }
        }
    }
}