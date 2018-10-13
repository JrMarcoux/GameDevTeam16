using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSphereColliderScr : MonoBehaviour {

    public float gravity = -18f;
    public float time;
    public string nameOfTargets;
    public Rigidbody projectile;
    public Vector3 velocity;
    public Vector3 positionInit;
    private List<GameObject> targets;

    // Use this for initialization
    void Start () {
        projectile = this.transform.parent.GetComponent<Rigidbody>();
        velocity = projectile.velocity;
        positionInit = this.transform.parent.position;
        targets = this.transform.parent.GetComponent<RangeAttack>().targets;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void test()
    {
        float vitesseSca = Mathf.Sqrt(Mathf.Pow(velocity.x, 2) + Mathf.Pow(velocity.y, 2));
        float angle = Mathf.Atan(velocity.y) - Mathf.Atan(velocity.x);
    }
}
