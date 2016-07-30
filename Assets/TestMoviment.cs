using UnityEngine;
using System.Collections;

public class TestMoviment : MonoBehaviour {

    Vector3 move = new Vector3();
    float speed = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
            move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            this.transform.position += move * speed * Time.deltaTime;
    }
}
