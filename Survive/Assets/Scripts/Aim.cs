using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPos = Camera.main.ScreenToWorldPoint((new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z - transform.position.z))));
        newPos.z = transform.position.z;
        direction = newPos - gameObject.transform.position;
        direction.Normalize();
        Debug.DrawLine(gameObject.transform.position, direction, Color.red);
	}
}
