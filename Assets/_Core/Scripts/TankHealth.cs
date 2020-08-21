using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
  [SerializeField] private int enemyNumber = 0;

  [Header("Values:")]
  [SerializeField] private float minHealth = 0f;
  [SerializeField] private float maxHealth = 100f;

  [Header("UI:")]
  [SerializeField] private Slider slider;
  [SerializeField] private Image image;
  [SerializeField] private Color maxHealthColor = Color.green;
  [SerializeField] private Color minHealthColor = Color.red;

  [Header("FX:")]
  [SerializeField] private GameObject explosionPrefab;

  private float currentHealth;

  private void OnEnable()
  {
    currentHealth = maxHealth;

    SetHealthUI();
  }

  public void TakeDamage(float amount)
  {
    currentHealth -= amount;

    if (currentHealth <= 0f)
      currentHealth = 0f;

    if (IsDead()) OnDeath();

    SetHealthUI();
  }

  private void SetHealthUI()
  {
    slider.value = currentHealth;

    image.color = Color.Lerp(
        minHealthColor,
        maxHealthColor,
        currentHealth / maxHealth
    );
  }

  private void OnDeath()
  {
    Instantiate(explosionPrefab);

    // Disable the gameObject
    gameObject.SetActive(false);

    FindObjectOfType<GameManager>().FinishRound(enemyNumber);
  }

  private bool IsDead()
  {
    return currentHealth <= 0f;
  }
}
