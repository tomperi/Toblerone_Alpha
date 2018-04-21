using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawnManager : MonoBehaviour
{

    public GameObject spawner;
    public GameObject projectile;
    private bool projectileIsAlive;
    // Use this for initialization
    void Start()
    {
        projectileIsAlive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!projectileIsAlive && spawner.activeInHierarchy)
        {
            projectileIsAlive = true;
            SpawnProjectile();
        }

    }

    private void SpawnProjectile()
    {
        StartCoroutine(SpawnProjectileCoroutine());
    }

    IEnumerator SpawnProjectileCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        projectile = Instantiate(projectile);
        projectile.transform.position = new Vector3(spawner.transform.position.x + 3, 4, spawner.transform.position.z);
        projectile.SetActive(true);
        
    }
}
