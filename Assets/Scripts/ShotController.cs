using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

    private float speed = 10f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector2(speed, 0) * Time.deltaTime);
        if(transform.position.x > 20)
        {
            GameObject.Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject.Destroy(gameObject);
    }
}

