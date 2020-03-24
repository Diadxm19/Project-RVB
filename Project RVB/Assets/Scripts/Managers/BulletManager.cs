using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private GameObject bulletHolder;
    private GameObject bullet;
    private int MaxBullets;
    private List<GameObject> bulletList;

    // Start is called before the first frame update
    public void BulletManagerStart()
    {
        bulletHolder = GameObject.Find("BulletHolder");
        bullet = GameObject.Find("Bullet");
        MaxBullets = 300;
        bullet.transform.parent = bulletHolder.transform;

        FillBullets();
    }

    private void FillBullets()
    {
        bulletList = new List<GameObject>();
        for (int i = 0; i < MaxBullets; i++)
        {
            GameObject temp = GameObject.Instantiate(bullet);
            temp.transform.parent = bulletHolder.transform;
            temp.transform.position = bulletHolder.transform.position;
            bulletList.Add(temp);
            temp.SetActive(false);
        }
    }

    public GameObject GetBullet()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeInHierarchy)
            {
                return bulletList[i];
            }
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
