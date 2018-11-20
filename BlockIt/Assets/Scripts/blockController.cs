using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockController : MonoBehaviour {

    public int fallSpeed;
    Rigidbody rb;
    public bool isGrounded;
    public Vector3 currentPosition;

	// Use this for initialization
	void Start () 
    {
        rb = gameObject.GetComponent<Rigidbody>();
        isGrounded = false;

        StartCoroutine(Fall());
	}
	
//    void FixedUpdate ()
//    {
//        if (isGrounded)
//        {
//            StopCoroutine(Fall());
//        }
//    }

	IEnumerator Fall ()
    {
        while (!isGrounded)
        {
            currentPosition = rb.position;


            rb.position = new Vector3(currentPosition.x , currentPosition.y - 1.0f, currentPosition.z);

            print(Time.time);
            yield return new WaitForSeconds(1);
            print(Time.time);
        }

        yield break;
	}

    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
    }
}
