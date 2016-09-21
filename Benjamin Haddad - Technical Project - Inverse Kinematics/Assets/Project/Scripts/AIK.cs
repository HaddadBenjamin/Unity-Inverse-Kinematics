using UnityEngine;
using System.Collections;

public abstract class AIK : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private bool enableIK = true;
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
    void OnAnimatorIK()
    {
        if (this.EnableIK)
            this.IKBehaviour();
        else
            this.ResetIK();
    }
    #endregion

    #region Abstract Behaviour
    /// <summary>
    /// Comportement de l'IK.
    /// </summary>
    protected abstract void IKBehaviour();

    /// <summary>
    /// Permet de réinitialiser le modèle de sorte qu'il n'utilise plus son IK.
    /// </summary>
    protected abstract void ResetIK();
    #endregion
}
