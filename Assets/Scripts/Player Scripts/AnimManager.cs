using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    //singleton
    public static AnimManager anim;

    public Animator playerAnimator;
    public string[] boolNames;
    //public List<int> animBools = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        if (anim != this) {
            anim = this;
        }
        else {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    /* void AnimBoolSetup() {
        for (int i = 0; i <= boolNames.Length; ++i) {
            animBools.Add(playerAnimator.StringToHash(boolNames[i]));
        }
    } */
}
