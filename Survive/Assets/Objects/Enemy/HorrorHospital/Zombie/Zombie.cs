using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Zombie : NetworkBehaviour
{
    public float turnSpeed = 1.0f;
    public float moveSpeed = 3.0f;
    public float bloodlust = 1.0f;
    public float priority = 1.0f;
    public int attentionSpan = 5;
    public int attention;
    public GameObject target;
    public Vector3 lastDirection = new Vector3(0.0f, 0.0f, 0.0f);
    public float range = 15.0f;
    public float attackDelay = 1.0f;
    public float attackedAt = 0.0f;

    public GameObject attackPrefab;

    //Common Attack Variables
    public int pierce = 1;
    public float damage = 5.0f;
    public float armorPierce = 5.0f;
    public string type = "physical";
    public float knockback = 5.0f;

    //Cold variables
    public float extra1 = 0.25f;
    public float extra2 = 0.80f;
    public float extra3 = 6.0f;

    public string[] attackVariables = { "", "", "", "", "", "", "", "" };

    public Transform attackSpawn;
    public float attackLife = 0.3f;
    public Vector3 dir;

    //Movement variables
    public float speed = 1.0f;
    public string moveType = "straight";
    public float arc = 0.0f;

    public string[] moveVariables = { "", "", "" };
    public GameObject[] leftLegs;
    public GameObject[] rightLegs;
    public GameObject[] bodies;
    public GameObject[] leftArms;
    public GameObject[] rightArms;
    public GameObject[] heads;
    public GameObject leftLeg;
    public GameObject rightLeg;
    public GameObject body;
    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject head;

    //Zombie specific
    public int leg;
    public bool attacking;
    public int walkTime;
    // Use this for initialization
    void Start()
    {
        int randNum = Random.Range(0, leftLegs.Length);
        leftLeg = Instantiate(leftLegs[randNum], gameObject.transform);
        randNum = Random.Range(0, rightLegs.Length);
        rightLeg = Instantiate(rightLegs[randNum], gameObject.transform);
        randNum = Random.Range(0, bodies.Length);
        body = Instantiate(bodies[randNum], gameObject.transform);
        randNum = Random.Range(0, leftArms.Length);
        leftArm = Instantiate(leftArms[randNum], gameObject.transform);
        randNum = Random.Range(0, rightArms.Length);
        rightArm = Instantiate(rightArms[randNum], gameObject.transform);
        randNum = Random.Range(0, heads.Length);
        head = Instantiate(heads[randNum], gameObject.transform);
        leg = Random.Range(0, 2);
        attacking = false;
        walkTime = 20;

        attention = attentionSpan;
        target = FindTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
        {
            return;
        }
        attention--;
        if (attention < 0)
        {
            attention = attentionSpan;
            target = FindTarget();
        }

        //Zombie stuttery walk
        if (walkTime < 12)
        {
            MoveTowardsTarget();
        }
        else
        {
            if (leg == 0)
            {
                leftLeg.GetComponent<Animator>().SetFloat("Speed", 0.0f);
            }
            else
            {
                rightLeg.GetComponent<Animator>().SetFloat("Speed", 0.0f);
            }
        }
        if (walkTime < 1)
        {
            walkTime = 30;
        }
        walkTime--;

        if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= range)
        {
            //Debug.Log(attackedAt + "    " + attackDelay + "    " + Time.time);
            if (attackedAt + attackDelay < Time.time)
            {
                attackVariables[0] = "" + pierce;
                attackVariables[1] = "" + damage;
                attackVariables[2] = "" + armorPierce;
                attackVariables[3] = "" + type;
                attackVariables[4] = "" + extra1;
                attackVariables[5] = "" + extra2;
                attackVariables[6] = "" + extra3;
                attackVariables[7] = "" + knockback;
                moveVariables[0] = "" + moveType;
                moveVariables[1] = "" + speed;
                moveVariables[2] = "" + arc;
                dir = target.transform.position - gameObject.transform.position;
                dir.Normalize();
                attackedAt = Time.time;
                StartCoroutine(AttackTarget(dir, attackVariables, moveVariables));
            }
        }
    }

    GameObject FindTarget()
    {
        GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag("Player");
        //Debug.Log(potentialTargets.Length);
        GameObject bestTarget = potentialTargets[0];
        for (int i = 0; i < potentialTargets.Length; i++)
        {
            float curDist = Vector3.Distance(bestTarget.transform.position, gameObject.transform.position);
            float newDist = Vector3.Distance(potentialTargets[i].transform.position, gameObject.transform.position);
            float curAggro = 0.0f;
            if (bestTarget.GetComponent<PlayerHealth>() != null)
            {
                curAggro = bestTarget.GetComponent<PlayerHealth>().aggro;
            }
            else
            {
                curAggro = bestTarget.GetComponent<Health>().aggro;
            }
            float newAggro = 0.0f;
            if (potentialTargets[i].GetComponent<PlayerHealth>() != null)
            {
                newAggro = potentialTargets[i].GetComponent<PlayerHealth>().aggro;
            }
            else
            {
                newAggro = potentialTargets[i].GetComponent<Health>().aggro;
            }
            if (curAggro * priority - curDist * bloodlust <= newAggro * priority - newDist * bloodlust)
            {
                bestTarget = potentialTargets[i];
            }
        }
        //Debug.Log(bestTarget.transform);
        return bestTarget;
    }

    public void MoveTowardsTarget()
    {
        //Debug.Log("Moving towards target");
        Vector3 direction = target.transform.position - gameObject.transform.position;
        direction.Normalize();
        if(direction.x > 0.0)
        {
            leftLeg.GetComponent<SpriteRenderer>().flipX = false;
            rightLeg.GetComponent<SpriteRenderer>().flipX = false;
            body.GetComponent<SpriteRenderer>().flipX = false;
            leftArm.GetComponent<SpriteRenderer>().flipX = false;
            rightArm.GetComponent<SpriteRenderer>().flipX = false;
            head.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            leftLeg.GetComponent<SpriteRenderer>().flipX = true;
            rightLeg.GetComponent<SpriteRenderer>().flipX = true;
            body.GetComponent<SpriteRenderer>().flipX = true;
            leftArm.GetComponent<SpriteRenderer>().flipX = true;
            rightArm.GetComponent<SpriteRenderer>().flipX = true;
            head.GetComponent<SpriteRenderer>().flipX = true;
        }
        var x = 0.0f;
        var y = 0.0f;
        if (attacking)
        {
            x = direction.x * Time.deltaTime * moveSpeed/2.0f;
            y = direction.y * Time.deltaTime * moveSpeed/2.0f;
            if (leg == 0)
            {
                leftLeg.GetComponent<Animator>().SetFloat("Speed", moveSpeed * 0.6f);
            }
            else
            {
                rightLeg.GetComponent<Animator>().SetFloat("Speed", moveSpeed * 0.6f);
            }
        }
        else
        {
            x = direction.x * Time.deltaTime * moveSpeed;
            y = direction.y * Time.deltaTime * moveSpeed;
            if (leg == 0)
            {
                leftLeg.GetComponent<Animator>().SetFloat("Speed", moveSpeed * 1.2f);
            }
            else
            {
                rightLeg.GetComponent<Animator>().SetFloat("Speed", moveSpeed * 1.2f);
            }
        }
        //if (lastDirection.x == 0.0f && lastDirection.y == 0.0f)
        //{
        //lastDirection = direction;
        //    transform.Translate(x, y, y);
        //    return;
        //}
        //Debug.Log("About to try Math");
        //float tempAngle = Mathf.Acos(lastDirection.x * direction.x + lastDirection.y * direction.y);
        //Debug.Log("Math Done");
        //while (tempAngle > turnSpeed)
        //{
        //    Debug.Log("Finding Angle");
        //    Debug.Log("Calculating direction for enemy "+direction.x+"  "+direction.y+"     "+lastDirection.x+"  "+lastDirection.y);
        //    direction = direction + (lastDirection - direction) * 0.1f;
        //    direction.Normalize();
        //    tempAngle = Mathf.Acos(lastDirection.x * direction.x + lastDirection.y * direction.y);
        //    Debug.Log(tempAngle);
        //}
        //Debug.Log("Found good direction");
        //lastDirection = direction;
        //x = direction.x * Time.deltaTime * speed;
        //y = direction.y * Time.deltaTime * speed;
        //Debug.Log("Actually Move");
        transform.Translate(x, y, y);
        //Debug.Log("Done Moving");
    }

    public IEnumerator AttackTarget(Vector3 dir, string[] aVar, string[] mVar)
    {
        //Start playing animation
        attacking = true;
        rightArm.GetComponent<Animator>().SetTrigger("Attack");
        leftArm.GetComponent<Animator>().SetTrigger("Attack");

        //Delay the attack to allow for dodging
        yield return new WaitForSeconds(0.5f);

        var attack = (GameObject)Instantiate(
            attackPrefab,
            attackSpawn.position,
            attackSpawn.rotation);

        attack.transform.parent = gameObject.transform;
        //Debug.Log(attack.ToString());

        //Attack script adding
        //Debug.Log(attack.GetComponent<PhysicalDamage>().type);
        //Debug.Log(attack.GetComponent<PhysicalDamage>().variables.ToString());
        attack.GetComponent<PhysicalDamage>().variables.Add(aVar[0]);
        attack.GetComponent<PhysicalDamage>().variables.Add(aVar[1]);
        attack.GetComponent<PhysicalDamage>().variables.Add(aVar[2]);
        attack.GetComponent<PhysicalDamage>().variables.Add(aVar[3]);
        attack.GetComponent<PhysicalDamage>().variables.Add(aVar[4]);
        attack.GetComponent<PhysicalDamage>().variables.Add(aVar[5]);
        attack.GetComponent<PhysicalDamage>().variables.Add(aVar[6]);
        attack.GetComponent<PhysicalDamage>().variables.Add(aVar[7]);
        
        //Debug.Log(attack.GetComponent<MeleeMove>().variables.ToString());
        attack.GetComponent<MeleeMove>().variables.Add(mVar[0]);
        attack.GetComponent<MeleeMove>().variables.Add(mVar[1]);
        attack.GetComponent<MeleeMove>().variables.Add(mVar[2]);
        attack.GetComponent<MeleeMove>().direction = dir;

        Debug.Log("Attempting to spawn on server");
        NetworkServer.Spawn(attack);

        Debug.Log("Setting Object life");
        Destroy(attack, attackLife);
        attacking = false;
    }
}