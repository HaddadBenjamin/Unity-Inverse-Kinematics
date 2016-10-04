using UnityEngine;
using System.Collections;

public sealed class AnalyticTwoBoneIK2DInformation : ABaseIKInformation
{
    [SerializeField]
    private Transform target;
    private AnalyticTwoBoneIK2D IK;

    void Start()
    {
        this.IK = GetComponent<AnalyticTwoBoneIK2D>();

        base.enableIKListener += delegate (bool input)
        {
            //this.IK.EnableIK = input;
        };
    }

    protected override void GUIInformation()
    {
        base.GUIInformation();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Target position x.");
        this.target.SetLocalPositionX(GUILayout.HorizontalSlider(this.target.localPosition.x, -3.0f, 3.0f));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Target position y.");
        this.target.SetLocalPositionY(GUILayout.HorizontalSlider(this.target.localPosition.y, -3.0f, 3.0f));
        GUILayout.EndHorizontal();

        GUILayout.Label(string.Format("<color=yellow>Angle 1 : {0}, Angle 2 : {1}</color>", this.IK.AngleBone1, this.IK.AngleBone2));
        }
}