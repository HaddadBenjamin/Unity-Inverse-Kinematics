using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public sealed class IKFirstNode : AIKSolver
{
    #region Fields
    [SerializeField]
    private bool enableIK = true;
    [SerializeField]
    private IKController controller;
    [SerializeField]
    private Transform target, endTransform;

    private Transform myTransform;

    private Vector3 targetInitialPosition;
    #endregion

    #region Properties
    public bool EnableIK
    {
        get
        {
            return enableIK;
        }

        set
        {
            enableIK = value;
        }
    }
    #endregion

    #region Unity Behaviour
    private void Start()
    {
        this.myTransform = transform;

        this.targetInitialPosition = this.target.position;
    }

    public void LateUpdate()
    {
        if (this.Validate())
        {
            if (!this.EnableIK)
                this.target.position = this.targetInitialPosition;

            this.UpdateIK();
        }
    }
    #endregion

    #region Intern Behaviour
    /// <summary>
    /// Tourne les noeuds en partant du endTransform jusqu'au premier node.
    /// </summary>
    public override void UpdateIK()
    {
        Transform node = this.endTransform.parent;

        while (true)
        {
            this.RotateNode(node);

            if (node == this.myTransform)
                break;

            node = node.parent;
        }
    }

    /// <summary>
    /// Détermine si les paramètres du scripts sont valides pour pouvoir traiter notre IK.
    /// </summary>
    /// <returns></returns>
    private bool Validate()
    {
        return null != target &&
               null != endTransform &&
               null != controller;
    }

    /// <summary>
    /// C'est la méthode principale de notre système d'IK.
    /// Elle permet de tourner chaque noeud en partant du dernier jusqu'au premier.
    /// </summary>
    /// <param name="node"></param>
    private void RotateNode(Transform node)
    {
        Vector2 toTarget = target.position - node.position;
        Vector2 toEnd = endTransform.position - node.position; // itération 1 : dernier noeud, itération 2 : avant dernier noeud...
        float angle = MathHelper.SignedAngle(toEnd, toTarget); // Obtient un angle compris entre [-180 et 180]

        if (controller.IsNegativeLocalScale)
            angle *= -1.0f; // Ajuste l'angle au controlleur.

        angle = -(angle - node.eulerAngles.z); // Ajuste l'angle en fonction de l'angle courant Z.
        node.rotation = Quaternion.Euler(0.0f, 0.0f, angle); // Met à jour l'angle.
    }
    #endregion
}