using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime_controller : MonoBehaviour
{

    public GameObject player;
    public float speed;

    [Header("Shield")]
    public HealthSystem HP;
    public GameObject shield;
    public float shieldRotationSpeed;

    [Header("Detection Control")]
    public LineRenderer line;
    public GameObject slime;
    public float detectionRadius;
    public float detectionSpeed;
    public float slimeSpeed;

    bool isStuck = false;
    bool isDetected = false;
    float playerDefaultSpeed = .5f;
    

    /*
    void Start()
    {
        
        slime.transform.position = transform.position;
    }
    */
    

    // Update is called once per frame
    void Update()
    {
        //transform.up = player.transform.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if (shield != null)
            shield.transform.RotateAround(transform.position, Vector3.forward, shieldRotationSpeed * Time.deltaTime);

        if ((Vector3.Distance(transform.position, player.transform.position) <= detectionRadius) && (!isDetected))
        {
            
            isDetected = true;
            StartCoroutine(lunchSlime(slimeSpeed));
        }

        if (isStuck)
            slime.transform.position = player.transform.position;


        line.SetPosition(0, transform.position);
        line.SetPosition(1, slime.transform.position);
        
        if ((HP.health == 1) && (shield != null))
            Destroy(shield);
        
        

    }


    void detected()
    {
        
        
        isStuck = true;
        speed = detectionSpeed;
        playerDefaultSpeed = player.GetComponent<playerController>().jumpDuration;
        player.GetComponent<playerController>().jumpDuration *= 2.25f;
        
    }
    

    
    IEnumerator lunchSlime(float duration)
    {
        float t = 0;
        do
        {
            //Debug.Log(t/duration);

            if (t >= duration)
                detected();

            slime.transform.position = Vector3.Lerp(transform.position, player.transform.position, t/ duration);
            t += Time.deltaTime;


            yield return null;

        } while (t <= duration+Time.deltaTime);

            
    }
    


    

    public void die()
    {
        if (isStuck)
            player.GetComponent<playerController>().jumpDuration = playerDefaultSpeed;

        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

    }
}
