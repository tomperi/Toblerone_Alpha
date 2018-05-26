using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{

    public GameObject spawner;
    public GameObject projectilePrefab;
    public GameObject projectile;
    public FrameManager frameManager;
    public SpawnProjectileDirection spawnDirection;
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
        if (Input.GetKeyDown(KeyCode.T)) // Cheat Code!
        {
            DestroyFloatingStone();
        }
    }

    public void SpawnProjectile()
    {
        if (!projectileIsAlive && stoneIsAlive)
        {
            StartCoroutine(SpawnProjectileWithDelay());
        }
    }

    public void EnableNewProjectilesOnDeath()
    {
        projectileIsAlive = false;
    }

    public void DestroyFloatingStone()
    {
        stoneIsAlive = false;
        projectileIsAlive = false;
        spawner.GetComponent<FloatingStoneController>().PlayDeathAndDestory();
    }

    IEnumerator SpawnProjectileWithDelay()
    {
        yield return new WaitForSeconds(1f);
        projectileIsAlive = true;
        projectile = Instantiate(projectilePrefab);
        int direction = spawnDirection.Equals(SpawnProjectileDirection.Right) ? 1 : -1;
        projectile.transform.position = new Vector3(spawner.transform.position.x + 5 * direction, spawner.transform.position.y + 3, spawner.transform.position.z);
        projectile.GetComponent<ProjectileController>().projectileManager = this;
        projectile.SetActive(true);
    }
    


}
