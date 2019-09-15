using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
	public gameManager GM;
	public Transform player;
	
	[Header("Spawning Crawlers")]
	public bool spawnCrawlers = true;
	public GameObject crawler;
	public int spawnCount = 1;
	public float spawnFreq = 2f;
	private IEnumerator crawlerSpawner;

	bool started = false;
	int lastScore;
    
    void Start()
    {
		crawlerSpawner = crawlerSpawnerCoroutine();
		StartCoroutine(crawlerSpawner);
		lastScore = GM.score;

	}
	

    /*
    void Update()
    {
		if (lastScore != GM.score)
		{

			if ((GM.score % 10) == 0)
				spawnCount++;
			spawnFreq = Mathf.LerpUnclamped(1f, .99f, GM.score);
		}

		lastScore = GM.score;

        

		
    }
	*/

	IEnumerator crawlerSpawnerCoroutine()
	{
		started = true;
		while (spawnCrawlers)
		{
			if (spawnCount == 1)
			{ 
				GameObject clone = Instantiate(crawler, randomSpawningPosition(), Quaternion.identity);
				clone.GetComponent<crawler_Controller>().target = player;
			}
			else
			{
				for(int i =1; i <= spawnCount; i++)
				{
					GameObject clone = Instantiate(crawler, randomSpawningPosition(), Quaternion.identity);
					clone.GetComponent<crawler_Controller>().target = player;
					
				}
			}
			yield return new WaitForSeconds(spawnFreq);
			if (!spawnCrawlers)
				break;
		}

	}

	Vector3 randomSpawningPosition()
	{
		Vector3 P;
		do
		{
			P = new Vector3(Random.Range(-2.8f, 2.8f), Random.Range(-5f, 5f), 0);
		} while (Vector3.Distance(player.position, P) <= 1.5f);
		return P;
	}

}
