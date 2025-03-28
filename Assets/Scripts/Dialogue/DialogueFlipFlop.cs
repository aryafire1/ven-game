using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueFlipFlop : DialogueInteract
{
    DialogueData[] baseGroup;
    public DialogueData[] nextGroup;
    bool cycle;

    public override void Start() {
        base.Start();
        baseGroup = base.sentences;
    }
    public override void StartText() {
        if (cycle == true) {
            base.sentences = nextGroup;
        }
        else if (cycle == false) {
            base.sentences = baseGroup;
        }
        base.StartText();
        cycle = !cycle;
    }
}
