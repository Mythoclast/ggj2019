using UnityEngine;
using UnityEngine.Events;
//[RequireComponent(typeof(Animator), typeof(BasicMovement))]
public class CharacterAnimationControl : MonoBehaviour {

	public Animator Animator {get; private set;}

	public BasicMovement BasicMovement{get;private set;}

	void Awake () {
		Animator = GetComponent<Animator>();
		BasicMovement = GetComponent<BasicMovement>();
	}

	private float speedFactor = 0.0f;

	public float SpeedFactor { 
		get{return speedFactor;} 
		private set{
			speedFactor = value;
			Animator.SetFloat("Walk",value);
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
	
	void Update(){
		SpeedFactor = Input.GetAxis("Horizontal");
		if(Mathf.Abs(SpeedFactor) > 0.2f)
			BasicMovement.Walk(SpeedFactor);
		
		IsJumping = Input.GetAxis("Jump") == 1.0f;
	}
	
	public void ExecuteJump(){
		BasicMovement.Jump(SpeedFactor);
	}
	
}
