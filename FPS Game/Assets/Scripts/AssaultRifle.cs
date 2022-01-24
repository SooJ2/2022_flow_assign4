using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AssaultRifle : Gun
{

  [SerializeField] Camera cam;

  PhotonView PV;
  public ParticleSystem muzzleFlash;

  public Transform itemBox;


  void Awake()
  {
    PV = GetComponent<PhotonView>();
  }

  public override void Use()
  {
    Shoot();
  }

  void Shoot()
  {
    Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
    ray.origin = cam.transform.position;
    if (Physics.Raycast(ray, out RaycastHit hit))
    {
      hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage);
      PV.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);
    }

    Quaternion rotUp1 = Quaternion.Euler(-0.5f, 0, 0);
    // Quaternion rotUp2 = Quaternion.Euler(0, 0, Random.value - 1);

    itemBox.rotation = itemBox.rotation * rotUp1;
    // itemBox.rotation = itemBox.rotation * rotUp2;

    // itemBox.Rotate(-1, Random.value - 1, 0);

    muzzleFlash.Play();
  }

  [PunRPC]
  void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
  {
    Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
    if (colliders.Length != 0)
    {
      GameObject bulletImpactObj = Instantiate(bulletImpactPrefab, hitPosition + hitNormal * 0.001f, Quaternion.LookRotation(hitNormal, Vector3.up) * bulletImpactPrefab.transform.rotation);
      Destroy(bulletImpactObj, 10f);
      bulletImpactObj.transform.SetParent(colliders[0].transform);
    }
  }
}
