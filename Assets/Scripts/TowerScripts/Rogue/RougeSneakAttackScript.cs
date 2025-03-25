using UnityEngine;

public class RougeSneakAttackScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float sneakSpeed = 5f;    // Speed of the projectile
    [SerializeField] private int totalDotDamage = 10;  // Total damage over time
    [SerializeField] private float dotDuration = 3f;   // Duration of DoT effect

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target || !target.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * sneakSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)  
    {
        if (other == null) return;

        Health targetHealth = other.gameObject.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.ApplyDot(totalDotDamage, dotDuration);
        }

        Destroy(gameObject); // Destroy after applying DoT
    }
}