using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShoot : MonoBehaviour
{
  public int playerNumber = 1;

  [Header("Values:")]
  [SerializeField] private GameObject shellPrefab;
  [SerializeField] private Transform firePoint;
  [SerializeField] private float minLaunchForce = 15f;
  [SerializeField] private float maxLaunchForce = 30f;
  [Space]
  [SerializeField] private float maxChargingTime = 0.75f;

  [Header("Audio:")]
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip shotFired;
  [SerializeField] private AudioClip shotCharging;

  [Header("UI:")]
  [SerializeField] private Slider slider;

  private string fireButton = "";
  private float currentLaunchForce;
  private float chargeSpeed;
  private bool isFired;

  private void OnEnable()
  {
    slider.value = currentLaunchForce = minLaunchForce;
  }

  private void Start()
  {
    fireButton = "Fire " + playerNumber;

    chargeSpeed = (
      maxLaunchForce -
      minLaunchForce
    ) / maxChargingTime;
  }

  private void Update()
  {
    slider.value = minLaunchForce;

    if (currentLaunchForce >= maxLaunchForce && !isFired)
    {
      currentLaunchForce = maxLaunchForce;

      // Instantiate the shell GameObject
      Fire();
    }
    else if (Input.GetButtonDown(fireButton))
    {
      isFired = false;
      currentLaunchForce = minLaunchForce;

      audioSource.clip = shotCharging;
      audioSource.Play();
    }
    else if (Input.GetButton(fireButton) && !isFired)
    {
      slider.value = currentLaunchForce += chargeSpeed * Time.deltaTime;
    }
    else if (Input.GetButtonUp(fireButton) && !isFired)
    {
      // Instantiate the shell GameObject
      Fire();
    }
  }

  private void Fire()
  {
    isFired = true;

    Rigidbody rigidbody = Instantiate(
        shellPrefab,
        firePoint.position,
        firePoint.rotation).GetComponent<Rigidbody>();

    rigidbody.velocity = currentLaunchForce * firePoint.forward; ;

    audioSource.clip = shotFired;
    audioSource.Play();

    currentLaunchForce = minLaunchForce;
  }
}
