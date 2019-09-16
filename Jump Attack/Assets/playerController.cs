using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public gameManager GM;
    [SerializeField] public GameObject scorePop;
    public bool isMoving = false;
	public Camera cam;
    //public float wind;

    [Header("General Components")]
    public HealthSystem healthSystem;
    

    [Header("Movment Controls")]
    public float jumpDuration = 0.3f;
    public AnimationCurve jumpCurve;
    public float stretchAndSquash = 2f;
    public AnimationCurve jumpStretching;

	[Header("Attack Controls")]
	public Animator animator;
    //public ParticleSystem explosion;
    public float attackRange = 1f;
    int playerMana = 0;


    


   
    



    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
			
			animator.SetTrigger("moving");
			
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
        int scoreSum = 0;
        //explosion.Emit(1);
		animator.SetTrigger("attack");
        GM.playAudio(3);

		
		

        Collider2D[] enemies =Physics2D.OverlapCircleAll(transform.position, attackRange,  LayerMask.GetMask("Enemies"));
		

		foreach (Collider2D enemy in enemies)
        {
			Debug.Log(Vector3.Dot(enemy.transform.position - transform.position, transform.up) + enemy.name);
			if ((Vector3.Dot(enemy.transform.position - transform.position, transform.up) >= 0) || ( Vector3.Distance(transform.position, enemy.transform.position) <= .5f))
			{

				enemy.GetComponent<HealthSystem>().damage(1);
				scoreSum += 1;
				GameObject clone =Instantiate(scorePop, enemy.transform.position, Quaternion.identity);
				clone.GetComponent<scorePopup>().textColor = enemy.GetComponent<SpriteRenderer>().color;
				clone.GetComponent<scorePopup>().score = 1*enemies.Length;
			}

        }


        GM.updateScore(scoreSum,enemies.Length);
        playerMana += scoreSum * enemies.Length;
        //GM.checkClear();

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
