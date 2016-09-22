using UnityEngine;
using System.Collections;

public sealed class HandIKInformation : ABaseIKInformation
{
    #region Override Behaviour
    protected override void GUIInformation()
    {
        base.GUIInformation();

        GUILayout.Label("Enable / Disable IK to hold the item in front of your character.");
    }
    #endregion
}
