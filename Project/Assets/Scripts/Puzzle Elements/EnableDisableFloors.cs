using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableFloors : MonoBehaviour {

    public GameObject[] gameObjects;
    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            StartCoroutine(zoomOutAndSwitch());   
        }
    }

    IEnumerator zoomOutAndSwitch()
    {
        if (gameController.IsZoomedIn)
        {
            gameController.zoomInOut();
        }

        yield return new WaitForSeconds(1f);

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
