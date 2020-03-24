using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed;
    private float damage;
    private float lifeTime;
    private float AliveTime;
    private Rigidbody rb;
    void Start()
    {
        speed = 20;
        lifeTime = 4;
        AliveTime = 0;
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != tag && collision.transform.gameObject.layer != 11)
        {
            ResetSelf();
        }
    }

    private void TimerTick()
    {
        if (this.gameObject.activeInHierarchy)
        {
            AliveTime = AliveTime + Time.deltaTime;

            if (AliveTime > lifeTime)
            {
                ResetSelf();
            }
        }
    }

    public void SetTeam(int s)
    {
        this.transform.gameObject.layer = s;
    }

    private void ResetSelf()
    {
        AliveTime = 0;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        TimerTick();
    }
}
