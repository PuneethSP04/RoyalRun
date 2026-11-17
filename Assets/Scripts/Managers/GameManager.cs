using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal; // Change to .HighDefinition if using HDRP

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerControler playerControler;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] float startTime = 5f;

    [Header("Post Processing")]
    [SerializeField] Volume postProcessVolume;
    [SerializeField] Color startBloomTint = Color.white; // (255, 255, 255)
    [SerializeField] Color gameOverBloomTint = Color.blue; // (0, 0, 255)
    [SerializeField] float transitionDuration = 1.2f;

    Bloom bloom;
    bool gameOver = false;

    public bool GameOver => gameOver;
    float timeLeft;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        timeLeft = startTime;
        gameOverPanel.SetActive(false);

        // Grab the Bloom override from the Volume profile and set initial color
        if (postProcessVolume != null && postProcessVolume.profile.TryGet(out bloom))
        {
            bloom.tint.overrideState = true;
            bloom.tint.value = startBloomTint;
        }
    }

    void Update()
    {
        DecreaseTime();
    }

    private void DecreaseTime()
    {
        if (gameOver) return;
        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F1");

        if (timeLeft <= 0)
        {
            PlayerGameOver();
        }
    }

    void PlayerGameOver()
    {
        gameOver = true;
        playerControler.enabled = false;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0.1f;

        if (bloom != null)
        {
            StartCoroutine(TransitionBloomTint());
        }
    }

    IEnumerator TransitionBloomTint()
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            // Use unscaledDeltaTime so Time.timeScale = 0.1f doesn't slow down the transition
            elapsedTime += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);
            
            bloom.tint.value = Color.Lerp(startBloomTint, gameOverBloomTint, t);
            yield return null;
        }

        bloom.tint.value = gameOverBloomTint;
    }

    public void IncreaseTimer(float amount)
    {
        timeLeft += amount;
    }
}