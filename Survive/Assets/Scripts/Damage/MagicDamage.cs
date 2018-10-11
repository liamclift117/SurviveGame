using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MagicDamage : Damage
{
    //Common Attack Variables
    public int pierce = 1;
    public float damage = 10.0f;
    public float armorPierce = 5.0f;
    public string type = "magic";
    public float knockback = 1.0f;

    //Networking
    public SyncListString variables = new SyncListString();

    public override void OnStartClient()
    {
        base.OnStartClient();
        SetVariables();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (pierce < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            var hit = collision.gameObject;
            var health = hit.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage, armorPierce, knockback, gameObject.transform.position, type);
            }
        }
    }

    public void SetVariables()
    {
        pierce = int.Parse(variables[0]);
        damage = float.Parse(variables[1]);
        armorPierce = float.Parse(variables[2]);
        type = variables[3];
        knockback = float.Parse(variables[7]);
    }
}