
using UnityEngine;

public class mine_controller : MonoBehaviour
{
	public GameObject player;
	public float speed;
	SpriteRenderer SR;
	//public GameObject gm;
	 HealthSystem HP;
    // Start is called before the first frame update
    void Start()
    {
		SR = GetComponent<SpriteRenderer>();
		HP = player.GetComponent<HealthSystem>();
		//SR = gm.GetComponent<SpriteRenderer>();
		Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
		SR.color = Color.Lerp(Color.red, Color.black, Mathf.Sin(Time.time*15f));
		//LeanTween.color(this.gameObject, Color.black, Time.deltaTime).setLoopPingPong();
		//LeanTween.value(gm, SR.color, new Color(SR.color.r, SR.color.g, SR.color.b, 0), 0.5f).setEaseInCubic().setLoopPingPong();
		transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

	public void die()
	{
		HP.damage(1);
		Destroy(this.gameObject);
	}
}
