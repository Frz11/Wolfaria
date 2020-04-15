using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class LevelController : MonoBehaviour
{

    public int  SheepNumber;
    public Text Dialog;
    public Text SheepText;
    public int State;
    public int DetectedPlayerNumber;

    public GameObject PlayerObject, Check;
    protected PlayerController Player;

    // Start is called before the first frame update
    void Start()
    {
        DetectedPlayerNumber = 0;
        Debug.Log(PlayerObject);
        Player = PlayerObject.GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void StateAction() {}

    public virtual void ShowSheeps()
    {
        SheepText.text = SheepNumber.ToString();

        if (SheepNumber == 0)
        {
            StateAction();
        }
    }

    protected void UnBlockPlayer()
    {
        Player.Blocked = false;
    }

    protected void BlockPlayer()
    {
        Player.Blocked = true;
    }


    public void ShowTextNow(string text)
    {
        StartCoroutine(ShowText(new string[1] { text }, new float[1] { 0 }));
    }

    protected IEnumerator ShowText(string[] texts, float[] delays, string exitAction = null, bool clear = true, float clearTime = 4f)
    {
        if (texts.Length == delays.Length)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                yield return new WaitForSeconds(delays[i]);

                Dialog.text = texts[i];


                if (clear)
                {
                    yield return new WaitForSeconds(clearTime);

                    Dialog.text = "";
                }
            }

            if (exitAction != null && exitAction.Length > 0)
            {
                Invoke(exitAction, 0);
            }
        } else
        {
            Debug.LogError("Texts and delays have different lenghts in ShowText method.");
        }
    }

}
