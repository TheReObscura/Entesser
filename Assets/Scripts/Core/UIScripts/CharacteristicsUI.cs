using Assets.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacteristicsUI : MonoBehaviour
{

    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI magicText;
    public TextMeshProUGUI survivalText;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI manaText;

    public TextMeshProUGUI pointsText;
    public Player player;
    void Start()
    {
        UpdateUI();
    }
    void UpdateUI()
    {
        var s = player.stats;

        strengthText.text = "STR: " + s.strength;
        magicText.text = "MAG: " + s.magic;
        survivalText.text = "SUR: " + s.survival;

        hpText.text = "HP: " + s.currentHP + " / " + s.MaxHP;
        manaText.text = "Mana: " + s.currentMana + " / " + s.MaxMana;

        pointsText.text = "Points: " + s.upgradePoints;
    }
    public void IncreaseStrength()
    {
        player.stats.IncreaseStrength();
        UpdateUI();
    }

    public void DecreaseStrength()
    {
        player.stats.DecreaseStrength();
        UpdateUI();
    }

    public void IncreaseMagic()
    {
        player.stats.IncreaseMagic();
        UpdateUI();
    }

    public void DecreaseMagic()
    {
        player.stats.DecreaseMagic();
        UpdateUI();
    }

    public void IncreaseSurvival()
    {
        player.stats.IncreaseSurvival();
        UpdateUI();
    }

    public void DecreaseSurvival()
    {
        player.stats.DecreaseSurvival();
        UpdateUI();
    }

    public void ResetStats()
    {
        player.stats.ResetStats();
        UpdateUI();
    }

}