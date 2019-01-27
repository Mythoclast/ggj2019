using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Grounding : MonoBehaviour {
	public bool IsGrounded {get;private set;}

	[SerializeField]
	public UnityEvent OnGround;

	[SerializeField]
	public UnityEvent OnUnGround;

	void OnTriggerEnter2D(Collider2D col){
		IsGrounded = true;
		OnGround.Invoke();
		foreach (ISteppable trigger in col.GetComponents<ISteppable>())
			trigger.OnStep();
	}

	void OnTriggerExit2D(Collider2D col){
		IsGrounded = false;
		OnUnGround.Invoke();

		foreach (ISteppable trigger in col.GetComponents<ISteppable>())
			trigger.OnRelease();
	}
}
