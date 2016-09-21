using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Animator))]
public class HandIK : AIK
{
    #region Fields
    private Animator myAnimator;
    [SerializeField]
    private Transform rightHandPlaceHolder = null;
    [SerializeField]
    private Transform leftHandPlaceHolder = null;
    #endregion

    #region Unity Behaviour
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        if (this.myAnimator)
        {
            if (base.EnableIK)
            {
                this.myAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
                this.myAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

                if (this.rightHandPlaceHolder != null)
                {
                    this.myAnimator.SetIKPosition(AvatarIKGoal.RightHand, this.rightHandPlaceHolder.position);
                    this.myAnimator.SetIKRotation(AvatarIKGoal.RightHand, this.rightHandPlaceHolder.rotation);
                }

                this.myAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
                this.myAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

                if (this.leftHandPlaceHolder != null)
                {
                    this.myAnimator.SetIKPosition(AvatarIKGoal.LeftHand, this.leftHandPlaceHolder.position);
                    this.myAnimator.SetIKRotation(AvatarIKGoal.LeftHand, this.leftHandPlaceHolder.rotation);
                }
            }

            else
            {
                this.myAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                this.myAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                this.myAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                this.myAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            }
        }
    }
    #endregion
}
