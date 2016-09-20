using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Animator))]
public class HandIK : MonoBehaviour
{
    #region Fields
    private bool enableIK;
    private Animator myAnimator;
    [SerializeField]
    private Transform rightHandPlaceHolder = null;
    #endregion

    #region Properties
    public bool EnableIK
    {
        get
        {
            return enableIK;
        }

        set
        {
            enableIK = value;
        }
    }
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
            if (this.enableIK)
            {
                this.myAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
                this.myAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

                if (this.rightHandPlaceHolder != null)
                {
                    this.myAnimator.SetIKPosition(AvatarIKGoal.RightHand, this.rightHandPlaceHolder.position);
                    this.myAnimator.SetIKRotation(AvatarIKGoal.RightHand, this.rightHandPlaceHolder.rotation);
                }
            }

            else
            {
                this.myAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                this.myAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            }
        }
    }
    #endregion
}
