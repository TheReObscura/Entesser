using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Image hpBar;
    public Image manaBar;
    public Image xpBar;

    private Player player;
    public PlayerLevelSystem levelSystem;
    private void Start()
    {
       //Debug.Log(Player.Instance);
        player = Player.Instance;
    }

    private void Update()
    {
        if (player == null) return;
        if (levelSystem == null) return;

        float xpNeeded = levelSystem.XPToNextLevel();

        xpBar.fillAmount = (float)levelSystem.currentXP / xpNeeded;

        hpBar.fillAmount = player.stats.currentHP / player.stats.MaxHP;
        manaBar.fillAmount = player.stats.currentMana / player.stats.MaxMana;
       
    }
}