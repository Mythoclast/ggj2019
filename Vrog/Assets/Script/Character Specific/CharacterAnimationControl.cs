using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAnimationControl : MonoBehaviour {

	public Animator Animator {get; private set;}

	public BasicMovement BasicMovement{get;private set;}

	public Rigidbody2D Rigidbody2D{get;private set;}

	public Cannon Cannon{get;private set;}


	void Awake () {
		Cannon = GetComponent<Cannon>();
		Animator = GetComponent<Animator>();
		BasicMovement = GetComponent<BasicMovement>();
		Rigidbody2D = GetComponent<Rigidbody2D>();
	}
	private bool direction;
	public bool Direction { get{return direction; } private set {
		if(direction != value)
			direction = value;
			Animator.SetBool("Direction",value);
		}
	}
	private float speedFactor = 0.0f;

	public float SpeedFactor { 
		get{return speedFactor;} 
		private set{
			speedFactor = value;
			Animator.SetFloat("Walk",Mathf.Abs(value));
		} 
	}
	private float verticalVelocity;
	public float VerticalVelocity{
		get{return verticalVelocity;}
		private set{
			verticalVelocity = value;
			Animator.SetFloat("VerticalVelocity",verticalVelocity);
		}
	}



	public bool IsGrounded {  
		set{	Animator.SetBool("IsGrounded", value);} }

	public bool IsWalking { 
		set{	Animator.SetBool("IsWalking", value);} }

	public bool IsAiming {	
		set{	Animator.SetBool("IsAiming", value);} }

	private bool isJumping = false;
	public bool IsJumping{
		get{return isJumping;}
		private set{
			if(isJumping != value)
				isJumping = value;
				Animator.SetBool("IsJumping", value);
		}
	}
	
	void FixedUpdate(){
		SpeedFactor = Input.GetAxis("Horizontal");
		



//------------------------------------------
		
		if(SpeedFactor>0)
			BasicMovement.LookLeft();

		
		else if(SpeedFactor<0)
			BasicMovement.LookRight();

//------------------------------------------
		if(Input.GetAxis("Aim")>0.5f){
			
		}


	
		VerticalVelocity = Rigidbody2D.velocity.y;

		if(Mathf.Abs(SpeedFactor) > 0.2f)
			BasicMovement.Walk(SpeedFactor);
		
		IsJumping = Input.GetAxis("Jump") == 1.0f;
	}
	
	public void ExecuteJump(){
		BasicMovement.Jump(SpeedFactor);
	}

}
