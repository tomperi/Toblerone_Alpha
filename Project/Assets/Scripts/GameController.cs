using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject zoomOutCamera;
    public Camera mainCamera;
    public PlayerController player;
    public FrameManager frameManager;
    public LayerMask playerLayerMask;
    public LayerMask frameLayerMask;
    public LayerMask floorLayerMask;
<<<<<<< HEAD
    public bool isPlayerNotInLevel;
=======
>>>>>>> 1024912be0f5f8dc538298ccd0555c17e09ab704

    private bool zoomIn;

    private Laser laser;

    
    void Start()
    {
        zoomIn = !isPlayerNotInLevel;
        zoomInOut();

        laser = FindObjectOfType<Laser>();
        StartCoroutine(waitAndShootLaser());
    }
    
    void Update()
    {
        //zoom in or out
        if (Input.GetButtonDown("Jump"))
        {
            zoomInOut();
        }

        //move player
        if (Input.GetMouseButtonDown(0))
        {
            if (zoomIn)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 10000f, playerLayerMask))
                {
                    player.GoToPosition(hit.point);
                }
            }
            else
            {
                // Use raycast to change frames
            }
        }

<<<<<<< HEAD
        //rotate frame
=======
>>>>>>> 1024912be0f5f8dc538298ccd0555c17e09ab704
        if (Input.GetMouseButtonDown(1))
        {
            if (!zoomIn)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;


                if (Physics.Raycast(ray, out hit, 10000f, floorLayerMask))
                {
                    GameObject frame = hit.transform.parent.gameObject;

                    if (frame != null)
                    {
                        frame.transform.Rotate(new Vector3(0f, 90f, 0f));
<<<<<<< HEAD
                        laser.ShootLaser();
=======
>>>>>>> 1024912be0f5f8dc538298ccd0555c17e09ab704
                    }
                }
            }
        }

        // Move frame
        if ((Input.GetKeyDown(KeyCode.UpArrow)) && (!zoomIn))
        {
            frameManager.SwitchEmptyFrameLocation(FrameManager.Direction.Up);
            laser.ShootLaser();
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow)) && (!zoomIn))
        {
            frameManager.SwitchEmptyFrameLocation(FrameManager.Direction.Right);
            laser.ShootLaser();
        }

        if ((Input.GetKeyDown(KeyCode.DownArrow)) && (!zoomIn))
        {
            frameManager.SwitchEmptyFrameLocation(FrameManager.Direction.Down);
            laser.ShootLaser();
        }

        if ((Input.GetKeyDown(KeyCode.LeftArrow)) && (!zoomIn))
        {
            frameManager.SwitchEmptyFrameLocation(FrameManager.Direction.Left);
            laser.ShootLaser();
        }
    }

    void zoomInOut()
    {
        if (!isPlayerNotInLevel)
        {
            if (zoomIn)
            {
                zoomOutCamera.SetActive(true);
                zoomIn = false;
                player.StopAtPlace();
                //Debug.Log("Zoom out");
            }
            else
            {
                zoomOutCamera.SetActive(false);
                zoomIn = true;
                //Debug.Log("Zoom in");
            }
        }
    }

    IEnumerator waitAndShootLaser()
    {
        yield return new WaitForSeconds(0.1f);
        laser.ShootLaser();
    }


}
