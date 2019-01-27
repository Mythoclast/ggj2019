using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour {
    Collider2D col;
    void Awake(){
        col = GetComponent<Collider2D>();
    }
    public Transform magnetTarget;

    void Update(){
        if(magnetTarget != null)
            transform.position = Vector3.Lerp(transform.position, magnetTarget.position, 20f * Time.deltaTime);
    }
}