using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicMovement : MonoBehaviour {
	private Rigidbody2D rb2D;
		public SpriteRenderer spriteRenderer;
	public float moveSpeed;
	public Vector2 jumpVerticalDirection;
	public float jumpVerticalStrengh;
	public Vector2 jumpHorizontalDirection;
	public float jumpHorizontalStrengh;

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
		OnJump.Invoke();
		if(Mathf.Abs(factor)>0){
			rb2D.AddForce(jumpVerticalDirection * jumpVerticalStrengh, ForceMode2D.Impulse);
			OnJumpHorizontal.Invoke();
		}else{
			rb2D.AddForce(jumpHorizontalDirection * jumpHorizontalStrengh, ForceMode2D.Impulse);
			OnJumpVertical.Invoke();
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, jumpVerticalDirection.normalized * jumpVerticalStrengh);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, jumpHorizontalDirection.normalized * jumpHorizontalStrengh);
	}
}

public class FloatUnityEvent:UnityEvent<float>{}