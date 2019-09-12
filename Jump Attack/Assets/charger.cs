using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charger : MonoBehaviour
{
    public Transform target;
    public float speed = 0.5f;
    public float detectionRadius = 2f;
    public float chargingDuration = 2f;

    private bool isCharging = false;
    private Vector3 targetPosition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
            

        if (isCharging == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target.position) <= detectionRadius)
            {
                isCharging = true;
                targetPosition = target.position;
            }
        }
        else
        {
            charge();
        }

    }

    void charge()
    {
        if (isCharging)
        {
            
            //LeanTween.scale(this.gameObject, transform.localScale * 0.5f, 1f);
            StartCoroutine(chargeAfterTime());
            if (transform.position == targetPosition)
                isCharging = false;
        }
        
        
    }

    IEnumerator chargeAfterTime()
    {
        while (transform.position != targetPosition) { 
            Debug.Log(isCharging);
            yield return new WaitForSeconds(1f);
            LeanTween.move(this.gameObject, targetPosition, chargingDuration).setOnComplete(resetTarget);
        }


    }

    void resetTarget()
    {
        isCharging = false;
        transform.localScale = new Vector3(.2f, .2f, 0);
        //Debug.Log(isCharging);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
