using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameAttachable : MonoBehaviour {

    private FrameManager frameManager;

    private void Start()
    {
        frameManager = FindObjectOfType<FrameManager>();
    }

    private void PlaceGameObjectInActiveFrameHierarchy(int row, int col)
    {
        GameObject frame = frameManager.getFrameGameObject(row + 1, col + 1);
        transform.parent = frame.transform;
    }

    private Position CheckIfGameObjectArrivedToNewFrame(GameObject frame)
    {
        Position gameObjectPosition = GetCurrentParentFramePosition(gameObject);
        Position framePosition = GetCurrentParentFramePosition(frame);
        return gameObjectPosition.col != framePosition.col || gameObjectPosition.row != framePosition.row ? GetParentFrameIntialCordinates(frame) : new Position(-1, -1);
    }

    private Position GetCurrentParentFramePosition(GameObject childObject)
    {
        if (childObject.transform.parent == null)
        {
            return new Position(-1, -1);
        }
        while (childObject.transform.parent.name.Substring(0, childObject.transform.parent.name.Length - 2) != "Frame")
        {
            return GetCurrentParentFramePosition(childObject.transform.parent.gameObject);
        }
        GameObject frameController = childObject.transform.parent.GetChild(1).gameObject;
        return new Position(frameController.GetComponent<Frame>().currentRow - 1, frameController.GetComponent<Frame>().currentCol - 1);
    }

    public void UpdateGameObjectPositionIfNeeded(GameObject frame)
    {
        Position newPosition = CheckIfGameObjectArrivedToNewFrame(frame);
        if (newPosition.row != -1 && newPosition.col != -1)
        {
            PlaceGameObjectInActiveFrameHierarchy(newPosition.row, newPosition.col);
        }
    }

    private Position GetParentFrameIntialCordinates(GameObject frame)
    {
        while (frame.transform.parent.name.Substring(0, frame.transform.parent.name.Length - 2) != "Frame")
        {
            return GetParentFrameIntialCordinates(frame.transform.parent.gameObject);
        }
        return new Position(int.Parse(frame.transform.parent.name.Substring(frame.transform.parent.name.Length - 2, 1)), int.Parse(frame.transform.parent.name.Substring(frame.transform.parent.name.Length - 1, 1)));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FrameController")
        {
            UpdateGameObjectPositionIfNeeded(other.gameObject);
            Debug.Log(other.gameObject.name + " - " + LayerMask.LayerToName(other.gameObject.layer));
        }
    }
}
