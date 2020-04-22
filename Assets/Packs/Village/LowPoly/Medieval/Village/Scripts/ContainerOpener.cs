using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerOpener : EntityController
{
    public bool IsOpened  = true;
    public string message = "";
    public GameObject NoteGO;

    private Animator animator;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        
        if (IsOpened)
        {
            animator.SetBool("Opened", IsOpened);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Player" && !IsOpened)
        {
            StartCoroutine(Open());
        }
    }

    public IEnumerator Open()
    {
        LevelControllerInstance.BlockPlayer();
        
        animator.SetBool("Opened", true);
        IsOpened = true;

        yield return new WaitForSeconds(
            animator.GetCurrentAnimatorStateInfo(0).length
        );

        if (message != "")
        {
            NoteGO.GetComponent<NoteController>().ShowNote(message);
        } else {
            LevelControllerInstance.UnBlockPlayer();
        }
    }

    public void Close()
    {
        animator.SetBool("Opened", false);
        IsOpened = false;
    }

    public void Toggle()
    {
        if (IsOpened)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
}
