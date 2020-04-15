using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TutorialController : LevelController
{
    [SerializeField]
    private GameObject Human, Dog;

    void Start()
    {
        DetectedPlayerNumber = 0;
        SheepNumber = 4;
        ShowSheeps();
        Player = PlayerObject.GetComponent<PlayerController>();


        State = 0;
        StateAction();
    }

    private void Update()
    {
        if (DetectedPlayerNumber == 1)
        {
            ShowTextNow("Sheeps will run if you enter in their field of view, attracting the attention of their guardians");
            DetectedPlayerNumber++;
        }
    }

    protected override void StateAction()
    {

        switch (State)
        {
            case 0:
                BlockPlayer();
                StartCoroutine(
                    ShowText(
                        new string[3]
                        {
                            "Pinch to zoom in or out",
                            "The objective is to eat all the sheeps",
                            "Press and hold on screen to make the wolf move around",
                        },
                        new float[3] { 0, 2, 2 },
                        "UnBlockPlayer"
                    )
                );

                State++;
                break;

            case 1:
                StartCoroutine(
                    ShowText(
                        new string[1]
                        {
                            "Well Done!",
                        },
                        new float[1] { 0 },
                        "BlockPlayer"
                   )
                );

				StartCoroutine(State1());
                break;

        }
    }

    private IEnumerator State1()
    {
		yield return new WaitForSeconds(4f);

        PlayerObject.transform.position = new Vector3(25, PlayerObject.transform.position.y, 65);

		StartCoroutine(
            ShowText(
                new string[1]
                {
                    "There are two types of enemies that you will encounter",
                },
                new float[1] { 0 }
            )
        );

        yield return new WaitForSeconds(5f);

		//show
        StartCoroutine(
            ShowText(
                new string[3]
                {
                    "The dog can smell you if you get too close to him, but will not detect you if you are behind an object",
                    "If he catches you, you will lose one heart and he will die",
                    "Upon death, the dog barks and attracts nearby enemies as well as making sheeps flee",
                },
                new float[3] {0, 2, 2}    
            )
        );


        yield return new WaitForSeconds(20f);

        Dog.SetActive(false);
        Human.SetActive(true);

        //hide + show next enemy

        StartCoroutine(
           ShowText(
               new string[2]
               {
                    "The shepheard will detect you if he sees you",
                    "If he catches you, you will lose the level",
               },
               new float[2] { 0, 2 },
               null,
               true,
               2f
           )
       );

       yield return new WaitForSeconds(10f);

       Player.target = new Vector3(29, PlayerObject.transform.position.y, 64);
       Player.move_to_target = true;

       StartCoroutine(
            ShowText(
                new string[1]
                {
                    "Hidding in a bush will make you invisible to everyone else!",
                },
                new float[1] { 0 }
            )
       );

        yield return new WaitForSeconds(5f);

        Human.GetComponent<HumanController>().target = new Vector3(27, Human.transform.position.y, 66);
        Human.GetComponent<HumanController>().is_moving = true;

        yield return new WaitForSeconds(5f);
        PlayerObject.transform.position = new Vector3(36, PlayerObject.transform.position.y, 67);

        StartCoroutine(
            ShowText(
                new string[1]
                {
                    "Mushrooms have random effects on you, from healing to killing you",
                },
                new float[1] { 0 }
            )
        );
        yield return new WaitForSeconds(5f);

        Player.target = new Vector3(36, PlayerObject.transform.position.y, 70);
        Player.move_to_target = true;

        yield return new WaitForSeconds(2f);
        Check.SetActive(true);

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Menu");

        
    }
}
