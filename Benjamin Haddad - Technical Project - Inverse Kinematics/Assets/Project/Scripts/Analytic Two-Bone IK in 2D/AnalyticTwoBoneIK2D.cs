using UnityEngine;
using System.Collections;
using System;


public class AnalyticTwoBoneIK2D : AIK
{
    #region Fields
    [SerializeField]
    private Transform target,
                      bone1,
                      bone2;

    private Vector3 targetInitialPosition;

    private const float bone1Length = 0.5f,
                        bone2Length = 0.5f;
    #endregion

    #region Properties
    public float AngleBone1
    {
        get
        {
            return this.bone1.eulerAngles.z;
        }

        private set
        {
            Vector3 eulerAngles = this.bone1.eulerAngles;

            this.bone1.eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y, value);
        }
    }

    public float AngleBone2
    {
        get
        {
            return this.bone2.eulerAngles.z;
        }

        private set
        {
            Vector3 eulerAngles = this.bone2.eulerAngles;

            this.bone2.eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y, value);
        }
    }

    private bool IsPositiveAngle
    {
        get
        {
            return this.bone2.position.GetSignOfAngle(this.target.position) > 0.0f;
        }
    }

    public float TargetXPosition
    {
        get
        {
            return this.target.position.x;
        }
    }

    public float TargetYPosition
    {
        get
        {
            return this.target.position.y;
        }
    }
    #endregion

    #region Unity Behaviour
    private void Start()
    {
        this.targetInitialPosition = this.target.position;
    }
    #endregion

    #region Override Behaviour
    /// <summary>
    /// J'ai tenté d'implémenter l'algorithme Analytic Two-Bone IK 2D : http://www.ryanjuckett.com/programming/analytic-two-bone-ik-in-2d/
    /// </summary>
    protected override void IKBehaviour()
    {
        if (this.Validate())
        {
            #region Looking For Angle 2
            float x = this.target.position.x - this.bone1.position.x;
            float y = this.target.position.y - this.bone1.position.y;

            this.AngleBone2 = Mathf.Abs(Mathf.Acos(
                (x.Square() + y.Square() - bone1Length.Square() - bone2Length.Square()) /
                (2.0f * bone1Length * bone2Length)) * Mathf.Rad2Deg);
            #endregion

            #region Looking For Angle 1
            float cosAngle2 = Mathf.Cos(this.AngleBone2 / Mathf.Rad2Deg);
            float sinAngle2 = Mathf.Sin(this.AngleBone2 / Mathf.Rad2Deg);

            this.AngleBone1 = Mathf.Atan2(
                                y * (bone1Length + bone2Length * cosAngle2) - x * (bone2Length * sinAngle2),
                                x * (bone1Length + bone2Length * cosAngle2) + y * (bone2Length * sinAngle2)) *
                                Mathf.Rad2Deg;
            #endregion
            //Debug.LogFormat("Angle 1 : {0}, Angle 2 : {1}", this.AngleBone1, this.AngleBone2);
        }
    }

    protected override void ResetIK()
    {
        this.target.position = this.targetInitialPosition;
        this.IKBehaviour();
    }
    #endregion

    #region Intern Behaviour
    private bool Validate()
    {
        return  bone1Length > 0.0f &&
                bone2Length > 0.0f;
    }
    #endregion
}
