using UnityEngine;
using UnityEngine.UI;

public class NoteController : EntityController
{
    public Text NoteText;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        GetComponent<Button>().onClick.AddListener(Disband);
    }

    public void Disband()
    {
        this.gameObject.SetActive(false);
        LevelControllerInstance.UnBlockPlayer();
    }

    public void ShowNote(string text)
    {
        gameObject.SetActive(true);
        NoteText.text = text;
    }
}
