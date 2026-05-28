using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public float hp;
    public float maxHp = 30f;
    public float moveSpeed = 2f;

    public float damage = 5f;

    public float attackDistance = 1.2f;

    public float attackCooldown = 1f;

    private float attackTimer;

    private Player player;
    public int expReward = 25;
    private void Start()
    {
        hp = maxHp;
        player =
            FindFirstObjectByType<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (player == null)
            return;

        float dist =
            Vector2.Distance(transform.position, player.transform.position);
   
        if (dist > attackDistance)
        {
            MoveToPlayer();
        }
        else
        {
            StopAndAttack();
        }
    }

    void MoveToPlayer()
    {
        Vector2 dir =
    (player.transform.position - transform.position)
    .normalized;

        rb.linearVelocity = dir * moveSpeed;
    }

    void StopAndAttack()
    {
        rb.linearVelocity = Vector2.zero;

        attackTimer -= Time.deltaTime;

        if (attackTimer > 0)
            return;

        attackTimer = attackCooldown;

        DoAttack();
    }
    void DoAttack()
    {
        var p =
            FindFirstObjectByType<Player>();

        if (p == null)
        {
            Debug.Log("Player NOT FOUND");
            return;
        }

        p.stats.TakePhysicalDamage(damage);

        Debug.Log("Player damaged");
    }
    public void TakeDamage(float dmg)
    {
        hp -= dmg;

        Debug.Log("Slime hit");

        if (hp <= 0)
            Die();
    }

    void Die()
    {
        PlayerLevelSystem level =
            FindFirstObjectByType<PlayerLevelSystem>();

        if (level != null)
        {
            level.AddXP(expReward);
        }

        Destroy(gameObject);
    }
}
