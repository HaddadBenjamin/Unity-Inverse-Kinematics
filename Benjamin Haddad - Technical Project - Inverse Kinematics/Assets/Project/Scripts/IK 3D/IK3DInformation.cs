using UnityEngine;
using System.Collections;

public sealed class IK3DInformation : ABaseIKInformation
{
    private IK3D myOwnIK;
    private float transition;
    [SerializeField]
    private Transform elbowTarget = null,
                      handTarget = null;

    public float Transition
    {
        get
        {
            return transition;
        }

        set
        {
            transition = this.myOwnIK.Transition = value;
        }
    }

    protected override void GUIInformation()
    {
        base.GUIInformation();

        GUILayout.Label("In this scene you can modify the IK behaviour by moving the elbow and hand target of your character.");

        GUILayout.BeginHorizontal();
        GUILayout.Label("IK Weight.");
        this.Transition = GUILayout.HorizontalSlider(this.Transition, 0.0f, 1.0f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Hand target position x.");
        this.handTarget.SetLocalPositionX(GUILayout.HorizontalSlider(this.handTarget.localPosition.x, -3.0f, .0f));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Hand target position y.");
        this.handTarget.SetLocalPositionY(GUILayout.HorizontalSlider(this.handTarget.localPosition.y, 0.0f, 3.0f));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Hand target position z.");
        this.handTarget.SetLocalPositionZ(GUILayout.HorizontalSlider(this.handTarget.localPosition.z, -3.0f, 3.0f));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Elbow target position x.");
        this.elbowTarget.SetLocalPositionX(GUILayout.HorizontalSlider(this.elbowTarget.localPosition.x, -3.0f, .0f));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Elbow target position y.");
        this.elbowTarget.SetLocalPositionY(GUILayout.HorizontalSlider(this.elbowTarget.localPosition.y, 0.0f, 3.0f));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Elbow target position z.");
        this.elbowTarget.SetLocalPositionZ(GUILayout.HorizontalSlider(this.elbowTarget.localPosition.z, -3.0f, 3.0f));
        GUILayout.EndHorizontal();
    }

    void Start()
    {
        this.myOwnIK = base.AIK as IK3D;

        this.Transition = this.myOwnIK.Transition;
    }
}