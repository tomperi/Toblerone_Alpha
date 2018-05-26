using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableFloors : MonoBehaviour {
    public GameObject[] gameObjects;

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject go in gameObjects)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                GameObject child = go.transform.GetChild(i).gameObject;
                child.SetActive(!child.activeSelf);
            }
        }
    }
}
