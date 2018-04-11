using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject zoomOutCamera;
    public Camera mainCamera;
    public PlayerController player;
    public FrameManager frameManager;
    public LayerMask playerLayerMask;
    public LayerMask frameLayerMask;

    private bool zoomIn;

    
    void Start()
    {
        zoomIn = false;
        zoomInOut();
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            zoomInOut();
        }

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

        // Move frame
        if ((Input.GetKeyDown(KeyCode.UpArrow)) && (!zoomIn))
        {
            frameManager.SwitchEmptyFrameLocation(FrameManager.Direction.Up);
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow)) && (!zoomIn))
        {
            frameManager.SwitchEmptyFrameLocation(FrameManager.Direction.Right);
        }

        if ((Input.GetKeyDown(KeyCode.DownArrow)) && (!zoomIn))
        {
            frameManager.SwitchEmptyFrameLocation(FrameManager.Direction.Down);
        }

        if ((Input.GetKeyDown(KeyCode.LeftArrow)) && (!zoomIn))
        {
            frameManager.SwitchEmptyFrameLocation(FrameManager.Direction.Left);
        }
    }

    void zoomInOut()
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
