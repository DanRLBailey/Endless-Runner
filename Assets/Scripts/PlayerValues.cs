using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValues : MonoBehaviour
{
    public float stamina;
    public float staminaDecreaseValue;
    public int score;
    public int coins;

    [Header("UI Elements")]
    public Slider staminaSlider;
    public Text scoreText;
    public Text coinsText;

    [Header("Saved Values")]
    public int totalCoins;
    public int highScore;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        stamina -= staminaDecreaseValue;

        UpdateUIElements();
    }

    void StartGame()
    {
        stamina = 1;
        score = 0;
        coins = 0;
    }

    void EndGame()
    {
        if (score > highScore)
            highScore = score;

        totalCoins += coins;
    }

    void UpdateUIElements()
    {
        staminaSlider.value = stamina;
        scoreText.text = score.ToString();
        coinsText.text = coins.ToString();
    }

    public void AddStamina(float staminaToAdd)
    {
        stamina += staminaToAdd;

        stamina = Mathf.Clamp(stamina, 0f, 1f);
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public void AddCoins(int coinsToAdd)
    {
        coins += coinsToAdd;
    }
}
