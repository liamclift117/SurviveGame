using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SawLauncherProjectile : NetworkBehaviour {

    //Common Attack Variables
    public int pierce = 3;
    public float damage = 10.0f;
    public float armorPierce = 5.0f;
    public string type = "phyiscal";
    public float knockback = 1.0f;
    public float speed = 1.0f;
    public Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        direction = gameObject.transform.parent.GetComponent<Aim>().direction;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (pierce < 0)
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.GetComponent<Health>() != null)
        {
            var hit = collision.gameObject;
            var health = hit.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage, armorPierce, knockback, gameObject.transform.position, type);
            }
            pierce = pierce - 1;
        }
        else
        {
            //Bounce off walls instead of terminating on them
         //   Vector3 normal = collision.gameObject.GetComponent<Normal>().normal;
         //   direction = -1 * (direction - 2 * Vector3.Dot(direction, normal) * normal);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var x = direction.x * Time.deltaTime * speed;
        var y = direction.y * Time.deltaTime * speed;

        transform.Translate(x, y, y);
    }
}
