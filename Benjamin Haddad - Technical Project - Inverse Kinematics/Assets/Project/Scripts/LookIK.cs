using UnityEngine;
using System.Collections;
using System;

public class LookIK : AIK
{
    #region Fields
    private float weight;
    private float bodyWeight = 0.0f;
    private float headWeight = 1.00f;
    private float eyesWeight = 0.00f;
    private float clampWeight = 0.50f;
    #endregion

    #region Override Behaviour
    protected override void IKBehaviour()
    {
    }

    protected override void ResetIK()
    {
    }
    #endregion
}