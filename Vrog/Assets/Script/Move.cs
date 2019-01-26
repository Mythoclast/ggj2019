using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	public float moveStrengh;
	public Vector2 jumpVerticalAngle;
	public float jumpVerticalStrengh;
	public Vector2 jumpHorizontalAngle;
	public float jumpHorizontalStrengh;
	public float groundingDistance;
	public float groundingSpace;
	private Rigidbody2D rb2D;

	public void Walk(Vector2 factor){
		rb2D.AddForce(factor * moveStrengh, ForceMode2D.Force);
	}
	private Collider2D Ground { 
		get{
			RaycastHit2D hit = Physics2D.Raycast(transform.position.ToVector2() + Vector2.left  * groundingSpace, Vector2.down, groundingDistance, ~( (1<<9) | (1<<10) | (1<<12) | (1<<13) ));
			if(hit.collider == null)
				hit = 		   Physics2D.Raycast(transform.position.ToVector2() + Vector2.right * groundingSpace, Vector2.down, groundingDistance, ~( (1<<9) | (1<<10) | (1<<12) | (1<<13) ));
			return hit.collider;
		}
	}
	public void HorizontalJump(){
		Jump(jumpVerticalAngle, jumpVerticalStrengh);
	}
	public void VerticalJump(){
		Jump(jumpHorizontalAngle, jumpHorizontalStrengh);
	}
	private void Jump(Vector2 angle, float force){
		if (Ground != null)
			rb2D.AddForce(angle * force, ForceMode2D.Impulse);

	}
	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, jumpVerticalAngle.normalized * jumpVerticalStrengh);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, jumpHorizontalAngle.normalized * jumpHorizontalStrengh);
		Gizmos.color = Color.white;
		//center + left*displace + down*distance
		Gizmos.DrawLine(transform.position + Vector3.left  * groundingSpace, transform.position + Vector3.left * groundingSpace + Vector3.down * groundingDistance);
		Gizmos.DrawLine(transform.position + Vector3.right * groundingSpace, transform.position + Vector3.right * groundingSpace + Vector3.down * groundingDistance);
		
	}
}
