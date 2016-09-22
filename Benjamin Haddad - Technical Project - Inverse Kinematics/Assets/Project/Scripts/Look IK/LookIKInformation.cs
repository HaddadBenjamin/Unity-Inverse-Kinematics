using UnityEngine;
using System.Collections;

public sealed class LookIKInformation : ABaseIKInformation
{
    #region Fields
    [SerializeField]
    private BallMovement ballMovement;
    private LookIK lookIK;

    private float ballUpSpeed;
    private float ballRightSpeed;

    private float weight;
    private float bodyWeight = 0.0f;
    private float headWeight = 1.00f;
    private float eyesWeight = 0.00f;
    private float clampWeight = 0.50f;
    #endregion

    #region Properties
    public float BallUpSpeed
    {
        get
        {
            return ballUpSpeed;
        }

        set
        {
            this.ballMovement.MoveUpSpeed = ballUpSpeed = value;
        }
    }

    public float BallRightSpeed
    {
        get
        {
            return ballRightSpeed;
        }

        set
        {
            this.ballMovement.MoveRightSpeed = ballRightSpeed = value;
        }
    }

    public float Weight
    {
        get
        {
            return weight;
        }

        set
        {
            this.lookIK.Weight = weight = value;
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
            this.lookIK.BodyWeight = bodyWeight = value;
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
            this.lookIK.HeadWeight = headWeight = value;
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
            this.lookIK.EyesWeight = eyesWeight = value;
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
            this.lookIK.ClampWeight = clampWeight = value;
        }
    }
    #endregion

    #region Unity Behaviour
    void Start()
    {
        this.BallUpSpeed = this.ballMovement.MoveUpSpeed;
        this.BallRightSpeed = this.ballMovement.MoveRightSpeed;

        this.lookIK = base.AIK as LookIK;

        this.Weight = this.lookIK.Weight;
        this.BodyWeight = this.lookIK.BodyWeight;
        this.HeadWeight = this.lookIK.HeadWeight;
        this.ClampWeight= this.lookIK.ClampWeight;
        this.EyesWeight = this.lookIK.EyesWeight;
    }
    #endregion

    #region Override Behaviour
    protected override void GUIInformation()
    {
        base.GUIInformation();

        GUILayout.Label("In this scene your character is looking a ball in movement and you can modify how your character is looking it.");

        GUILayout.BeginHorizontal();
        GUILayout.Label("Ball position x.");
        this.BallRightSpeed = GUILayout.HorizontalSlider(this.BallRightSpeed, 0.0f, 1.0F);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Ball position y.");
        this.BallUpSpeed = GUILayout.HorizontalSlider(this.BallUpSpeed, 0.0f, 0.325f);
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        GUILayout.Label("IK weight.");
        this.Weight = GUILayout.HorizontalSlider(this.Weight, 0.0f, 1.0f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Body IK eeight.");
        this.BodyWeight = GUILayout.HorizontalSlider(this.BodyWeight, 0.0f, 1.0f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Head IK weight.");
        this.HeadWeight = GUILayout.HorizontalSlider(this.HeadWeight, 0.0f, 1.0f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Eyes IK weight.");
        this.EyesWeight = GUILayout.HorizontalSlider(this.EyesWeight, 0.0f, 1.0f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Clamp IK.");
        this.ClampWeight = GUILayout.HorizontalSlider(this.ClampWeight, 0.0f, 1.0f);
        GUILayout.EndHorizontal();
    }
    #endregion
}