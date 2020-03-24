using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingSMG : Operators
{
    // Start is called before the first frame update
    void Start()
    {
        OperatorStart("rick",1,0.25f,3.5f,5,1f,0.6f);
    }

    private void FixedUpdate()
    {
        ShootAtEnemy();
        LookAhead();
        ResetVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetCharacterCanMove())
        {
            CooldownTimer();
        }
    }
}
