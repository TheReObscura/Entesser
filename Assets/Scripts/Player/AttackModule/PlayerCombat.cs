using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public static PlayerCombat instance;
    public Player player;
    public Transform attackPoint;
    public Vector2 attackSize = new Vector2(1f, 1f);
    public LayerMask enemyLayer;
    private float attackCooldown;

    public GameObject fireballPrefab;
    public Transform castPoint;
    public float manaCost = 50f;
    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (GameInput.instance.Attack())
        {
            Debug.Log("yes");
            Attack();
        }

        UpdateAttackDirection();
    }
    public void CastFireball()
    {
        Vector2 dir = player.lastMoveDirection;

        if (dir == Vector2.zero)
            dir = Vector2.right;

        GameObject fb =
            Object.Instantiate(fireballPrefab, player.transform.position,  Quaternion.identity);
        float damage = player.stats.MagicDamage * player.stats.fireballLevel;
        fb.GetComponent<MagicProjectile>().Init(dir, damage);

    }
    void Attack()
    {
        if (attackCooldown > 0)
            return;

        WeaponData weapon =
            player.itemSystem.equippedWeapon;

        if (weapon == null)
            return;

        attackCooldown = weapon.attackCooldown;

        UpdateAttackDirection();

        Collider2D[] hits =
            Physics2D.OverlapBoxAll(
                attackPoint.position,
                attackSize,
                0,
                enemyLayer
            );

        foreach (var hit in hits)
        {
            Enemy enemy =
                hit.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(weapon.damage);
            }
        }

    }

    void UpdateAttackDirection()
    {
        Vector2 dir =
            player.lastMoveDirection;

        attackPoint.localPosition =
            dir.normalized;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(
            attackPoint.position,
            attackSize
        );
    }
}
