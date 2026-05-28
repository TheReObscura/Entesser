using Assets.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellUpgradeUI : MonoBehaviour
{

    public TextMeshProUGUI fireballText;
    public Player player; 
    void Start()
    {

        Refresh();
    }

    void Refresh()
    {
        fireballText.text = "Fireball : lvl " + player.stats.fireballLevel;
    }

    public void UpgradeFireball()
    {
        player.stats.IncreaseFireBall();
        Refresh();
    }

    public void DowngradeFireball()
    {
        player.stats.DecreaseFireBall();
        Refresh();
    }
}