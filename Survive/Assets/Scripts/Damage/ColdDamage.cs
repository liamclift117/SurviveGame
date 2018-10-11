using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ColdDamage : Damage
{
    //Common Attack Variables
    public int pierce = 1;
    public float damage = 10.0f;
    public float armorPierce = 5.0f;
    public string type = "cold";
    public float knockback = 1.0f;

    //Cold variables
    public float coldSlow = 0.25f;
    public float coldChance = 0.80f;
    public float coldDuration = 6.0f;

    //Networking
    public SyncListString variables = new SyncListString();

    public override void OnStartClient()
    {
        base.OnStartClient();
        SetVariables();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if (pierce < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            pierce -= 0;
            var hit = collision.gameObject;
            var health = hit.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage, armorPierce, knockback, gameObject.transform.position, type);
            }

            //Cold
            if(Random.value <= coldChance)
            {
                //Add cold to hit object
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

        coldSlow = float.Parse(variables[4]);
        coldChance = float.Parse(variables[5]);
        coldDuration = float.Parse(variables[6]);
    }
}