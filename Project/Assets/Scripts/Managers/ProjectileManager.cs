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
    }

    private void SpawnProjectile()
    {
        StartCoroutine(SpawnProjectileCoroutine());
    }

    IEnumerator SpawnProjectileCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        projectile = Instantiate(projectilePrefab);
        projectile.transform.position = new Vector3(spawner.transform.position.x + 3, 4, spawner.transform.position.z);
        PlaceProjectileObjectInActiveFrameHierarchy(frameManager.initialPlayerFrameRow, frameManager.initialPlayerFrameColumn); // PlayerFrame = SpawnerFrame
        projectile.GetComponent<ProjectileController>().projectileManager = this;
        projectile.SetActive(true);
        
    }

    private void PlaceProjectileObjectInActiveFrameHierarchy(int row, int col)
    {
        GameObject frame = frameManager.getFrameGameObject(row + 1, col + 1);
        projectile.transform.parent = frame.transform;
    }

    private Position CheckIfProjectileArrivedToNewFrame(GameObject projectile, GameObject openDoor)
    {
        Position projectilePosition = GetCurrentParentFramePosition(projectile);
        Position openDoorPosition = GetCurrentParentFramePosition(openDoor);
        return projectilePosition.col != openDoorPosition.col || projectilePosition.row != openDoorPosition.row ? GetParentFrameIntialCordinates(openDoor) : new Position(-1, -1);
    }

    private Position GetCurrentParentFramePosition(GameObject childObject)
    {
        while(childObject.transform.parent.name.Substring(0, childObject.transform.parent.name.Length - 2) != "Frame") // "Drills up" Hierarchy until FrameXY (row, column) is the parent
        {
            return GetCurrentParentFramePosition(childObject.transform.parent.gameObject);
        }
        GameObject frameController = childObject.transform.parent.GetChild(1).gameObject;
        return new Position(frameController.GetComponent<Frame>().currentRow - 1, frameController.GetComponent<Frame>().currentCol - 1);     
    }

    public void UpdateProjectilePositionIfNeeded(GameObject projectile, GameObject openDoor)
    {
        Position newPosition = CheckIfProjectileArrivedToNewFrame(projectile,openDoor);
        //Debug.Log(newPosition.row + "  " + newPosition.col);
        if (newPosition.row != -1 && newPosition.col != -1)
        {
            PlaceProjectileObjectInActiveFrameHierarchy(newPosition.row,newPosition.col);
        }
    }

    private Position GetParentFrameIntialCordinates(GameObject openDoor)
    {
        while (openDoor.transform.parent.name.Substring(0, openDoor.transform.parent.name.Length - 2) != "Frame") // "Drills up" Hierarchy until FrameXY (row, column) is the parent
        {
            return GetParentFrameIntialCordinates(openDoor.transform.parent.gameObject);
        }
        //Debug.Log(openDoor.transform.parent.name);
        return new Position(int.Parse(openDoor.transform.parent.name.Substring(openDoor.transform.parent.name.Length - 2,1)), int.Parse(openDoor.transform.parent.name.Substring(openDoor.transform.parent.name.Length - 1, 1)));
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
