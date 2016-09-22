using UnityEngine;

using System.Collections;

public sealed class FootIKInformation : ABaseIKInformation
{
    #region Override Behaviour
    protected override void GUIInformation()
    {
        base.GUIInformation();

        GUILayout.Label("Move with QSZD or arrow directionnal to see better the foot IK of your character.");
    }
    #endregion
}
