using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class IKSolverBase : IKSolverAbst
{
    public bool EnableIK = true;
    public Transform target, endTransform;

    public IKController controller;
    private Vector3 targetInitialPosition;

    public void Start()
    {
        this.targetInitialPosition = this.target.position;
    }

    public void LateUpdate()
    {
        if (this.Validate())
        {
            if (!this.EnableIK)
                this.target.position = this.targetInitialPosition;

            Debug.Log(this.EnableIK);
            this.UpdateIK();
        }
    }

    public override void UpdateIK()
    {
        Transform node = endTransform.parent;

        while (true)
        {
            RotateNode(node);

            if (node == transform)
                break;

            node = node.parent;
        }
    }

    private bool Validate()
    {
        return null != target &&
               null != endTransform &&
               null != controller;
    }

    private void RotateNode(Transform node)
    {
        Vector2 toTarget = target.position - node.position;
        Vector2 toEnd = endTransform.position - node.position; // itération 1 : dernier noeud, itération 2 : avant dernier noeud...
        float angle = SignedAngle(toEnd, toTarget); // Obtient un angle compris entre [-180 et 180]

        if (controller.Inverse)
            angle *= -1.0f; // Ajuste l'angle au controlleur.

        angle = -(angle - node.eulerAngles.z); // Ajuste l'angle en fonction de l'angle courant Z.
        node.rotation = Quaternion.Euler(0.0f, 0.0f, angle); // Met à jour l'angle.
    }
}