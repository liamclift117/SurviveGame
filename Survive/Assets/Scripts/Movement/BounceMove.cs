using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BounceMove : NetworkBehaviour
{
    [SyncVar]
    public Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);
    public SyncListString variables = new SyncListString();

    public float speed = 1.0f;

    public override void OnStartClient()
    {
        base.OnStartClient();
        SetVariables();
    }

    // Update is called once per frame
    void Update()
    {
        var x = direction.x * Time.deltaTime * speed;
        var y = direction.y * Time.deltaTime * speed;

        transform.Translate(x, y, y);
    }

    public void SetVariables()
    {
        speed = float.Parse(variables[1]);
    }
}