using System.Collections;
using UnityEngine;
using TMPro;
using System.IO;

public class TheDialogueBox : MonoBehaviour
{
    public TMP_Text mainTextComponent;

    public string[] dialogue;
    private int index;

    void Start()
    {
        index = -1;
    }
    public void Next(){
        index++;
        mainTextComponent.text=dialogue[index];
    }
}
