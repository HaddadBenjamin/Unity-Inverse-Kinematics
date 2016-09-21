using UnityEngine;
using System.Collections;
using System;

public sealed class HandIK : AIK
{
    #region Fields
    [SerializeField]
    private Transform rightHandPlaceHolder = null;
    [SerializeField]
    private Transform leftHandPlaceHolder = null;
    #endregion

    #region Unity Behaviour
    void Start()
    {
        
    }
    #endregion

    #region Override Behaviour
    protected override void IKBehaviour()
    {
        base.MyAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        base.MyAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        if (this.rightHandPlaceHolder != null)
        {
            base.MyAnimator.SetIKPosition(AvatarIKGoal.RightHand, this.rightHandPlaceHolder.position);
            base.MyAnimator.SetIKRotation(AvatarIKGoal.RightHand, this.rightHandPlaceHolder.rotation);
        }

        base.MyAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        base.MyAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        if (this.leftHandPlaceHolder != null)
        {
            base.MyAnimator.SetIKPosition(AvatarIKGoal.LeftHand, this.leftHandPlaceHolder.position);
            base.MyAnimator.SetIKRotation(AvatarIKGoal.LeftHand, this.leftHandPlaceHolder.rotation);
        }
    }

    protected override void ResetIK()
    {
        base.MyAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
        base.MyAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
        base.MyAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
        base.MyAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
    }
    #endregion
}
