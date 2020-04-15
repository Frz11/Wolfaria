using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button Tutorial, Exit;
    public GameObject Menu, Intro;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame());

        Tutorial.onClick.AddListener(PlayTutorial);
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

    void PlayTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);

        Intro.SetActive(false);
        Menu.SetActive(true);
    }
}
