using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MushroomController : EntityController
{
    public float duration = 10f;
    [SerializeField]
    private string type;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            LevelControllerInstance.DoShroomEffect(type, duration);
            Destroy(this.gameObject);
        }
    }

}
