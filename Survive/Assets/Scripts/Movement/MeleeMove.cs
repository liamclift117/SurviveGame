using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MeleeMove : NetworkBehaviour
{
    [SyncVar]
    public Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);
    public SyncListString variables = new SyncListString();

    public float edgeDistance = 0.15f;
    public float yToOrigin = -0.60f;
    public float xToOrign = 0.0f;

    public override void OnStartClient()
    {
        base.OnStartClient();
        SetVariables();
    }

    // Update is called once per frame
    void Update()
    {
        var x = direction.x * edgeDistance + gameObject.transform.parent.transform.position.x + xToOrign;
        var y = direction.y * edgeDistance + gameObject.transform.parent.transform.position.y + yToOrigin;

        transform.Translate(x, y, y);
    }

    public void SetVariables()
    {
        edgeDistance = float.Parse(variables[1]);
    }
}