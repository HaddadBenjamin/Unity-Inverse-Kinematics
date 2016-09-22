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
        GUILayout.Label("Position x de la cible.");
        this.target.SetLocalPositionX(GUILayout.HorizontalSlider(this.target.localPosition.x, -3.0f, 3.0f));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Position y de la cible.");
        this.target.SetLocalPositionY(GUILayout.HorizontalSlider(this.target.localPosition.y, -3.0f, 3.0f));
        GUILayout.EndHorizontal();
    }
}