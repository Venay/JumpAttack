using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Bomb : MonoBehaviour
{
    public float bombSpeed = 1;
    public float toSize = 1;
    public Color toColor;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, bombSpeed);
        LeanTween.scale(gameObject, new Vector3(toSize, toSize, 0), bombSpeed/20).setEaseOutCirc();
        LeanTween.color(gameObject, toColor, bombSpeed-bombSpeed/20).setDelay(bombSpeed/20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void die()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player") )
            collision.GetComponent<HealthSystem>().damage(1);




    }
}
