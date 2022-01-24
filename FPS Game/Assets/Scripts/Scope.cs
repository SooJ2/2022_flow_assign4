using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
  public Animator animator;
  public Camera mainCamera;

  private bool isScoped = false;

  public float scopedFOV;
  private float normalFOV = 60f;

  public GameObject scopeOverlay;
  public GameObject crossHair;


  public void handleScope(bool isScoped)
  {
    animator.SetBool("scoped", isScoped);

    scopeOverlay.SetActive(isScoped);
    crossHair.SetActive(!isScoped);

    if (isScoped)
      onScoped();
    else
      onUnScoped();
  }


  void onScoped()
  {
    // normalFOV = mainCamera.fieldOfView;
    mainCamera.fieldOfView = scopedFOV;
  }

  void onUnScoped()
  {
    mainCamera.fieldOfView = normalFOV;
  }
}
