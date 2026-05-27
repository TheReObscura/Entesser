using Assets.Scripts.Core.Debug;
using Assets.Scripts.Player;
using UnityEngine;

public class PlayerDebugControls : MonoBehaviour
{
    public  Player player;

    public  void DamageHP()
    {
        Debug.Log("Here 1");
        player.stats.TakePhysicalDamage(10f);
    }

    public  void DamageMana()
    {
        Debug.Log("Here 2");
        player.stats.UseMana(10f);
    }

    public  void Heal()
    {
        Debug.Log("Here 3");
        player.stats.Heal(10f);
    }
    void Update()
    {
        if (DebugInputCont.instance.DealDamage()) DamageHP();
        if(DebugInputCont.instance.Heal()) Heal();
        if(DebugInputCont.instance.TakeMana()) DamageMana();
    }
}