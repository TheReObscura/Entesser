using Assets.Scripts.Player;
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    public float speed = 8f;
    public float damage = 15f;
    public float lifeTime = 3f;

    private Vector2 direction;

    public void Init(Vector2 dir, float dmg)
    {
        direction = dir.normalized;
        damage = dmg;

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy =
            other.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}