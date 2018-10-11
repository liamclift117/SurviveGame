using UnityEngine;
using UnityEngine.Networking;

public class Movement : NetworkBehaviour
{
    public float speed = 3.0f;
    //private float lastX = 0.0f;
    //private float lastY = 0.0f;
	// Update is called once per frame
	void Update () {
        //If this isn't the local player, don't give any control
        if(!isLocalPlayer)
        {
            return;
        }
        //lastX = gameObject.transform.position.x;
        //lastY = gameObject.transform.position.y;
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Translate(x, y, y);
	}

}
