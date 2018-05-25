using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingStoneController : MonoBehaviour {

    private Animator animator;
    private MoveToNextLevelScript moveToNextLevelScript;

    void Start()
    {
        animator = GetComponent<Animator>();
        moveToNextLevelScript = FindObjectOfType<MoveToNextLevelScript>();
    }

    public void PlayDeathAndDestory()
    {
        animator.SetBool("StoneDeath", true);
        Destroy(gameObject, 2f);
    }

    private void OnDestroy()
    {
        moveToNextLevelScript.OnLevelComplete();
    }

    IEnumerator CompleteLevelCoroutine()
    {
        yield return new WaitForSeconds(2f);
    }



}
