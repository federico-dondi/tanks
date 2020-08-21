using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellDamage : MonoBehaviour
{
  [Header("Collision:")]
  [SerializeField] private LayerMask mask;

  [Header("Values:")]
  [SerializeField] private float maxDamage = 100f;
  [SerializeField] private float explosionForce = 1000f;
  [SerializeField] private float explosionRadius = 5f;

  [Header("FX:")]
  [SerializeField] private GameObject explosionPrefab;

  private void OnTriggerEnter(Collider other)
  {
    Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius, mask);

    for (int i = 0; i < cols.Length; i++)
    {
      Rigidbody rigidbody = cols[i].GetComponent<Rigidbody>();

      if (rigidbody)
      {
        rigidbody.AddExplosionForce(
          explosionForce,
          transform.position,
          explosionRadius
        );

        TankHealth health = cols[i].GetComponent<TankHealth>();

        if (health)
          health.TakeDamage(CalculateDamage(rigidbody.position));
      }
    }

    Instantiate(explosionPrefab);

    // Destroy the gameObject
    Destroy(gameObject);
  }

  private float CalculateDamage(Vector3 position)
  {
    Vector3 direction = position - transform.position;

    float explosionDistance = direction.magnitude;
    float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;

    float damage = relativeDistance * maxDamage;

    if (damage < 0f)
      damage = 0f;

    return damage;
  }
}
