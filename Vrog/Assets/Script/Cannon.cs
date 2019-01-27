using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cannon : MonoBehaviour {
	public Transform[] ammoSlots;
	public float maxForce;
	public float maxDelayTime;
	///How long does it take for the bullet to consider colisions agains the player after launched;
	public float colisionDelay;
	public Vector2 target;
	public Stack<Rigidbody2D> ammo;

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
		float timer = Time.time;
		if(OnStartAiming != null)
			OnStartAiming.Invoke();

		yield return new WaitUntil(() => { return !IsAiming || IsTossing; } );

		if(IsTossing){
			Toss(Mathf.Clamp01( (Time.time - timer) / maxDelayTime ));
			IsTossing = false;

		}else if(OnStopAiming != null)
			OnStopAiming.Invoke();

		IsAiming = false;
	}
	public IEnumerator DelayBulletColision(Rigidbody2D bullet){
		yield return new WaitForSeconds(colisionDelay);
		bullet.gameObject.layer = 15;
	}
	public void Toss(float strengh){
		Rigidbody2D bullet = ammo.Pop();

		bullet.AddForce((strengh * maxForce) * target, ForceMode2D.Impulse);
		StartCoroutine(DelayBulletColision(bullet));

		if(OnTossing != null)
			OnTossing.Invoke();
	
	}

}
