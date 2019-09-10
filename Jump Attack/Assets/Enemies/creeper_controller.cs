using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creeper_controller : MonoBehaviour
{
    public Transform target;
    public SpriteRenderer SR;
    public float speed;
    [Header("flash setting")]
    public AnimationCurve flashCurve;
    public float flashRange;
    public float flashDuration;
    public float flashRate;

    // Start is called before the first frame update
    void Start()
    {
        SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 0f);
        InvokeRepeating("flash", 0f, flashRate);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.up = target.position - transform.position;
        if (  Vector3.Distance  (transform.position , target.position)   <= flashRange)
        {
            SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 1-Vector3.Distance(transform.position, target.position)/flashRange);
            //Debug.Log(Vector3.Distance(transform.position, target.position));
        }
        else
        {
            SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 0f);
        }
        Debug.DrawLine(transform.position, transform.position + transform.up,Color.green);
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

    void flash()
    {
        if (Vector3.Distance(transform.position, target.position) > flashRange)
            LeanTween.color(this.gameObject, new Color(SR.color.r, SR.color.g, SR.color.b, 1f), flashDuration).setEase(flashCurve);
        
    }
}
