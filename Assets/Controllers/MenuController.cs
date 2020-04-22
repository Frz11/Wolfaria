using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button Exit;
    public GameObject Menu, Intro;

    private int CurrentLevel;
    private Transform Levels;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame());
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            Debug.Log("HAS!");
            CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }

        Levels = Menu.transform.Find("Levels");

        int levelIndex = 0;
        foreach (Transform level in Levels.transform)
        {
            if (levelIndex <= CurrentLevel)
            {
                level
                    .GetComponent<Button>()
                    .onClick
                    .AddListener(() => { PlayLevel(level.name); });

                level.Find("Unavailable").gameObject.SetActive(false);

                if (levelIndex < CurrentLevel)
                {
                    level.Find("check").gameObject.SetActive(true);
                }

                levelIndex++;

            } else
            {
                level
                    .GetComponent<Button>()
                    .onClick
                    .AddListener(() => { PlayLevel("Not reached!"); });
            }

        }

        //Tutorial.onClick.AddListener(() => { PlayLevel(0); });
        //Level1.onClick.AddListener(() => { PlayLevel(1); });
        Exit.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void PlayLevel(string level)
    {
        switch(level)
        {
            default:
                Transform Dialog = Menu.transform.Find("Dialog");
                Dialog.gameObject.SetActive(true);
                Button DialogButton = Dialog.GetComponent<Button>();
                DialogButton.onClick.AddListener(() => { Dialog.gameObject.SetActive(false); });
                break;

            case "Tutorial":
                SceneManager.LoadScene("Tutorial");
                break;

            case "Level 1":
                SceneManager.LoadScene("Level1");
                break;               
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);

        Intro.SetActive(false);
        Menu.SetActive(true);
    }
}
