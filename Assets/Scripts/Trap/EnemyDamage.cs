using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { // Pakai CompareTag lebih efisien di Unity
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}