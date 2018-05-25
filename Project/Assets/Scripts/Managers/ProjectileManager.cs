using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{

    public GameObject spawner;
    public GameObject projectilePrefab;
    public GameObject projectile;
    public FrameManager frameManager;
    private bool projectileIsAlive;
    private bool stoneIsAlive;
    
    // Use this for initialization
    void Start()
    {
        projectileIsAlive = false;
        stoneIsAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!projectileIsAlive && stoneIsAlive)
        {
            projectileIsAlive = true;
            SpawnProjectile();
        }

        if (Input.GetKeyDown(KeyCode.T)) // Cheat Code!
        {
            DestroyFloatingStone();
        }
    }

    private void SpawnProjectile()
    {
        StartCoroutine(SpawnProjectileCoroutine());
    }

    IEnumerator SpawnProjectileCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        projectile = Instantiate(projectilePrefab);
        projectile.transform.position = new Vector3(spawner.transform.position.x + 4, 3f, spawner.transform.position.z);
        projectile.GetComponent<ProjectileController>().projectileManager = this;
        projectile.SetActive(true);
        
    }

    public void SpawnNewProjectileOnDeath()
    {
        projectileIsAlive = false;
    }

    public void DestroyFloatingStone()
    {
        stoneIsAlive = false;
        projectileIsAlive = false;
        spawner.GetComponent<FloatingStoneController>().PlayDeathAndDestory();
    }


}
