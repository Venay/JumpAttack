using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seeker_controller : MonoBehaviour
{
	public Transform player;
	public Rigidbody2D RB;

	public float speed = 1f;
	public float rotationSpeed = 200f;
    

    
    void Update()
    {
		
		Vector2 direction = (player.position - transform.position).normalized;
		RB.angularVelocity = -(Vector3.Cross(direction, transform.up).z) * rotationSpeed;
		RB.velocity = transform.up * speed;

    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if ((collision.tag == "Player") && (!collision.gameObject.GetComponent<playerController>().isMoving))
			collision.GetComponent<HealthSystem>().damage(1);
	}

	public void die()
	{
		Destroy(this.gameObject);
	}
	
}
