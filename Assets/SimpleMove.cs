using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour {
    public Vector3 mov;
    public float speed = 2;


	void Update () {
        mov = new Vector3 (Input.GetAxis("XMove"), Input.GetAxis("YMove"), Input.GetAxis("ZMove"));
        transform.position += mov * Time.deltaTime * speed;
	}
}
