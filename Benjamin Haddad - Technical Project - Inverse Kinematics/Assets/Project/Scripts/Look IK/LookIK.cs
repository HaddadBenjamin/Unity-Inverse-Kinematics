using UnityEngine;
using System.Collections;
using System;

public sealed class LookIK : AIK
{
    #region Fields
    [SerializeField]
    private Transform lookObjectPlaceHolder;

    [SerializeField]
    private float weight = 1.0f;
    [SerializeField]
    private float bodyWeight = 0.0f;
    [SerializeField]
    private float headWeight = 1.00f;
    [SerializeField]
    private float eyesWeight = 0.00f;
    [SerializeField]
    private float clampWeight = 0.50f;
    #endregion

    #region Properties
    public float Weight
    {
        get
        {
            return weight;
        }

        set
        {
            weight = value;
        }
    }

    public float BodyWeight
    {
        get
        {
            return bodyWeight;
        }

        set
        {
            bodyWeight = value;
        }
    }

    public float HeadWeight
    {
        get
        {
            return headWeight;
        }

        set
        {
            headWeight = value;
        }
    }

    public float EyesWeight
    {
        get
        {
            return eyesWeight;
        }

        set
        {
            eyesWeight = value;
        }
    }

    public float ClampWeight
    {
        get
        {
            return clampWeight;
        }

        set
        {
            clampWeight = value;
        }
    }
    #endregion

    #region Override Behaviour
    protected override void IKBehaviour()
    {
        if (this.lookObjectPlaceHolder != null)
        {
            base.MyAnimator.SetLookAtWeight(this.Weight, this.BodyWeight, this.HeadWeight, this.EyesWeight, this.ClampWeight);
            base.MyAnimator.SetLookAtPosition(this.lookObjectPlaceHolder.position);
        }
    }

    protected override void ResetIK()
    {
        base.MyAnimator.SetLookAtWeight(0.0f);
    }
    #endregion
}