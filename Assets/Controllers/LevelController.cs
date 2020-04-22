using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class LevelController : MonoBehaviour
{

    public int  SheepNumber;
    public TextMeshProUGUI Dialog;
    public Text SheepText;
    public int State;
    public int DetectedPlayerNumber;
    public GameObject PlayerObject, Check, WeirdEffect;

    protected PlayerController Player;
    protected int LevelIndex;

    // Start is called before the first frame update
    protected void Start()
    {
        DetectedPlayerNumber = 0;
        Player = PlayerObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoShroomEffect(string type, float duration = 10f)
    {
        switch(type)
        {
            case "poison":
                Player.is_dead = true;
                StartCoroutine(
                    ShowText(
                        new string[1] { "That mushroom just killed you!" },
                        new float[1] { 0 }
                    )
                );
                break;

            case "psychedelic":
                StartCoroutine(DisplayWeirdEffect(duration));
                break;
        }
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

    public void UnBlockPlayer()
    {
        Player.Blocked = false;
    }

    public void BlockPlayer()
    {
        Player.Blocked = true;
    }


    public void ShowTextNow(string text)
    {
        StartCoroutine(ShowText(new string[1] { text }, new float[1] { 0 }));
    }

    protected IEnumerator DisplayWeirdEffect(float seconds)
    {
        WeirdEffect.SetActive(true);
        yield return new WaitForSeconds(seconds);
        WeirdEffect.SetActive(false);
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

    protected IEnumerator FinishLevel()
    {
        Check.SetActive(true);
        yield return new WaitForSeconds(5f);
        PlayerPrefs.SetInt("CurrentLevel", LevelIndex + 1);
        SceneManager.LoadScene("Menu");
    }


}
