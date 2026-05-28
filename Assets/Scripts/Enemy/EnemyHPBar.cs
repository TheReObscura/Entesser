
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public Image fill;
    public Enemy enemy;

    void Update()
    {
        if (enemy == null) return;

        fill.fillAmount = enemy.hp / enemy.maxHp;
    }
    void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
}