using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChange1 : DialogueInteract
{
    public DialogueData[] nextGroup;
    int cycle;

    public override void StartText() {
        if (cycle != 0) {
            base.sentences = nextGroup;
        }
        base.StartText();
        cycle = 1;
    }
}
