using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Operators : MonoBehaviour
{
    // Start is called before the first frame update
    private string Name;
    private float Health;
    private float Damage;
    private float Speed;
    [SerializeField]private Weapons Weapon;
    private float MaxDistance;
    private float MovementCooldown;
    private float CoolingDown;
    [SerializeField] private SphereCollider sCollider;
    private bool canMove;
    private float Accuracy;
    private bool hasShot;
    private float FireTimer;
    private float strength;
    float DistanceFromEnemy;
    private GameManager gameManager;
    [SerializeField]private Rigidbody rb;
    private bool ObjBlocking;

    private GameObject target;

    [SerializeField]private NavMeshAgent agent;
    public void OperatorStart(string name, float health, float damage, float speed, float travel, float cooldown,  float accuracy)
    {
        SetName(name);
        SetHealth(health);
        SetDamage(damage);
        SetSpeed(speed);
        SetMaxDistance(travel);
        SetMovementCooldown(cooldown);
        canMove = true;
        SetAccuracyPlayer(accuracy);
        hasShot = false;
        FireTimer = 0;
        strength = 10f;
        gameManager = GameObject.Find("GameManagerPF").GetComponent<GameManager>();
        ObjBlocking = false;

        // View Collider stuff
        GetsCollider().radius = GetMaxDistance();

        //Nav Mesh Stuff
        GetNavMeshAgent().speed = GetSpeed();
    }

    public void CooldownTimer()
    {
        CoolingDown = CoolingDown + Time.deltaTime;

        if (CoolingDown >= MovementCooldown)
        {
            CoolingDown = 0;
            canMove = true;
        }
    }

    private void SelectTarget(GameObject go)
    {
        if (target == null)
        {
            SettargetEnemy(go);
        }
        else
        {
            if (go.transform.name != GetTargetEnemy().transform.name)
            {
                float DistanceFromGO = Vector3.Distance(this.gameObject.transform.position, go.transform.position);
                float DistanceFromTarget = Vector3.Distance(this.gameObject.transform.position, GetTargetEnemy().transform.position);

                if (DistanceFromGO < DistanceFromTarget)
                {
                    SettargetEnemy(go);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other is SphereCollider)
        {
            // Do nothing
        }
        else if (other is CapsuleCollider)
        {
            if  (other.gameObject.layer != this.gameObject.layer)
            {
                SelectTarget(other.gameObject);
                DistanceFromEnemy = Vector3.Distance(this.gameObject.transform.position, GetTargetEnemy().transform.position);
                TurnToEnemy();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if  (other is CapsuleCollider || other.transform.gameObject.layer != this.gameObject.layer)
        {
            SelectTarget(null);
        }
    }

    public void LookAhead()
    {
        RaycastHit[] hit = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), GetMaxDistance());
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject.layer == 10)
            {
                float DistanceFromWall = Vector3.Distance(hit[i].transform.position, this.gameObject.transform.position);

                if (DistanceFromWall < DistanceFromEnemy || GetTargetEnemy() == null)
                {
                    ObjBlocking = true;
                    Debug.Log("Wall is in front of me");
                }
            }
        }
    }

    public void ShootAtEnemy()
    {
        if (GetTargetEnemy() && !hasShot && !ObjBlocking)
        {
            Debug.Log(GetTargetEnemy());
            Debug.Log("Testing");
            float rand = Random.Range(1, 100);
            if (rand > (CalculateAccuracy() * 100))
            {
                ShootBullet();
                hasShot = true;
                GetWeapon().ShotBullet();
                DealDamage(GetTargetEnemy(), GetDamage());
            }
            else if (rand < (CalculateAccuracy() * 100))
            {
                ShootBullet();
                hasShot = true;
                GetWeapon().ShotBullet();
            }
        }

        if (hasShot)
        {
            
            FireTimer = FireTimer + Time.deltaTime;
            if (FireTimer >= GetWeapon().GetRateOfFire())
            {
                FireTimer = 0;
                hasShot = false;
            }
        }
    }

    public void TurnToEnemy()
    {
        if (GetTargetEnemy() != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(GetTargetEnemy().transform.position - transform.position);
            var str = Mathf.Min(strength * Time.deltaTime, 1);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
        }
        if (GetTargetEnemy() == null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Input.mousePosition - transform.position);
            var str = Mathf.Min(strength * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
        }
    }

    public void TakeDamage(GameObject go, float dmg)
    {
        SetHealth(GetHealth() - dmg);
        if (GetHealth() <= 0)
        {
            Destroy(go);
        }
    }

    private void ShootBullet()
    {
        GameObject temp = gameManager.GetBulletManager().GetBullet();
        temp.transform.position = this.transform.position;
        temp.transform.rotation = this.transform.rotation;
        temp.GetComponent<Bullet>().SetTeam(this.gameObject.layer);
        temp.SetActive(true);
    }

    private void ShootBulletMiss()
    {

    }

    private void DealDamage(GameObject go, float dmg)
    {
        go.GetComponent<Operators>().TakeDamage(go, dmg);
    }

    private float CalculateAccuracy()
    {
        float finalAccuracy;
        finalAccuracy = (GetPlayerAccuracy() + GetWeapon().GetWeaponAccuracy() / 2);

        return finalAccuracy;
    }

    public void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }

    ////////////////////////////////////////////////////////////
    //Setters//
    public void SetName(string n) { Name = n; }
    public void SetHealth(float hp) { Health = hp; }
    public void SetDamage(float dmg) { Damage = dmg; }
    public void SetSpeed(float s) { Speed = s; }
    public void SetWeapon(Weapons w) { Weapon = w; }
    public void SetNavMeshAgent(NavMeshAgent a) { agent = a; }
    public void SetsCollider(SphereCollider s) { sCollider = s; }
    public void SetMaxDistance(float d) { MaxDistance = d; }
    public void SetMovementCooldown(float m) { MovementCooldown = m; }
    public void SetCharacterCanMove(bool b) { canMove = b; }
    public void SettargetEnemy(GameObject go) { target = go; }
    public void SetAccuracyPlayer(float f) { Accuracy = f; }
    //Getters//
    public string GetName() { return Name; }
    public float GetHealth() { return Health; }
    public float GetDamage() { return Damage; }
    public float GetSpeed() { return Speed; }
    public Weapons GetWeapon() { return Weapon; }
    public NavMeshAgent GetNavMeshAgent() { return agent; }
    public SphereCollider GetsCollider() { return sCollider; }
    public float GetMaxDistance() { return MaxDistance; }
    public float GetMovementCooldown() { return MovementCooldown; }
    public bool GetCharacterCanMove() { return canMove; }
    public GameObject GetTargetEnemy() { return target; }
    public float GetPlayerAccuracy() { return Accuracy; }
    public GameManager GetGameManager() { return gameManager; }
}
