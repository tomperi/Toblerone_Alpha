using UnityEngine;

public class Frame : MonoBehaviour
{

    public int currentRow;
    public int currentCol;

    void Start () 
	{

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
        if(other.gameObject.tag == "player")
        {
            
        }
    }
}
