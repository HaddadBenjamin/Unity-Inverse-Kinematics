using UnityEngine;
using System.Collections;
using System;

public class IK3D : AIK
{
    #region Fields
    [SerializeField]
    private Transform upperArm, forearm, hand, target, elbowTarget;
    [SerializeField]
    private bool debug = true;
    [SerializeField]
    private float transition = 1.0f;

    private Transform myTransform;

    private Quaternion upperArmStartRotation, forearmStartRotation, handStartRotation;
    private Vector3 targetRelativeStartPosition, elbowTargetRelativeStartPosition;

    private GameObject upperArmAxisCorrection, forearmAxisCorrection, handAxisCorrection;
    #endregion

    #region Properties
    public float Transition
    {
        get
        {
            return transition;
        }

        set
        {
            transition = value;
        }
    }
    #endregion

    #region Unity Behaviour
    private void Start()
    {
        this.myTransform = transform;
        this.upperArmStartRotation = this.upperArm.rotation;
        this.forearmStartRotation = this.forearm.rotation;
        this.handStartRotation = this.hand.rotation;
        this.targetRelativeStartPosition = this.target.position - this.upperArm.position;
        this.elbowTargetRelativeStartPosition = this.elbowTarget.position - this.upperArm.position;

        this.upperArmAxisCorrection = new GameObject("upperArmAxisCorrection");
        this.forearmAxisCorrection = new GameObject("forearmAxisCorrection");
        this.handAxisCorrection = new GameObject("handAxisCorrection");
    }
    #endregion

    void LateUpdate()
    {
        if (base.EnableIK)
        {
            //Calculate ikAngle variable.
            float upperArmLength = Vector3.Distance(this.upperArm.position, this.forearm.position),
                  forearmLength = Vector3.Distance(this.forearm.position, this.hand.position),
                  armLength = upperArmLength + forearmLength,
                  hypotenuse = upperArmLength,
                  targetDistance = Vector3.Distance(this.upperArm.position, this.target.position);

            targetDistance = Mathf.Min(targetDistance, armLength - 0.0001f); //Do not allow target distance be further away than the arm's length.
                                                                             //float adjacent = (targetDistance * hypotenuse) / armLength;

            float adjacent = (Mathf.Pow(hypotenuse, 2) - Mathf.Pow(forearmLength, 2) + Mathf.Pow(targetDistance, 2)) / (2 * targetDistance),
                  ikAngle = Mathf.Acos(adjacent / hypotenuse) * Mathf.Rad2Deg;

            //Store pre-ik info.
            Vector3 targetPosition = this.target.position,
                    elbowTargetPosition = this.elbowTarget.position;
        
            Transform upperArmParent = this.upperArm.parent,
                      forearmParent = this.forearm.parent,
                      handParent = this.hand.parent;

            Vector3 upperArmScale = this.upperArm.localScale,
                    forearmScale = this.forearm.localScale,
                    handScale = this.hand.localScale,
                    upperArmLocalPosition = this.upperArm.localPosition,
                    forearmLocalPosition = this.forearm.localPosition,
                    handLocalPosition = this.hand.localPosition;

            Quaternion upperArmRotation = this.upperArm.rotation,
                       forearmRotation = this.forearm.rotation,
                       handRotation = this.hand.rotation;

            //Reset arm.
            this.target.position = this.targetRelativeStartPosition + this.upperArm.position;
            this.elbowTarget.position = this.elbowTargetRelativeStartPosition + this.upperArm.position;
            this.upperArm.rotation = this.upperArmStartRotation;
            this.forearm.rotation = this.forearmStartRotation;
            this.hand.rotation = this.handStartRotation;

            //Work with temporaty game objects and align & parent them to the arm.
            this.myTransform.position = upperArm.position;
            this.myTransform.LookAt(targetPosition, elbowTargetPosition - this.myTransform.position);

            this.upperArmAxisCorrection.SetActive(true);
            this.forearmAxisCorrection.SetActive(true);
            this.handAxisCorrection.SetActive(true);

            this.upperArmAxisCorrection.transform.position = this.upperArm.position;
            this.upperArmAxisCorrection.transform.LookAt(this.forearm.position, this.myTransform.root.up);
            this.upperArmAxisCorrection.transform.parent = this.myTransform;
            this.upperArm.parent = this.upperArmAxisCorrection.transform;
            this.forearmAxisCorrection.transform.position = this.forearm.position;
            this.forearmAxisCorrection.transform.LookAt(this.hand.position, this.myTransform.root.up);
            this.forearmAxisCorrection.transform.parent = this.upperArmAxisCorrection.transform;
            this.forearm.parent = this.forearmAxisCorrection.transform;
            this.handAxisCorrection.transform.position = this.hand.position;
            this.handAxisCorrection.transform.parent = this.forearmAxisCorrection.transform;
            this.hand.parent = this.handAxisCorrection.transform;

            //Reset targets.
            this.target.position = targetPosition;
            this.elbowTarget.position = elbowTargetPosition;

            //Apply rotation for temporary game objects.
            this.upperArmAxisCorrection.transform.LookAt(this.target, this.elbowTarget.position - this.upperArmAxisCorrection.transform.position);

            Vector3 eulerAngles = this.upperArmAxisCorrection.transform.localRotation.eulerAngles;

            this.upperArmAxisCorrection.transform.localEulerAngles = new Vector3(eulerAngles.x - ikAngle, eulerAngles.y, eulerAngles.z);
            this.forearmAxisCorrection.transform.LookAt(this.target, this.elbowTarget.position - this.upperArmAxisCorrection.transform.position);
            this.handAxisCorrection.transform.rotation = this.target.rotation;

            //Restore limbs.
            this.upperArm.parent = upperArmParent;
            this.forearm.parent = forearmParent;
            this.hand.parent = handParent;
            this.upperArm.localScale = upperArmScale;
            this.forearm.localScale = forearmScale;
            this.hand.localScale = handScale;
            this.upperArm.localPosition = upperArmLocalPosition;
            this.forearm.localPosition = forearmLocalPosition;
            this.hand.localPosition = handLocalPosition;

            //Clean up temporary game objets.
            this.upperArmAxisCorrection.SetActive(false);
            this.forearmAxisCorrection.SetActive(false);
            this.handAxisCorrection.SetActive(false);

            //Transition.
            this.Transition = Mathf.Clamp01(this.Transition);
            this.upperArm.rotation = Quaternion.Slerp(upperArmRotation, this.upperArm.rotation, this.Transition);
            this.forearm.rotation = Quaternion.Slerp(forearmRotation, this.forearm.rotation, this.Transition);
            this.hand.rotation = Quaternion.Slerp(handRotation, this.hand.rotation, this.Transition);

            //Debug.
            if (this.debug)
            {
                Debug.DrawLine(this.forearm.position, this.elbowTarget.position, Color.yellow);
                Debug.DrawLine(this.upperArm.position, this.target.position, Color.red);
            }
        }
    }

    #region Override Behaviour
    protected override void IKBehaviour()
    {
        
    }

    protected override void ResetIK()
    {
    }
    #endregion
}