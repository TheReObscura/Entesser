using Assets.Scripts.Items;
using UnityEngine;
using Assets.Scripts.Core.Debug;

public class ItemSpawnerDebug : MonoBehaviour
{
    public ItemWorld itemPrefab;
    public ItemData testItem;

    void Update()
    {
        if (DebugInputCont.instance.SpawnItem())
        {
            Spawn();
        }
    }

    void Spawn()
    {
        var obj = Instantiate(
            itemPrefab,
            transform.position,
            Quaternion.identity
        );

        obj.item = testItem;
        obj.amount = 1;
    }
}