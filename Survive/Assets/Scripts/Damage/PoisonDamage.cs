using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PoisonDamage : Damage
{
    //Common Attack Variables
    public int pierce = 1;
    public float damage = 10.0f;
    public float armorPierce = 5.0f;
    public string type = "poison";
    public float knockback = 1.0f;

    //Poison Specific Variables
    public float poisonDamage = 1.0f;
    public float poisonDuration = 10.0f;
    public float poisonChance = 0.5f;

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
            
            //Poison
            if(Random.value <= poisonChance)
            {
                //Add the poison script to the hit object
                //hit.AddComponent
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

        poisonDamage = float.Parse(variables[4]);
        poisonDuration = float.Parse(variables[5]);
        poisonChance = float.Parse(variables[6]);
    }
}