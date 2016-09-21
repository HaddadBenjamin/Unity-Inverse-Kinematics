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

        GUILayout.Label("Dans cette scène, votre personnage regarde une balle qui se déplace et vous pouvez modifier comment il la regarde.");

        GUILayout.BeginHorizontal();
        GUILayout.Label("Position de la balle sur l'axe x.");
        this.BallRightSpeed = GUILayout.HorizontalSlider(this.BallRightSpeed, 0.0f, 1.0F);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Position de la balle sur l'axe y.");
        this.BallUpSpeed = GUILayout.HorizontalSlider(this.BallUpSpeed, 0.0f, 0.325f);
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        GUILayout.Label("Poids de l'IK.");
        this.Weight = GUILayout.HorizontalSlider(this.Weight, 0.0f, 1.0f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Poids de l'IK du corps.");
        this.BodyWeight = GUILayout.HorizontalSlider(this.BodyWeight, 0.0f, 1.0f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Poids de l'IK de la tête.");
        this.HeadWeight = GUILayout.HorizontalSlider(this.HeadWeight, 0.0f, 1.0f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Poids de l'IK des yeux.");
        this.EyesWeight = GUILayout.HorizontalSlider(this.EyesWeight, 0.0f, 1.0f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Poids du clamp de l'IK.");
        this.ClampWeight = GUILayout.HorizontalSlider(this.ClampWeight, 0.0f, 1.0f);
        GUILayout.EndHorizontal();
    }
    #endregion
}