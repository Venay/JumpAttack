using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gameManager : MonoBehaviour
{
    public GameObject player;
    //public float clearTimer;
    //private void donotkill;

    public TextMeshProUGUI debugText;
    //private bool clearable = false;
    //private float clearCountDown = 10f;
    
    private Color BGcolor;
    //[SerializeField] private float deathTimer;
    

    [Header("Death")]
    public GameObject deathUI;

    [Header("Score")]
    public TextMeshProUGUI scoreText;
    public int score;
	public static int generalScore;

    [Header("Audio")]
    public AudioSource AS2;
    public AudioSource AS12;

    [Header("Enemy Spawner")]
    public bool spawn = true;
    public float spawnRange;
    public GameObject crawler;
    public GameObject creeper;
    public GameObject mine;
    public int spawnCount = 1;
    public float spawnRate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //clearCountDown = clearTimer;
        BGcolor = Camera.main.backgroundColor;
        //Debug.Log(new Vector3(-1, 1, 0) - Vector3.zero);
        Time.timeScale = 1;
        scoreText.text = score.ToString();
        if (spawn)
        {
            InvokeRepeating("crawler_spawn", 0f, spawnRate);
            //InvokeRepeating("creeper_spawn", 0f, spawnRate * 2);

        }
        
    }

    
    /*
    void FixedUpdate()
        clearCountDown -= Time.fixedDeltaTime;
        Camera.main.backgroundColor = Color.Lerp( Color.red, BGcolor, clearCountDown / clearTimer);
        /*
        if (clearCountDown <= 0)
            player.GetComponent<HealthSystem>().damage(1);

        if ((transform.childCount <= 0) && (clearable))
        {
            Debug.Log("clear");
            clearable = false;
            resetClear();
        }
        //debugText.text = (Mathf.Round(clearCountDown * 10f) / 10f).ToString();
		
    }
         */
    
    
    

    public void updateScore(int addedScore,int scoreMulti)
    {
        score += addedScore*scoreMulti;
        scoreText.text = score.ToString();
        //scoreText.text = clearCountDown.ToString() + " - " + Time.fixedDeltaTime.ToString();
        
        //scoreText.text = transform.childCount.ToString();

    }

	/*
    void crawler_spawn()
    {
        clearable = true;
        for (int i =0; i<spawnCount; i++)
        {
			//float randIt = Random.Range(0, 1);
			if (Random.Range(0, 100) >= 5)
			{ 
            GameObject clone = Instantiate(crawler, randomVector(), Quaternion.identity, transform);
            clone.GetComponent<crawler_Controller>().target = player.transform;
			}else
			{
				GameObject clone = Instantiate(mine, randomVector(), Quaternion.identity, transform);
				clone.GetComponent<mine_controller>().player = player;
			}
            
        }
    }

    public void resetClear()
    {
        playAudio(12);
        clearCountDown = clearTimer;
        clearable = false;
    }

    void creeper_spawn()
    {
        clearable = true;
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject clone = Instantiate(creeper, randomVector(), Quaternion.identity, transform);
            clone.GetComponent<creeper_controller>().target = player.transform;
        }
    }
	*/

    public void playerDead()
    {
        Time.timeScale = 0;
        deathUI.SetActive(true);
        //reloadScene();
        //Invoke("reloadScene", 2f);
    }

    public void playAudio(int audioIndex)
    {
        switch (audioIndex)
        {
            case 3:
                AS2.Play();
                break;
            case 12:
                AS12.Play();
                break;
        }
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    Vector3 randomVector()
    {
        Vector3 rando;
        do
            rando = new Vector3(Random.Range(-2.81f, 2.81f), Random.Range(-5f, 5f), 0f);
        while (Vector3.Distance(player.transform.position, rando) < player.GetComponent<playerController>().attackRange);
        return rando;
        //return new Vector3(Random.Range(-2.81f,2.81f), Random.Range(-5f,5f ), 0f);
    }

	public void pauseGame()
	{
		Time.timeScale = 0;
	}

	public void resumeGame()
	{
		Time.timeScale = 1;
	}



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnRange);
        //Gizmos.DrawWireSphere(transform.position, 1.5f);
    }

}
