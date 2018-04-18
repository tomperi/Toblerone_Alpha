using UnityEngine;

public class Frame : MonoBehaviour
{

    public int currentRow;
    public int currentCol;

    private FrameManager frameManager;

    void Start () 
	{
        frameManager = FindObjectOfType<FrameManager>();
	}

	void Update () 
	{
		if (Input.GetMouseButtonDown (1)) 
		{
			Vector3 NewPosition = GetComponent<Transform> ().position;
			NewPosition.x -= 10;
			this.GetComponent<Transform> ().position = NewPosition;
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            frameManager.SwitchActiveFrame(currentRow, currentCol);
            //Debug.Log("Current active frame is " + currentRow + ", " + currentCol);
        }
    }
}
