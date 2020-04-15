using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MushroomController : MonoBehaviour
{
    public GameObject ShroomEffect, WeirdEffect;

    [SerializeField]
    private string type;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
            switch (type)
            {
                case "poison":
                    other.GetComponent<PlayerController>().is_dead = true;
                    ShroomEffect.SetActive(true);
                    ShroomEffect.GetComponent<Text>().text = "That mushroom just killed you!";
                    Destroy(this.gameObject);

                    break;

                case "psychedelic":
                    StartCoroutine(psychedelicFun());
                    break;
                default:
                    //random effect
                    break;
            }

            Destroy(this.gameObject);


        }
    }

    private IEnumerator psychedelicFun()
    {
        WeirdEffect.SetActive(true);
        yield return new WaitForSeconds(10f);

    }
}
