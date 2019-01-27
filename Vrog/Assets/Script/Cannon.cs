using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cannon : MonoBehaviour {
	public float maxForce;
	public float maxDelayTime;
	public Vector2 target;
	public Stack<Collider2D> ammo;

	public UnityEvent OnStartAiming;
	public UnityEvent OnStopAiming;
	public UnityEvent OnTossing;

	private bool isAiming;
	public bool IsAiming { 
		get{
			return isAiming;
		}
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
	public void Toss(float strengh){
		Collider2D bullet = ammo.Pop();
		if(OnTossing != null)
			OnTossing.Invoke();
		

		Debug.Log("Tossed");
	}

}
