using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //As this game is based on turns but with the twist that
    //players have to commit their turns simultaneously
    //there is a commit turn button, which is currently unimplemented
    public class CommitButton : GemFlipButton
    {
        public override void onClick()
        {
            Debug.Log("Commit clicked");
        }
    }

