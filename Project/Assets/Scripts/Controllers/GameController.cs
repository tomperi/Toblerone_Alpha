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

    private bool isZoomedIn;

    private Laser laser;

    
    void Start()
    {
        isZoomedIn = startZoomedOut;
        startZoomInOut();
        laser = FindObjectOfType<Laser>();
        if (laser != null)
        {
            StartCoroutine(shootLaserAtStart());
        }
    }

    void Update()
    {
        //zoom in or out PC
        if (Input.GetButtonDown("Jump"))
        {
            zoomInOut();
        }

        //zoom in or out Mobile
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            if (deltaMagnitudeDiff < 0 && !isZoomedIn)
            {
                zoomInOut();
            }
            else if ((deltaMagnitudeDiff > 0 && isZoomedIn))
            {
                zoomInOut();
            }

        }

        //move player PC
        if (Input.GetMouseButtonDown(0))
        {
            if (isZoomedIn && isPlayerInLevel)
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

        //move player Mobile
        if (Input.touchCount == 1 && isZoomedIn)
        {
            Touch touch = Input.touches[0];
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, playerLayerMask))
            {
                GameObject recipient = hit.transform.gameObject;

                if (touch.phase == TouchPhase.Ended)
                {
                    player.GoToPosition(hit.point);
                }
            }
        }

        //rotate frame PC
        if (Input.GetMouseButtonDown(1)) // Mouse Right Click
        {
            if (!isZoomedIn)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;


                if (Physics.Raycast(ray, out hit, 10000f, floorLayerMask))
                {
                    GameObject frame = hit.transform.parent.gameObject;

                    if (frame != null)
                    {
                        frame.transform.Rotate(new Vector3(0f, 90f, 0f));
                        foreach (Transform transformChild in frame.transform) // Messy, needs to fix later! ~ Amir
                        {
                            if(transformChild.name == "ShadowProjectile(Clone)")
                            {
                                transformChild.gameObject.GetComponent<ProjectileController>().ChangeDirectionOnRotate();
                            }
                        }
                        StartCoroutine(waitAndShootLaser());
                    }
                }
            }
        }

        //Rotate Frame Mobile

        //move player Mobile
        if (Input.touchCount == 1 && !isZoomedIn)
        {
            Touch touch = Input.touches[0];
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, floorLayerMask))
            {
                if (touch.phase == TouchPhase.Ended)
                {
                    GameObject frame = hit.transform.parent.gameObject;

                    if (frame != null)
                    {
                        frame.transform.Rotate(new Vector3(0f, 90f, 0f));
                        foreach (Transform transformChild in frame.transform) // Messy, needs to fix later! ~ Amir
                        {
                            if (transformChild.name == "ShadowProjectile(Clone)")
                            {
                                transformChild.gameObject.GetComponent<ProjectileController>().ChangeDirectionOnRotate();
                            }
                        }
                        StartCoroutine(waitAndShootLaser());
                    }
                }
            }
        }

        // Move frame
        if ((Input.GetKeyDown(KeyCode.UpArrow)) && (!isZoomedIn))
        {
            frameManager.SwitchEmptyFrameLocation(Direction.Up);
            StartCoroutine(waitAndShootLaser());
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow)) && (!isZoomedIn))
        {
            frameManager.SwitchEmptyFrameLocation(Direction.Right);
            StartCoroutine(waitAndShootLaser());
        }

        if ((Input.GetKeyDown(KeyCode.DownArrow)) && (!isZoomedIn))
        {
            frameManager.SwitchEmptyFrameLocation(Direction.Down);
            StartCoroutine(waitAndShootLaser());
        }

        if ((Input.GetKeyDown(KeyCode.LeftArrow)) && (!isZoomedIn))
        {
            frameManager.SwitchEmptyFrameLocation(Direction.Left);
            StartCoroutine(waitAndShootLaser());
        }

        if ((Input.GetKeyDown(KeyCode.P))) {
            ToggleTimeScale();
        } 
    }

    void zoomInOut()
    {
        if (allowZoomInOut)
        {
            if (isZoomedIn)
            {
                zoomOutCamera.SetActive(true);
                isZoomedIn = false;
                if (isPlayerInLevel)
                {
                    player.StopAtPlace();
                }
                //Debug.Log("Zoom out");
            }
            else
            {
                zoomOutCamera.SetActive(false);
                isZoomedIn = true;
                //Debug.Log("Zoom in");
            }
        }
    }

    public void startZoomInOut()
    {
        if (isZoomedIn)
        {
            zoomOutCamera.SetActive(true);
            isZoomedIn = false;
            if (isPlayerInLevel)
            {
                player.StopAtPlace();
            }
            //Debug.Log("Zoom out");
        }
        else
        {
            zoomOutCamera.SetActive(false);
            isZoomedIn = true;
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

    private void ToggleTimeScale()
    {
       Time.timeScale = (Time.timeScale + 1) % 2;
    }


}
