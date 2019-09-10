using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gameManager : MonoBehaviour
{
    public GameObject player;

    [Header("Death")]
    public GameObject deathUI;

    [Header("Score")]
    public TextMeshProUGUI scoreText;
    public int score;

    [Header("Audio")]
    public AudioSource AS2;

    [Header("Enemy Spawner")]
    public bool spawn = true;
    public GameObject crawler;
    public GameObject creeper;
    public int spawnCount = 1;
    public float spawnRate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(new Vector3(-1, 1, 0) - Vector3.zero);
        Time.timeScale = 1;
        scoreText.text = score.ToString();
        if (spawn)
        {
            InvokeRepeating("crawler_spawn", 0f, spawnRate);
            //InvokeRepeating("creeper_spawn", 0f, spawnRate * 2);

        }
        
    }

    /*e
    void Update()
    {
        
    }
    */

    public void updateScore(int addedScore)
    {
        score += addedScore;
        scoreText.text = score.ToString();
    }

    void crawler_spawn()
    {
        for (int i =0; i<spawnCount; i++)
        {
            GameObject clone =  Instantiate(crawler, randomVector(), Quaternion.identity);
            clone.GetComponent<crawler_Controller>().target = player.transform;
        }
    }

    void creeper_spawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject clone = Instantiate(creeper, randomVector(), Quaternion.identity);
            clone.GetComponent<creeper_controller>().target = player.transform;
        }
    }

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
        }
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    Vector3 randomVector()
    {
        return new Vector3(Random.Range(-2.81f,2.81f), Random.Range(-5f,5f ), 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(2.81f*2f, 5f*2, 1f));
    }

}
