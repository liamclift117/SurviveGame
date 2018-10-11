using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
    public float maxHealth = 100.0f;
    public float armor = 5.0f;
    public float resistane = 4.0f;
    public float regen = 0.5f;
    public float knockbackResist = 10.0f;
    [SyncVar(hook = "OnChangeHealth")]
    public float currentHealth = 100.0f;
    public RectTransform healthBar;
    public Queue<Vector3> knocks = new Queue<Vector3>();
    public Queue<int> knockDone = new Queue<int>();
    public int nextKnock = 0;
    public float aggro = 19.0f;

    void Start()
    {
    }

    void Update()
    {
        int temp = knocks.Count;
        for(int i = 0; i<temp; i++)
        {
            Vector3 tempVec = knocks.Dequeue();
            gameObject.transform.Translate(tempVec);
            int knocksLeft = knockDone.Dequeue();
            knocksLeft--;
            if (knocksLeft > 0)
            {
                knocks.Enqueue(tempVec);
                knockDone.Enqueue(knocksLeft);
            }
        }
        if (currentHealth >= maxHealth)
        {
            return;
        }
        currentHealth += regen;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    void OnCollision2DEnter(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
    }

    public void TakeDamage(float amount, float pierce, float knockback, Vector3 origin, string type)
    {
        Debug.Log("Taking damage " + gameObject + "   " + amount + "   " + origin + "   ");
        if (!isServer)
        {
            return;
        }

        //Calculate the reduction of damage based on armor/resistance
        float temp = 0.0f;
        if(type.Equals("physical") || type.Equals("bleed"))
        {
            temp = armor - pierce;
            if (temp <= 0.0)
            {
                temp = 0.0f;
            }
            amount -= temp;
        }
        else if (type.Equals("sound"))
        {
            //Do nothing special because sound pierces armor and resistance
        }
        else
        {
            temp = resistane - pierce;
            if (temp <= 0.0)
            {
                temp = 0.0f;
            }
            amount -= temp;
        }

        float distance = knockback / knockbackResist;
        Vector3 direction = gameObject.transform.position - origin;
        direction.Normalize();
        int splitKnock = 3;
        direction = direction* distance;
        knocks.Enqueue(direction / splitKnock);
        knockDone.Enqueue(splitKnock);

        //Take down the health by the proper amount
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Dead!");
        }
    }

    //When the health variable changes move the health bar
    void OnChangeHealth(float currentHealth)
    {
        healthBar.sizeDelta = new Vector2(
            currentHealth,
            healthBar.sizeDelta.y);
    }
}
