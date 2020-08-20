using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
  public int playerNumber = 1;

  [Header("Values:")]
  [SerializeField] private new Rigidbody rigidbody;
  public float movementSpeed = 12f;
  public float rotationSpeed = 180f;

  [Header("Audio:")]
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip engineIdle;
  [SerializeField] private AudioClip engineDriving;

  [Space]
  [SerializeField] private float pitchRange = 0.2f;

  private string horizontalAxis = "";
  private string verticalAxis = "";

  private float horizontalInput;
  private float verticalInput;

  private float originalPitch;

  private void OnEnable()
  {
    rigidbody.isKinematic = false;

    horizontalInput = 0f;
    verticalInput = 0f;
  }

  private void OnDisable()
  {
    rigidbody.isKinematic = true;
  }

  private void Start()
  {
    horizontalAxis = "Horizontal " + playerNumber;
    verticalAxis = "Vertical " + playerNumber;

    originalPitch = audioSource.pitch;
  }

  private void Update()
  {
    horizontalInput = Input.GetAxis(horizontalAxis);
    verticalInput = Input.GetAxis(verticalAxis);

    PlayAudio();
  }

  private void FixedUpdate()
  {
    Move();
    Rotate();
  }

  private void Move()
  {
    Vector3 movement = transform.forward * verticalInput * movementSpeed * Time.fixedDeltaTime;

    rigidbody.MovePosition(rigidbody.position + movement);
  }

  private void Rotate()
  {
    Quaternion rotation = Quaternion.Euler(
        0.0f,
        horizontalInput * rotationSpeed * Time.fixedDeltaTime,
        0.0f
    );

    rigidbody.MoveRotation(rigidbody.rotation * rotation);
  }

  private void PlayAudio()
  {
    if (Mathf.Abs(horizontalInput) < 0.1f && Mathf.Abs(verticalInput) < 0.1f)
    {
      if (audioSource.clip == engineDriving)
      {
        audioSource.clip = engineIdle;
        audioSource.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
        audioSource.Play();
      }
    }
    else
    {
      if (audioSource.clip == engineIdle)
      {
        audioSource.clip = engineDriving;
        audioSource.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
        audioSource.Play();
      }
    }
  }
}
