using UnityEngine;
using System.Collections;

public class IK3DInformation : ABaseIKInformation
{
    private IK3D myOwnIK;
    private float transition;
    [SerializeField]
    private Transform elbowTarget, handTarget;

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

        GUILayout.Label("Dans cette scène vous pouvez modifier le le comportement de l'IK en déplacant la cible du coude et de la main du personnage.");

        GUILayout.BeginHorizontal();
        GUILayout.Label("Valeur de transition de l'IK.");
        this.Transition = GUILayout.HorizontalSlider(this.Transition, 0.0f, 1.0f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Position x de la cible de la main.");
        this.handTarget.SetLocalPositionX(GUILayout.HorizontalSlider(this.handTarget.localPosition.x, -3.0f, .0f));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Position y de la cible de la main.");
        this.handTarget.SetLocalPositionY(GUILayout.HorizontalSlider(this.handTarget.localPosition.y, 0.0f, 3.0f));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Position z de la cible de la main.");
        this.handTarget.SetLocalPositionZ(GUILayout.HorizontalSlider(this.handTarget.localPosition.z, -3.0f, 3.0f));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Position x de la cible du coude.");
        this.elbowTarget.SetLocalPositionX(GUILayout.HorizontalSlider(this.elbowTarget.localPosition.x, -3.0f, .0f));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Position y de la cible du coude.");
        this.elbowTarget.SetLocalPositionY(GUILayout.HorizontalSlider(this.elbowTarget.localPosition.y, 0.0f, 3.0f));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Position z de la cible du coude.");
        this.elbowTarget.SetLocalPositionZ(GUILayout.HorizontalSlider(this.elbowTarget.localPosition.z, -3.0f, 3.0f));
        GUILayout.EndHorizontal();
    }

    void Start()
    {
        this.myOwnIK = base.AIK as IK3D;

        this.Transition = this.myOwnIK.Transition;
    }
}