using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private BulletManager bulletManager;
    // Start is called before the first frame update
    void Start()
    {
        playerManager.PlayerManagerStart();
        bulletManager.BulletManagerStart();
    }
    private void FixedUpdate()
    {
        playerManager.PlayerManagerFixedUpdate();
    }
    // Update is called once per frame
    void Update()
    {
        playerManager.PlayerManagerUpdate();
    }

    public BulletManager GetBulletManager() { return bulletManager; }
}
