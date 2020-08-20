using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
  private Quaternion relativeRotation;

  private void Start()
  {
    relativeRotation = transform.parent.localRotation;
  }

  private void Update()
  {
    transform.rotation = relativeRotation;
  }
}
