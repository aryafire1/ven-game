using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesNo : DialogueInteract
{
    [Header("Class Specific Variables")]
    public DialogueData[] yesData;
    public DialogueData[] noData;
    DialogueData[] baseData;
    bool yes, no;

    public override void Start() {
        baseData = base.sentences;
        base.Start();
    }

    public override void StartText() {
        if (yes) {
            base.sentences = yesData;
            yes = false;
        }
        else if (no) {
            base.sentences = noData;
            no = false;
        }
        else {
            base.sentences = baseData;
        }
        base.StartText();
    }

    //button ui voids
    public void Yes() {
        yes = true;
    }
    public void No() {
        no = true;
    }
}
