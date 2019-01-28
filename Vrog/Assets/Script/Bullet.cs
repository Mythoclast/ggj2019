using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour {
    public Collider2D col;
    public Rigidbody2D rb;
    void Awake(){
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    public Transform magnetTarget;

    void Update(){
        if(magnetTarget != null)
            transform.position = Vector3.Lerp(transform.position, magnetTarget.position, 20f * Time.deltaTime);
    }

    public void Release(){
        rb.velocity = Vector2.zero;
        col.enabled = true;
        rb.simulated = true;
        magnetTarget = null;
    }
    public void Bind(Transform target){
        rb.velocity = Vector2.zero;
        col.enabled = false;
        rb.simulated = false;
        magnetTarget = target;
    }
    public void Deactivate(){
		gameObject.layer = 14;
    }
    public void Activate(){
		gameObject.layer = 15;
    }
}