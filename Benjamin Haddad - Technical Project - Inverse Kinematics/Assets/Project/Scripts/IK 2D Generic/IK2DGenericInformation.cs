using UnityEngine;
using System.Collections;

public sealed class IK2DGenericInformation : ABaseIKInformation
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private IKFirstNode IK;

    void Start()
    {
        base.enableIKListener += delegate (bool input)
        {
            this.IK.EnableIK = input;
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
    }
}