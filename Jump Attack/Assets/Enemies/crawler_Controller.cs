using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crawler_Controller : MonoBehaviour
{
    public Transform target;
    public float speed;
    float defaultSpeed;
    public float minSpeed;
    public Color defaultColor;
    public Color fadeColor;
    //public GameObject GM;
    //public GameObject scorePopUp;
    // Start is called before the first frame update


    

    void Start()
    {
        speed = Random.Range(minSpeed, speed);
        defaultSpeed = speed;
        transform.Rotate(new Vector3(0, 0, 45f));
        
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position , speed * Time.deltaTime);
        transform.up = target.position - transform.position;
        if (target.GetComponent<playerController>().isMoving)
        {
            GetComponent<SpriteRenderer>().color = defaultColor;
            speed = defaultSpeed*2;
        }
        else
        {
            
            GetComponent<SpriteRenderer>().color = fadeColor;
            speed = minSpeed/2;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if ((collision.tag == "Player") && (!collision.gameObject.GetComponent<playerController>().isMoving))
			collision.GetComponent<HealthSystem>().damage(1);
		


		
	}

    public void die()
    {
        //Instantiate(scorePopUp, transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }
}
