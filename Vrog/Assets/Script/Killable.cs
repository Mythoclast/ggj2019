using UnityEngine;
using UnityEngine.Events;

public class Killable : MonoBehaviour {
    void Awake(){
        IsDead = false;
    }
    
    public bool IsDead{get;private set;}
   
    public UnityEvent OnDie;
   
    public void Kill(){
        IsDead = true;
        OnDie.Invoke();
    }

    public void Wipe(){
        Destroy(gameObject);
    }
}
