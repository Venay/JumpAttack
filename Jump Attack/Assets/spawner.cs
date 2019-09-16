using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
	public gameManager GM;
	public GameObject player;
	
	[Header("Spawning Crawlers")]
	public bool spawnCrawlers = true;
	public GameObject crawler;
	public int spawnCount = 1;
	public float spawnFreq = 2f;
	private IEnumerator crawlerSpawner;

    [Header("Spawning Slime")]
    public bool spawnSlimes = true;
    public GameObject slime;
    public int slimeSpawnCount = 1;
    public float slimeSpawnFreq = 10f;


    [Header("Spawning Seekers")]
    public bool spawnSeekers = true;
    public GameObject seeker;
    public int seekerSpawnCount = 1;
    public float seekerSpawnFreq = 15f;


	//bool started = false;
	int lastScore;
    
    void Start()
    {
		crawlerSpawner = crawlerSpawnerCoroutine();
		StartCoroutine(crawlerSpawner);
		StartCoroutine(slimeSpawnerCoroutine());
        StartCoroutine(seekerSpawnerCoroutine());
		lastScore = GM.score;

	}
	

        /*╕
    void Update()
    {
        lastScore = GM.score;

        if (lastScore < 5)
            spawnCount = 2;
        else if (inRange(lastScore, 5, 50))
            spawnFreq = .5f;
        else if (inRange(lastScore, 51, 250))
            spawnFreq = 0.2f;
        
            






    }
            */

	IEnumerator crawlerSpawnerCoroutine()
	{
		//started = true;
		while (spawnCrawlers)
		{
			if (spawnCount == 1)
			{ 
				GameObject clone = Instantiate(crawler, randomSpawningPosition(), Quaternion.identity, transform);
				clone.GetComponent<crawler_Controller>().target = player.transform;
			}
			else
			{
				for(int i =1; i <= spawnCount; i++)
				{
					GameObject clone = Instantiate(crawler, randomSpawningPosition(), Quaternion.identity, transform);
					clone.GetComponent<crawler_Controller>().target = player.transform;
					
				}
			}
			yield return new WaitForSeconds(spawnFreq);
			if (!spawnCrawlers)
				break;
		}

	}





    IEnumerator slimeSpawnerCoroutine()
    {
        //started = true;
        while (spawnSlimes)
        {
            yield return new WaitForSeconds(slimeSpawnFreq);


            if (slimeSpawnCount == 1)
            {
                GameObject clone = Instantiate(slime, randomSpawningPosition(), Quaternion.identity, transform);
                clone.GetComponent<slime_controller>().player = player;
            }
            else
            {
                for (int i = 1; i <= slimeSpawnCount; i++)
                {
                    GameObject clone = Instantiate(slime, randomSpawningPosition(), Quaternion.identity, transform);
                    clone.GetComponent<slime_controller>().player = player;

                }
            }
            if (!spawnSlimes)
                break;
        }

    }



    IEnumerator seekerSpawnerCoroutine()
    {
        //started = true;
        while (spawnSeekers)
        {
            yield return new WaitForSeconds(seekerSpawnFreq);


            if (seekerSpawnCount == 1)
            {
                if ((Random.value > .5f))
                {
                    GameObject clone = Instantiate(seeker, randomSpawningPosition(), Quaternion.identity, transform);
                    clone.GetComponent<seeker_controller>().player = player.transform;
                    GameObject clone2 = Instantiate(seeker, randomSpawningPosition(), Quaternion.identity, transform);
                    clone2.GetComponent<seeker_controller>().player = player.transform;

                }
                else
                {
                    GameObject clone = Instantiate(seeker, randomSpawningPosition(), Quaternion.identity, transform);
                    clone.GetComponent<seeker_controller>().player = player.transform;
                }
            }
            else
            {
                for (int i = 1; i <= seekerSpawnCount; i++)
                {
                    GameObject clone = Instantiate(seeker, randomSpawningPosition(), Quaternion.identity, transform);
                    clone.GetComponent<seeker_controller>().player = player.transform;

                }
            }
            if (!spawnSeekers)
                break;
        }

    }




    bool inRange (int x, int min, int max)
    {
        if ((x <= max) && (x >= min))
            return true;
        else
            return false;
    }

	Vector3 randomSpawningPosition()
	{
		Vector3 P;
        int lane = Random.Range(1, 4);
        do
		{
            


            switch (lane)
            {
                case 1:
                    P = new Vector3(2.82f, Random.Range(-5f, 5f), 0);
                    break;

                case 2:
                    P = new Vector3(-2.82f, Random.Range(-5f, 5f), 0);
                    break;

                case 3:
                    P = new Vector3(Random.Range(-2.8f, 2.8f), 5.1f, 0);
                    break;

                case 4:
                    P = new Vector3(Random.Range(-2.8f, 2.8f), 5.1f, 0);
                    break;

                default:
                    P = new Vector3(2.82f, 5.1f, 0);
                    break;


            }


			//P = new Vector3(Random.Range(-2.8f, 2.8f), Random.Range(-5f, 5f), 0);
		} while (Vector3.Distance(player.transform.position, P) <= 1.5f);
		return P;
	}

}
