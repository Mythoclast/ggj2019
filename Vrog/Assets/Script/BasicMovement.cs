using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicMovement : MonoBehaviour {
	private Rigidbody2D rb2D;
	public SpriteRenderer spriteRenderer;
	public float moveSpeed;
	public float jumpForceUpward;
	public FloatUnityEvent OnWalk;
	public UnityEvent OnJump;
	public bool IsStopped{get;set;}

	void Awake(){
		rb2D = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		IsStopped = false;
	}

	public void LookRight(){
		if(!IsStopped)
			spriteRenderer.flipX = true;
	}
	public void LookLeft(){
		if(!IsStopped)
			spriteRenderer.flipX = false;
	}

	public void Walk(float factor){
		if(!IsStopped){
			rb2D.AddForce(Vector2.right * moveSpeed * factor, ForceMode2D.Force);
			if(OnWalk!=null)
				OnWalk.Invoke(Mathf.Abs(factor));
		}
	}

	public void Jump(float factor){
		if(OnJump != null)
			OnJump.Invoke();

		Vector2 force = rb2D.velocity;;
		force.y = jumpForceUpward;
		rb2D.velocity = force;

	}

}

public class FloatUnityEvent:UnityEvent<float>{}