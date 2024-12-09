using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;

public class HUD : MonoBehaviour
{
    private Transport transport;
    private HealthSystem healthSystem;
    private Engine engine;
    private Player player;
    private Score score;

    public Text pHpText;
    public Text tHpText;
    public Text stateText;
    public Text fuelText;
    public Text scoreText;
    public Text hScoreText;

    void Awake()
    {
        transport = FindObjectOfType<Transport>();
        healthSystem = FindObjectOfType<HealthSystem>();
        engine = FindObjectOfType<Engine>();
        player = FindObjectOfType<Player>();
        score = FindObjectOfType<Score>();
    }

    void Update()
    {
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        pHpText.text = "HP: " + math.floor(player.currentHealth);
        tHpText.text = "HP: " + math.floor(healthSystem.currentHealth);
        stateText.text = "State: " + transport.CurrentState;
        fuelText.text = "Fuel: " + math.floor(engine.fuel);
        scoreText.text = "" + math.floor(score.currentScore);
        hScoreText.text = "" + math.floor(score.hightScore);
    }
}
