using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimationControl : MonoBehaviour {
	public Animator Animator {get; private set;}

	// Use this for initialization
	void Awake () {
		Animator = GetComponent<Animator>();
	}
	public bool IsGrounded {  
		set{	Animator.SetBool("IsGrounded", value);} }

	public bool IsWalking { 
		set{	Animator.SetBool("IsWalking", value);} }

	public bool IsAiming {	
		set{	Animator.SetBool("IsAiming", value);} }
	// Update is called once per frame
}
