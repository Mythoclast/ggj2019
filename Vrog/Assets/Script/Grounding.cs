using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Grounding : MonoBehaviour {
	public bool IsGrounded {get;private set;}
	public Transform[] rayTargets;
	[SerializeField]
	public UnityEvent OnGround;

	[SerializeField]
	public UnityEvent OnUnGround;

	private RaycastHit2D hit;
	void FixedUpdate(){
		Vector3 c;
		bool result = false;
		for (int i = 0; i < rayTargets.Length && !result; i++) {
			c = rayTargets[i].position;
			c.y = transform.position.y;
			hit = Physics2D.Raycast(c, Vector2.down, Vector2.Distance(c,rayTargets[i].position), 1<<8 | 1<<15);
			result = hit.collider != null;
		}
		if(result != IsGrounded){
			IsGrounded = result;
			if(IsGrounded){
				OnGround.Invoke();
				if(hit.transform != null)
					foreach (ISteppable trigger in hit.transform.GetComponents<ISteppable>())
						trigger.OnStep();
			}
			else{
				OnUnGround.Invoke();
				if(hit.transform != null)
					foreach (ISteppable trigger in hit.transform.GetComponents<ISteppable>())
						trigger.OnRelease();
			}
		}
	}
	void OnDrawGizmosSelected(){
		Vector3 c;
		for (int i = 0; i < rayTargets.Length; i++) {
			c = rayTargets[i].position;
			c.y = transform.position.y;
			Gizmos.DrawRay(c,Vector2.down *Vector2.Distance(c,rayTargets[i].position));
		}
	}

}
