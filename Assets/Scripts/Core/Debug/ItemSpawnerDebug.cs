using Assets.Scripts.Items;
using UnityEngine;

public class ItemSpawnerDebug : MonoBehaviour
{
    public ItemWorld itemPrefab;
    public ItemData testItem;

    void Update()
    {
        if (GameInput.instance.IsDebugSpawn())
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