using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crawler_Controller : MonoBehaviour
{
    public Transform target;
    public float speed;
	//public GameObject GM;
    //public GameObject scorePopUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.up = target.position - transform.position;
        Debug.DrawLine(transform.position, transform.position + transform.up);
        transform.Rotate(new Vector3(0, 0, 45f));
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if ((collision.tag == "Player") && (!collision.gameObject.GetComponent<playerController>().isMoving))
			collision.GetComponent<HealthSystem>().damage(1);
		else if ((collision.tag == "Player") && (!collision.gameObject.GetComponent<playerController>().isMoving))
			this.GetComponent<HealthSystem>().damage(1);

		
	}

    public void die()
    {
        //Instantiate(scorePopUp, transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }
}
