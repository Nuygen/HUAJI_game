using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector2(-20, 0) * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector2(-5, 0) * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector2(-10, 0) * Time.deltaTime);
        }

        if (gameObject.transform.position.x < -12)
        {
            GameObject.Destroy(gameObject);
        }
	}
}
