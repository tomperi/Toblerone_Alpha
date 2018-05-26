using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTrigger : MonoBehaviour {

    private ProjectileManager projectileManager;
    private GameController gameController;

    private GameObject offSprite;
    private GameObject onSprite;
    private bool isOff;

    private void Start()
    {
        projectileManager = FindObjectOfType<ProjectileManager>();
        gameController = FindObjectOfType<GameController>();
        offSprite = transform.GetChild(0).gameObject;
        onSprite = transform.GetChild(1).gameObject;

        isOff = true;
        offSprite.SetActive(true);
        onSprite.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            StartCoroutine(waitAndDo());
        }
    }

    IEnumerator waitAndDo()
    {

        if (isOff)
        {
            isOff = false;
            offSprite.SetActive(false);
            onSprite.SetActive(true);
        }
        else
        {
            isOff = true;
            offSprite.SetActive(true);
            onSprite.SetActive(false);
        }

        yield return new WaitForSeconds(0.3f);

        if (gameController.IsZoomedIn)
        {
            gameController.zoomInOut();
        }

        projectileManager.SpawnProjectile();
    }

}
