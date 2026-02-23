using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"){
            collision.GetComponent<Health>().TakeDamage(damage);
            GetComponent<Health>().TakeDamage(1);
            }
    }
}