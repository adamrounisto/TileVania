using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] float MoveSpeed=1f;


    Rigidbody2D myRigidbody;
    bool isFacingRight;

	// Use this for initialization


	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            myRigidbody.velocity = new Vector2(MoveSpeed, 0f);
        }
        else
        {
            myRigidbody.velocity = new Vector2(-MoveSpeed, 0f);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }

     bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }
}
