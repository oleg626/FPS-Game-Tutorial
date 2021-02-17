using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotGun : Gun
{
	[SerializeField] Camera cam;

	PhotonView PV;

	bool activeShoot = false;

	void Awake()
	{
		PV = GetComponent<PhotonView>();
	}

	public override void Use()
	{
		if (!activeShoot)
		{
			Shoot();
			activeShoot = true;
		}
	}

	public override void Release()
	{
		activeShoot = false;
	}

	private void Reload()
	{
		if (current_ammo != 0) return;
		if (full_ammo > 0 && current_ammo == 0)
		{	
			if (full_ammo > max_current_ammo)
			{
				current_ammo = max_current_ammo;
				full_ammo -= max_current_ammo;
			}
			else
			{
				current_ammo = full_ammo;
				full_ammo = 0;
			}
		}
	}
	protected void Shoot()
	{
		if (is_on_reload)
		{
			Reload();
			if (Time.time - reload_start_time < reload_delay_s)
			{
				return;
			}
		}
		
		if (current_ammo > 0)
		{
			current_ammo -= 1;
			is_on_reload = false;
			Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
			ray.origin = cam.transform.position;

			if(Physics.Raycast(ray, out RaycastHit hit))
			{
				hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage);
				PV.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);
			}
		}
		else
		{
 			is_on_reload = true;
			reload_start_time = Time.time;
		}
	}

	[PunRPC]
	protected void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
	{
		Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
		if(colliders.Length != 0)
		{
			GameObject bulletImpactObj = Instantiate(bulletImpactPrefab, hitPosition + hitNormal * 0.001f, Quaternion.LookRotation(hitNormal, Vector3.up) * bulletImpactPrefab.transform.rotation);
			Destroy(bulletImpactObj, 10f);
			bulletImpactObj.transform.SetParent(colliders[0].transform);
		}
	}
}
