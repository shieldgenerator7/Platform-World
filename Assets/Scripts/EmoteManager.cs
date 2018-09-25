using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteManager : MonoBehaviour
{

    public Emote selectedEmote;
    public List<Emote> unlockedEmotes = new List<Emote>();

    private bool isEmoting;
    public bool Emoting
    {
        get { return isEmoting; }
        set
        {
            if (isEmoting != value)
            {
                isEmoting = value;
                //2018-09-25: copied from https://docs.unity3d.com/ScriptReference/AnimatorOverrideController.html
                animatorOverrideController["Emote"] = selectedEmote.animationClip;
                //
                anim.SetBool("isEmoting", isEmoting);
            }
        }
    }

    private Animator anim;
    public AnimationState emoteState;
    private AnimatorOverrideController animatorOverrideController;

    // Use this for initialization
    void Start()
    {
        selectedEmote = unlockedEmotes[0];
        anim = GetComponent<Animator>();

        //2018-09-25: copied from https://docs.unity3d.com/ScriptReference/AnimatorOverrideController.html
        animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = animatorOverrideController;
    }
}
