using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour
{
    [SerializeField] DialogueSystem _system;

    Text characterName;
    Text dialogueText;
    Text[] optionText = new Text[Constants.Amount.OF_OPTION_WINDOWS];

    void Start()
    {
        characterName = transform.GetChild(1).GetComponent<Text>();
        dialogueText  = transform.GetChild(2).GetComponent<Text>();

        for (int i = 0; i < optionText.Length; i++) {
            optionText[i] = transform.GetChild(3).GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>();
        }

        for (int i = 0; i < 4; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (_system != null) {
            _system.Activate += ShowWindow;
            _system.ChangeToNext += ChangeText;
            _system.Options += ShowOptions;
        }
    }

    void ShowWindow()
    {
        for (int i = 0; i < 4; i++) {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    void ChangeText(string name, string sentence)
    {
        characterName.text = name;
        dialogueText.text  = sentence;
    }

    void ShowOptions(string[] options)
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (optionText[i] != null) {
                optionText[i].text = options[i];
            }
        }
    }
}
