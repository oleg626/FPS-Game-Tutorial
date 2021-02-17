using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
	public ItemInfo itemInfo;
	public GameObject itemGameObject;

	[SerializeField] protected int max_current_ammo;
	[SerializeField] protected int max_full_ammo;
	[SerializeField] protected int current_ammo;
	[SerializeField] protected int full_ammo;
	[SerializeField] protected float reload_delay_s;

	protected bool is_on_reload = false;
	protected float reload_start_time = 0;
	public string getCurrentAmmo()
	{
		Debug.Log(current_ammo.ToString());
		return current_ammo.ToString();
	}

	public string getFullAmmo()
	{
		return full_ammo.ToString();
	}
	
	public abstract void Use();
	public abstract void Release();
}