using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cannon : MonoBehaviour {
	public Transform[] ammoSlots;
	public float maxForce;
	public float maxDelayTime;
	public float colisionDelay;

	[HideInInspector]
	public Vector2 Target {get;set;}
	public Transform targetTransform;
	[HideInInspector]
	public List<Bullet> ammo;
	public int ammoMax;
	public UnityEvent OnStartAiming;
	public UnityEvent OnStopAiming;
	public UnityEvent OnTossing;

	private bool isAiming;
	public bool IsAiming { 
		get{	return isAiming;}
		set{
			if(ammo.Count > 0 && isAiming != value){
				isAiming = value;
				if (isAiming)
					StartCoroutine(LaunchTadpole());
			}
		}
	}

	private bool isTossing;
	public bool IsTossing{
		get{	return isTossing; } 
		set{
			if(isTossing != value){
				if ((IsAiming && value) || !value)
					IsTossing  = value;
			}
		}
	}

	public IEnumerator LaunchTadpole(){
		if(ammo.Count > 0){
			Bullet bullet = PopAmmo();
			float timer = Time.time;
			bullet.Bind(targetTransform);
	
			if(OnStartAiming != null)
				OnStartAiming.Invoke();

			yield return new WaitUntil(() => { 
				targetTransform.position = Target.normalized * Vector3.Distance(targetTransform.position, transform.position);
				return !IsAiming || IsTossing; 
				}
			);

			if(IsTossing){
				Toss(Mathf.Clamp01( (Time.time - timer) / maxDelayTime ),bullet);
				IsTossing = false;

			}else if(OnStopAiming != null){
				OnStopAiming.Invoke();
				if(ammo.Count < ammoMax)
					AddAmmo(bullet);
				else{
					bullet.Release();
				}
			}
			IsAiming = false;
		}
	}
	public void Toss(float strengh, Bullet bullet){
		bullet.Release();
		bullet.rb.AddForce((strengh * maxForce) * (targetTransform.position), ForceMode2D.Impulse);
		bullet.Invoke("Activate", colisionDelay);
		if(OnTossing != null)
			OnTossing.Invoke();
	}
	public void AddAmmo(Bullet bullet){
		bullet.Bind(ammoSlots[ammo.Count]);
		ammo.Add(bullet);
	}
	public Bullet PopAmmo(){
		Bullet bullet = ammo[0]; 
		ammo.RemoveAt(0);
		for (int i = 0; i < ammo.Count; i++)
			ammo[i].magnetTarget = ammoSlots[i];
		return bullet;
	}
	 void OnCollisionEnter2D(Collision2D col)
    {	
		if(ammo.Count < ammoMax){
			Bullet b = col.gameObject.GetComponent<Bullet>();
			if(b != null)
				AddAmmo(b);
		}
    }

}
