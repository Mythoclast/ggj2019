using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicMovement : MonoBehaviour {
	private Rigidbody2D rb2D;
	public SpriteRenderer spriteRenderer;
	public float moveSpeed;
	public float jumpVerticalForceUpward;
	public float jumpHorizontalForceForward;
	public float jumpHorizontalForceUpward;

	public FloatUnityEvent OnWalk;
	public UnityEvent OnJump;
	public UnityEvent OnJumpVertical;
	public UnityEvent OnJumpHorizontal;

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
		Vector2 force = rb2D.velocity;;
		if(OnJump != null)
			OnJump.Invoke();
			
		if(Mathf.Abs(factor)>0){
			force.y = jumpHorizontalForceUpward;
			rb2D.velocity = force;
			rb2D.AddForce((jumpHorizontalForceForward * ((factor>0f)?1f:-1f))* Vector2.right, ForceMode2D.Impulse);

			if(OnJumpVertical != null)
				OnJumpVertical.Invoke();
		}else{
			force.y = jumpVerticalForceUpward;
			rb2D.velocity = force;
			if(OnJumpHorizontal != null)
				OnJumpHorizontal.Invoke();
		}
	}

}

public class FloatUnityEvent:UnityEvent<float>{}