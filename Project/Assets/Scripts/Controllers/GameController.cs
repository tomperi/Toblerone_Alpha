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

    //used for laser levels
    private Laser laser;

    //used for mobile
    private Vector3 firstTouchPosition;
    private Vector3 secondTouchPosition;
    private float dragDistance;

    void Start()
    {
        isZoomedIn = startZoomedOut;
        startZoomInOut();

        laser = FindObjectOfType<Laser>();
        if (laser != null)
        {
            StartCoroutine(shootLaserAtStart());
        }

        dragDistance = Screen.width * 10 / 100; //drag distance is 15% of the screen
    }

    void Update()
    {
        if (!isZoomedIn)
        {
            player.StopAtPlace();
        }

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

        //move player/frame Mobile

        if (Input.touchCount == 1 && !isZoomedIn) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                firstTouchPosition = touch.position;
                secondTouchPosition = touch.position;
            }
            /*
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                secondTouchPosition = touch.position;
            }*/
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                secondTouchPosition = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 15% of the screen height
                if (Mathf.Abs(secondTouchPosition.x - firstTouchPosition.x) > dragDistance || Mathf.Abs(secondTouchPosition.z - firstTouchPosition.z) > dragDistance)
                {//It's a drag
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, frameLayerMask))
                    {
                        GameObject frameParent = hit.transform.parent.gameObject;

                        if (frameParent != null)
                        {
                            Frame frame = frameParent.GetComponentInChildren<Frame>();

                            if (frame != null)
                            {
                                int frameRow = frame.currentRow;
                                int frameCol = frame.currentCol;
                                int emptyFrameRow = frameManager.EmptyFrame.row;
                                int emptyFrameCol = frameManager.EmptyFrame.col;

                                //check if the drag is vertical or horizontal
                                if (Mathf.Abs(secondTouchPosition.x - firstTouchPosition.x) > Mathf.Abs(secondTouchPosition.z - firstTouchPosition.z))
                                {   //If the horizontal movement is greater than the vertical movement...
                                    if ((secondTouchPosition.x > firstTouchPosition.x))  //If the movement was to the right)
                                    {   //Right swipe
                                        if (frameRow == emptyFrameRow && frameCol == emptyFrameCol - 1)
                                        {
                                            frameManager.SwitchEmptyFrameLocation(Direction.Right);
                                            StartCoroutine(waitAndShootLaser());
                                        }
                                        Debug.Log("Right Swipe");
                                    }
                                    else
                                    {   //Left swipe
                                        if (frameRow == emptyFrameRow && frameCol == emptyFrameCol + 1)
                                        {
                                            frameManager.SwitchEmptyFrameLocation(Direction.Left);
                                            StartCoroutine(waitAndShootLaser());
                                        }
                                        Debug.Log("Left Swipe");
                                    }
                                }
                                else if (Mathf.Abs(secondTouchPosition.x - firstTouchPosition.x) < Mathf.Abs(secondTouchPosition.z - firstTouchPosition.z))
                                {   //the vertical movement is greater than the horizontal movement
                                    if (secondTouchPosition.z > firstTouchPosition.z)  //If the movement was up
                                    {   //Up swipe
                                        if (frameRow == emptyFrameRow - 1 && frameCol == emptyFrameCol)
                                        {
                                            frameManager.SwitchEmptyFrameLocation(Direction.Up);
                                            StartCoroutine(waitAndShootLaser());
                                        }
                                        Debug.Log("Up Swipe");
                                        
                                    }
                                    else
                                    {   //Down swipe
                                        if (frameRow == emptyFrameRow + 1 && frameCol == emptyFrameCol)
                                        {
                                            frameManager.SwitchEmptyFrameLocation(Direction.Down);
                                            StartCoroutine(waitAndShootLaser());
                                        }
                                        Debug.Log("Down Swipe");
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
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
            }
        }
        
        //move player mobile
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
        /*

        //Rotate Frame Mobile
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
        }*/

        // Move frame PC
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

        // pause PC (not replicated in mobile)
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
