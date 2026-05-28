using Assets.Scripts.Items;
using UnityEngine;
using Assets.Scripts.Core.Debug;

public class ItemSpawnerDebug : MonoBehaviour
{
    public ItemWorld itemPrefab;
    public WeaponWorld weaponPrefab;
    public ItemData testItem;
    public WeaponData testWeapon;

    void Update()
    {
        if (DebugInputCont.instance.SpawnItem())
        {

            var obj = Instantiate(
                itemPrefab,
                transform.position,
                Quaternion.identity
            );

            obj.item = testItem;
            obj.amount = 1;
        }
        if (DebugInputCont.instance.SpawnWeapon())
        {
            var obj = Instantiate(
            weaponPrefab,
            transform.position,
            Quaternion.identity);

            obj.item = testWeapon;
            obj.amount = 1;
        }
    }

}