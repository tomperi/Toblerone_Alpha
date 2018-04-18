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
    public bool allowZoomInOut;
    public bool startZoomedOut;
    public bool isPlayerInLevel;

    private bool zoomIn;

    private Laser laser;

    
    void Start()
    {
        zoomIn = startZoomedOut;
        startZoomInOut();

        laser = FindObjectOfType<Laser>();
        StartCoroutine(shootLaserAtStart());
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
            if (zoomIn && isPlayerInLevel)
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

        //rotate frame
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
                        StartCoroutine(waitAndShootLaser());
                    }
                }
            }
        }

        // Move frame
        if ((Input.GetKeyDown(KeyCode.UpArrow)) && (!zoomIn))
        {
            frameManager.SwitchEmptyFrameLocation(FrameManager.Direction.Up);
            StartCoroutine(waitAndShootLaser());
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow)) && (!zoomIn))
        {
            frameManager.SwitchEmptyFrameLocation(FrameManager.Direction.Right);
            StartCoroutine(waitAndShootLaser());
        }

        if ((Input.GetKeyDown(KeyCode.DownArrow)) && (!zoomIn))
        {
            frameManager.SwitchEmptyFrameLocation(FrameManager.Direction.Down);
            StartCoroutine(waitAndShootLaser());
        }

        if ((Input.GetKeyDown(KeyCode.LeftArrow)) && (!zoomIn))
        {
            frameManager.SwitchEmptyFrameLocation(FrameManager.Direction.Left);
            StartCoroutine(waitAndShootLaser());
        }
    }

    void zoomInOut()
    {
        if (allowZoomInOut)
        {
            if (zoomIn)
            {
                zoomOutCamera.SetActive(true);
                zoomIn = false;
                if (isPlayerInLevel)
                {
                    player.StopAtPlace();
                }
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

    public void startZoomInOut()
    {
        if (zoomIn)
        {
            zoomOutCamera.SetActive(true);
            zoomIn = false;
            if (isPlayerInLevel)
            {
                player.StopAtPlace();
            }
            //Debug.Log("Zoom out");
        }
        else
        {
            zoomOutCamera.SetActive(false);
            zoomIn = true;
            //Debug.Log("Zoom in");
        }
    }

    IEnumerator shootLaserAtStart()
    {
        yield return new WaitForSeconds(0.3f);
        if (laser != null)
        {
            laser.ShootLaser();
        }
    }

    IEnumerator waitAndShootLaser()
    {
        if (laser != null)
        {
            laser.resetLaser();
        }

        yield return new WaitForSeconds(0.3f);
        if (laser != null)
        {
            laser.ShootLaser();
        }
    }


}
