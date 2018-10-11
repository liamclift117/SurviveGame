using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Attack : NetworkBehaviour
{
    public GameObject attackPrefab;

    //Common Attack Variables
    public int pierce = 1;
    public float damage = 10.0f;
    public float armorPierce = 5.0f;
    public string type = "cold";
    public float knockback = 5.0f;

    //Cold variables
    public float extra1 = 0.25f;
    public float extra2 = 0.80f;
    public float extra3 = 6.0f;

    public string[] attackVariables = {"","","","","","","",""};

    public Transform attackSpawn;
    public float attackLife = 2.0f;
    public Vector3 direction;

    //Movement variables
    public float speed = 1.0f;
    public string moveType = "straight";
    public float arc = 0.0f;

    public string[] moveVariables = {"","",""};

	// Use this for initialization
	void Start () {
        Aim aim = gameObject.GetComponent(typeof(Aim)) as Aim;
        direction = aim.direction;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
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
            direction = gameObject.GetComponent<Aim>().direction;
            CmdFire(direction, attackVariables, moveVariables);
        }
	}

    [Command]
    void CmdFire(Vector3 dir, string[] aVar, string[] mVar)
    {
        Debug.Log("In Command Fire");
        var attack = (GameObject)Instantiate(
            attackPrefab,
            attackSpawn.position,
            attackSpawn.rotation);
        Debug.Log(attack.ToString());

        //Attack script adding
        if (type.Equals("physical"))
        {
            Debug.Log(attack.GetComponent<PhysicalDamage>().type);
            Debug.Log(attack.GetComponent<PhysicalDamage>().variables.ToString());
            attack.GetComponent<PhysicalDamage>().variables.Add(aVar[0]);
            attack.GetComponent<PhysicalDamage>().variables.Add(aVar[1]);
            attack.GetComponent<PhysicalDamage>().variables.Add(aVar[2]);
            attack.GetComponent<PhysicalDamage>().variables.Add(aVar[3]);
            attack.GetComponent<PhysicalDamage>().variables.Add(aVar[4]);
            attack.GetComponent<PhysicalDamage>().variables.Add(aVar[5]);
            attack.GetComponent<PhysicalDamage>().variables.Add(aVar[6]);
            attack.GetComponent<PhysicalDamage>().variables.Add(aVar[7]);
        }
        else if (type.Equals("fire"))
        {
            Debug.Log(attack.GetComponent<FireDamage>().type);
            Debug.Log(attack.GetComponent<FireDamage>().variables.ToString());
            attack.GetComponent<FireDamage>().variables.Add(aVar[0]);
            attack.GetComponent<FireDamage>().variables.Add(aVar[1]);
            attack.GetComponent<FireDamage>().variables.Add(aVar[2]);
            attack.GetComponent<FireDamage>().variables.Add(aVar[3]);
            attack.GetComponent<FireDamage>().variables.Add(aVar[4]);
            attack.GetComponent<FireDamage>().variables.Add(aVar[5]);
            attack.GetComponent<FireDamage>().variables.Add(aVar[6]);
            attack.GetComponent<FireDamage>().variables.Add(aVar[7]);
        }
        else if (type.Equals("electric"))
        {
            Debug.Log(attack.GetComponent<ElectricDamage>().type);
            Debug.Log(attack.GetComponent<ElectricDamage>().variables.ToString());
            attack.GetComponent<ElectricDamage>().variables.Add(aVar[0]);
            attack.GetComponent<ElectricDamage>().variables.Add(aVar[1]);
            attack.GetComponent<ElectricDamage>().variables.Add(aVar[2]);
            attack.GetComponent<ElectricDamage>().variables.Add(aVar[3]);
            attack.GetComponent<ElectricDamage>().variables.Add(aVar[4]);
            attack.GetComponent<ElectricDamage>().variables.Add(aVar[5]);
            attack.GetComponent<ElectricDamage>().variables.Add(aVar[6]);
            attack.GetComponent<ElectricDamage>().variables.Add(aVar[7]);
        }
        else if (type.Equals("poison"))
        {
            Debug.Log(attack.GetComponent<PoisonDamage>().type);
            Debug.Log(attack.GetComponent<PoisonDamage>().variables.ToString());
            attack.GetComponent<PoisonDamage>().variables.Add(aVar[0]);
            attack.GetComponent<PoisonDamage>().variables.Add(aVar[1]);
            attack.GetComponent<PoisonDamage>().variables.Add(aVar[2]);
            attack.GetComponent<PoisonDamage>().variables.Add(aVar[3]);
            attack.GetComponent<PoisonDamage>().variables.Add(aVar[4]);
            attack.GetComponent<PoisonDamage>().variables.Add(aVar[5]);
            attack.GetComponent<PoisonDamage>().variables.Add(aVar[6]);
            attack.GetComponent<PoisonDamage>().variables.Add(aVar[7]);
        }
        else if (type.Equals("sound"))
        {
            Debug.Log(attack.GetComponent<SoundDamage>().type);
            Debug.Log(attack.GetComponent<SoundDamage>().variables.ToString());
            attack.GetComponent<SoundDamage>().variables.Add(aVar[0]);
            attack.GetComponent<SoundDamage>().variables.Add(aVar[1]);
            attack.GetComponent<SoundDamage>().variables.Add(aVar[2]);
            attack.GetComponent<SoundDamage>().variables.Add(aVar[3]);
            attack.GetComponent<SoundDamage>().variables.Add(aVar[4]);
            attack.GetComponent<SoundDamage>().variables.Add(aVar[5]);
            attack.GetComponent<SoundDamage>().variables.Add(aVar[6]);
            attack.GetComponent<SoundDamage>().variables.Add(aVar[7]);
        }
        else if (type.Equals("cold"))
        {
            Debug.Log(attack.GetComponent<ColdDamage>().type);
            Debug.Log(attack.GetComponent<ColdDamage>().variables.ToString());
            attack.GetComponent<ColdDamage>().variables.Add(aVar[0]);
            attack.GetComponent<ColdDamage>().variables.Add(aVar[1]);
            attack.GetComponent<ColdDamage>().variables.Add(aVar[2]);
            attack.GetComponent<ColdDamage>().variables.Add(aVar[3]);
            attack.GetComponent<ColdDamage>().variables.Add(aVar[4]);
            attack.GetComponent<ColdDamage>().variables.Add(aVar[5]);
            attack.GetComponent<ColdDamage>().variables.Add(aVar[6]);
            attack.GetComponent<ColdDamage>().variables.Add(aVar[7]);
        }
        else if (type.Equals("bleed"))
        {
            Debug.Log(attack.GetComponent<BleedDamage>().type);
            Debug.Log(attack.GetComponent<BleedDamage>().variables.ToString());
            attack.GetComponent<BleedDamage>().variables.Add(aVar[0]);
            attack.GetComponent<BleedDamage>().variables.Add(aVar[1]);
            attack.GetComponent<BleedDamage>().variables.Add(aVar[2]);
            attack.GetComponent<BleedDamage>().variables.Add(aVar[3]);
            attack.GetComponent<BleedDamage>().variables.Add(aVar[4]);
            attack.GetComponent<BleedDamage>().variables.Add(aVar[5]);
            attack.GetComponent<BleedDamage>().variables.Add(aVar[6]);
            attack.GetComponent<BleedDamage>().variables.Add(aVar[7]);
        }
        else //if (type.Equals("magic"))
        {
            Debug.Log(attack.GetComponent<MagicDamage>().type);
            Debug.Log(attack.GetComponent<MagicDamage>().variables.ToString());
            attack.GetComponent<MagicDamage>().variables.Add(aVar[0]);
            attack.GetComponent<MagicDamage>().variables.Add(aVar[1]);
            attack.GetComponent<MagicDamage>().variables.Add(aVar[2]);
            attack.GetComponent<MagicDamage>().variables.Add(aVar[3]);
            attack.GetComponent<MagicDamage>().variables.Add(aVar[4]);
            attack.GetComponent<MagicDamage>().variables.Add(aVar[5]);
            attack.GetComponent<MagicDamage>().variables.Add(aVar[6]);
            attack.GetComponent<MagicDamage>().variables.Add(aVar[7]);
        }

        //Movement script adding
        if (moveType.Equals("straight"))
        {
            Debug.Log(attack.GetComponent<StraightMove>().variables.ToString());
            attack.GetComponent<StraightMove>().variables.Add(mVar[0]);
            attack.GetComponent<StraightMove>().variables.Add(mVar[1]);
            attack.GetComponent<StraightMove>().variables.Add(mVar[2]);
            attack.GetComponent<StraightMove>().direction = dir;
        }
        else if (moveType.Equals("bounce"))
        {
            Debug.Log(attack.GetComponent<BounceMove>().variables.ToString());
            attack.GetComponent<BounceMove>().variables.Add(mVar[0]);
            attack.GetComponent<BounceMove>().variables.Add(mVar[1]);
            attack.GetComponent<BounceMove>().variables.Add(mVar[2]);
            attack.GetComponent<BounceMove>().direction = dir;
        }
        else //if (moveType.Equals("arc"))
        {
            Debug.Log(attack.GetComponent<ArcMove>().variables.ToString());
            attack.GetComponent<ArcMove>().variables.Add(mVar[0]);
            attack.GetComponent<ArcMove>().variables.Add(mVar[1]);
            attack.GetComponent<ArcMove>().variables.Add(mVar[2]);
            attack.GetComponent<ArcMove>().direction = dir;
        }

        Debug.Log("Attempting to spawn on server");
        NetworkServer.Spawn(attack);

        Debug.Log("Setting Object life");
        Destroy(attack, attackLife);
    }
}
