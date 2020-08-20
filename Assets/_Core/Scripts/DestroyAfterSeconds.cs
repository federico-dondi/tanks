using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
  [SerializeField] private float afterSeconds;

  private void Start()
  {
    Destroy(gameObject, afterSeconds);
  }
}
