using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public sealed class IKCCDFirstJoint : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private bool enableIK = true;
    [SerializeField]
    private Transform target = null, 
                      endTransform = null;

    private Transform myTransform = null;

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

    private void LateUpdate()
    {
        if (this.Validate())
        {
            if (!this.EnableIK)
                this.target.position = this.targetInitialPosition;

            this.IKCCDAlgorithm();
        }
    }
    #endregion

    #region Intern Behaviour
    /// <summary>
    /// Tourne les joints en partant du dernier joint jusqu'au premier.
    /// Ici, une optimisation est possible en stoquant tous les joints partant du dernier jusqu'au premier, cette méthode pourrait être appeler dans l'Awake.
    /// Ceci permettrait d'éviter de faire un transform.parent qui est un équivalent à un GetComponent<Transform> et par conséquent, il serai plus rapide
    /// de parcourir ce tableau de joints que le code ci-dessous.
    /// Je ne les pas fait de sorte à car ceci complixifierai le code et que je ne souhaitait te laisser que l'essentiel de mon code. (Cordialement).
    /// </summary>
    private void IKCCDAlgorithm()
    {
        Transform joint = this.endTransform.parent;

        while (true)
        {
            this.RotateJoint(joint);

            // Arrête la boucle lorsqu'il est arrivé au premier joint.
            if (joint == this.myTransform)
                break;

            joint = joint.parent;
        }
    }

    /// <summary>
    /// Détermine si les paramètres du scripts sont valides pour pouvoir traiter notre IK.
    /// </summary>
    /// <returns></returns>
    private bool Validate()
    {
        return null != target &&
               null != endTransform;
    }

    /// <summary>
    /// C'est la méthode principale de notre système d'IK.
    /// Elle permet de tourner chaque noeud en partant du dernier jusqu'au premier.
    /// </summary>
    /// <param name="jointToRotate"></param>
    private void RotateJoint(Transform jointToRotate)
    {
        Vector2 toTarget = target.position - jointToRotate.position;
        Vector2 toEnd = endTransform.position - jointToRotate.position; // itération 1 : dernier noeud, itération 2 : avant dernier noeud...

        // Calcul de l'angle du joint.
        float angle = toEnd.SignedAngle(toTarget); // Obtient un angle signé : compris entre [-180 et 180]
        if (this.DoesLocalScaleIsNegative())
            angle *= -1.0f; // Permet juste d'ajuster le sens de l'angle.
        angle = -(angle - jointToRotate.eulerAngles.z); // Ajuste l'angle en fonction de l'angle courant Z.

        // Met à jour l'angle.
        jointToRotate.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }

    /// <summary>
    /// Permet juste de déterminer
    /// </summary>
    /// <returns></returns>
    private bool DoesLocalScaleIsNegative()
    {
        return 0 > this.myTransform.localScale.x;
    }
    #endregion
}