using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
	public float maxForce;
	public float maxDelayTime;
	public Vector2 target;
	public Stack<GameObject> ammo;


	private bool isAiming;
	public bool IsAiming { 
		get{
			return isAiming;
		}
		set{
			if(isAiming != value){
				isAiming = value;
				if (isAiming)
					StartCoroutine(LaunchTadpole());
			}
		}
	}
	public bool IsTossing{
		get; set;
	}

	public IEnumerator LaunchTadpole(){
		float timer = Time.time;
		yield return new WaitUntil(() => { return !IsAiming || IsTossing; } );
		if(IsTossing){
			Toss(Mathf.Clamp01( (Time.time - timer) / maxDelayTime ));
			IsTossing = false;
		}
		IsAiming = false;
	}


	public void Toss(float strengh){
		Debug.Log("Tossed");
	}

}
