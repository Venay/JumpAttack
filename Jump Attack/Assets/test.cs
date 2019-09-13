using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    //public Transform thisOne;
    public AnimationCurve anim;
    public float animationSeconds = 5f;
    public float scale = 1f;
    private float X;
    private float O = 0;
    private Vector3 P;
    // Start is called before the first frame update
    void Start()
    {
        P = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward; 
             
        if (Input.anyKey)
        {
            StartCoroutine(shakeIt());
        }
        //P = transform.position;
        P.y = X;
        transform.position = P;

        
    }

    IEnumerator shakeIt()
    {
        int shakeTimer = 0;
        while (shakeTimer < animationSeconds*30)
        {
            shakeTimer++;
            //scale *= anim.Evaluate(shakeTimer / (animationSeconds * 30));
            //X = Mathf.PerlinNoise(Time.time*(scale* anim.Evaluate(shakeTimer / (animationSeconds * 30))), Time.time* (scale * anim.Evaluate(shakeTimer / (animationSeconds * 30))));
            X = Random.Range(-1, 1) + O;
            //X = Mathf.Lerp(1, -1, X);
            X *= anim.Evaluate(shakeTimer / (animationSeconds * 30));
            Debug.Log(X);
            O = X;
            yield return null;
            
            
        }
    }
}
