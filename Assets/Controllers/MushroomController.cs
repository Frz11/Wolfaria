using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MushroomController : EntityController
{
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
        LevelControllerInstance.DoShroomEffect(type);
        Destroy(this.gameObject);
    }

}
