using UnityEngine;
using UnityEngine.AI;

public class FrameManager : MonoBehaviour
{

    //Initial values that are defined in the heirarchy
    public int rows, cols;
    public int initialPlayerFrameRow;
    public int initialPlayerFrameColumn;
    public int initialEmptyFrameRow;
    public int initialEmptyFrameColumn;

    //Game Objects that need to be defined in the heirarchy
    public GameObject player;

    //Internal values that are used in other files
    private GameObject[,] frames;
    private position activeFrame;
    private position emptyFrame;
    private NavMeshAgent agent;

    public struct position
    {
        public int row, col;
    }

    void Start()
    {
        initFrameArray();

        agent = player.GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        // Move frame
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            switchEmptyFrameLocation(emptyFrame.row + 1, emptyFrame.col);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            switchEmptyFrameLocation(emptyFrame.row, emptyFrame.col - 1);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            switchEmptyFrameLocation(emptyFrame.row - 1, emptyFrame.col);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            switchEmptyFrameLocation(emptyFrame.row, emptyFrame.col + 1);
        }
    }

    private void initFrameArray()
    {
        frames = new GameObject[rows + 2, cols + 2];
        GameObject currentFrame;
        Frame currentFrameScript;

        for (int i = 1; i <= rows; i++)
        {
            for (int j = 1; j <= cols; j++)
            {
                currentFrame = GameObject.Find("Frame" + (i - 1) + (j - 1));
                frames[i, j] = currentFrame;
                // Set the position in the frame script
                currentFrameScript = currentFrame.GetComponentInChildren<Frame>();
                if(currentFrameScript != null)
                {
                    currentFrameScript.currentRow = i;
                    currentFrameScript.currentCol = j;
                }
            }
        }

        activeFrame.row = initialPlayerFrameRow + 1;
        activeFrame.col = initialPlayerFrameColumn + 1;
        emptyFrame.row = initialEmptyFrameRow + 1;
        emptyFrame.col = initialEmptyFrameColumn + 1;
    }

    public void switchEmptyFrameLocation(int row, int col)
    {
        if (frames[row, col] != null)
        {
            if (isActiveFrame(row, col))
            {
                SwitchPlayerPosition(row, col);
                SwitchActiveFrame(emptyFrame.row, emptyFrame.col);
                //Debug.Log("Move player!");
            }

            SwitchFramePositionWithEmptyFramePosition(row, col);

        }
        
    }

    public void SwitchActiveFrame(int row, int col)
    {
        activeFrame.row = row;
        activeFrame.col = col;
    }

    public void SwitchFramePositionWithEmptyFramePosition(int row, int col)
    {
        GameObject currentEmptyFrame = frames[emptyFrame.row, emptyFrame.col];
        GameObject frameToMove = frames[row, col];

        Vector3 emptyFramePosition = currentEmptyFrame.GetComponent<Transform>().position;
        currentEmptyFrame.GetComponent<Transform>().position = frameToMove.GetComponent<Transform>().position;
        frameToMove.GetComponent<Transform>().position = emptyFramePosition;

        frames[row, col] = currentEmptyFrame;
        frames[emptyFrame.row, emptyFrame.col] = frameToMove;
        emptyFrame.row = row;
        emptyFrame.col = col;
    }

    public void SwitchPlayerPosition(int row, int col)
    {
        agent.enabled = false;
        Vector3 playerRelativePosition = frames[row, col].transform.position - player.gameObject.transform.position;
        player.gameObject.transform.position = frames[emptyFrame.row, emptyFrame.col].gameObject.transform.position - playerRelativePosition;
        agent.enabled = true;
    }

    private bool isActiveFrame(int row, int col)
    {
        return row == activeFrame.row && col == activeFrame.col;
    }
}
