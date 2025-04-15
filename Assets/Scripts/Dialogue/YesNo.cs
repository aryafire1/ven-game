using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesNo : DialogueInteract
{
    [Header("Class Specific Variables")]
    public DialogueData[] yesData;
    public DialogueData[] noData;
    DialogueData[] baseData;
    public GameObject choiceObject;
    bool yes, no;

    public override void Start() {
        baseData = base.sentences;
        choiceObject.SetActive(false);
        base.Start();
    }

    public override void StartText() {
        base.StartText();
    }

    public override void TypeSkip() {
        choiceObject.SetActive(false);

        if (yes == false && no == false && base.text.text == base.sentences[base.Index].text) {
            if (base.Index >= base.sentences.Length - 1) {
                choiceObject.SetActive(true);
            }
        }
        if (base.Index >= base.sentences.Length - 1) {
            if (yes || no) {
                base.sentences = baseData;
                yes = false;
                no = false;
                base.Reset();
            }
        }
        base.TypeSkip();
    }

    //button ui voids
    public void Yes() {
        yes = true;
        base.sentences = yesData;
        choiceObject.SetActive(false);
        Invoke("StartText", 0.01f);
    }
    public void No() {
        no = true;
        base.sentences = noData;
        choiceObject.SetActive(false);
        Invoke("StartText", 0.01f);
    }
}
