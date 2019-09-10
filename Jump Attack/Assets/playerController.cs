using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public gameManager GM;
    public bool isMoving = false;
    public float wind;

    [Header("General Components")]
    public HealthSystem healthSystem;
    

    [Header("Movment Controls")]
    public float jumpDuration = 0.3f;
    public AnimationCurve jumpCurve;
    public float stretchAndSquash = 2f;
    public AnimationCurve jumpStretching;

    [Header("Attack Controls")]
    public ParticleSystem explosion;
    public float attackRange = 1f;


    


   
    



    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
            //Debug.Log("isMoving = " + isMoving);
            transform.localScale = new Vector3(.2f, .2f, 1);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            transform.up = mousePos - transform.position;
            LeanTween.move(this.gameObject, mousePos, jumpDuration).setEase(jumpCurve).setOnComplete(attack);
            LeanTween.scaleX(this.gameObject, transform.localScale.x / stretchAndSquash, jumpDuration).setEase(jumpStretching).setOnComplete(isNotMoving);
            LeanTween.scaleY(this.gameObject, transform.localScale.x * (stretchAndSquash * 0.75f), jumpDuration).setEase(jumpStretching);
            
        }

        Debug.DrawLine(transform.position,transform.position+(transform.up*attackRange), Color.red, 0f);

        //transform.position += new Vector3(-1, 1, 0)*wind;
    }

    


    void attack()
    {
        
        explosion.Emit(1);
        GM.playAudio(3);
        Collider2D[] enemies =Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach(Collider2D enemy in enemies)
        {
            if (enemy.tag == "Enemy")
            {
                enemy.GetComponent<HealthSystem>().damage(1);
                GM.updateScore(1);
                
            }
                

        }

    }

    public void die()
    {
        GM.playerDead();
        
    }

    void isNotMoving()
    {
        isMoving = false;
        //Debug.Log("isMoving = " + isMoving);
    }

    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
    }
}
