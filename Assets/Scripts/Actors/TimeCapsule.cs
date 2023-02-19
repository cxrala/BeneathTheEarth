using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCapsule : Collectable
{
    public Sprite openedTimeCapsule;
    private Animator animator;

    public Animator timeCapsuleAnimator;

    IEnumerator WaitForFunction(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        timeCapsuleAnimator.SetBool("Show", true);
        animator.SetBool("open", false);
    }

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = openedTimeCapsule;
            animator = GetComponent<Animator>();

            animator.SetBool("open", true);

            StartCoroutine(WaitForFunction(3.0f));
        }
    }
}
