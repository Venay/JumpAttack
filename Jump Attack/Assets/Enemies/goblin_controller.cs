using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblin_controller : MonoBehaviour
{
    public GameObject player;
    //public gameManager GM;
    public ParticleSystem attackPS;
    public ParticleSystem chargingPS;
    public GameObject goblinBomb;
    public float speed = 1;
    public float attackDelay = 1;
    public float attackRange = 1;
    

    bool isAttacking = false;



    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem.MainModule main = chargingPS.main;
        main.startLifetime = attackDelay;
        main.startSize = attackRange * 2;
        main = attackPS.main;
        main.startSize = attackRange * 2;
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + new Vector3(-1, 1, 0) * .5f * Time.deltaTime;


        if (!isAttacking)
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        
        if ((Vector3.Distance(transform.position, player.transform.position) <= attackRange) && (!isAttacking))
        {
            isAttacking = true;
            attack(attackDelay);
        }

        
        
    }



    void attack(float delay)
    {
        chargingPS.Emit(1);
        StartCoroutine(attackWithDelay(delay));
        StartCoroutine(isAttackinSet(false, delay));
    }


    IEnumerator attackWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);


        GameObject clone = Instantiate(goblinBomb, transform.position, Quaternion.identity);
        clone.GetComponent<Goblin_Bomb>().bombSpeed = attackDelay;
        clone.GetComponent<Goblin_Bomb>().toSize = attackRange*2;



        //attackPS.Emit(1);
        


        /*
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Player"));
        if (players.Length != 0)
        {
            player.GetComponent<HealthSystem>().damage(1);
        }
        */

    }


    IEnumerator isAttackinSet(bool b, float delay)
    {
        yield return new WaitForSeconds(delay);

        isAttacking = b;

    }


    public void die()
    {
        Destroy(gameObject);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
