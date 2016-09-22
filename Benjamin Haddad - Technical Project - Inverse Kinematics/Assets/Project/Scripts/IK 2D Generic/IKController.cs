using UnityEngine;
using System.Collections;

public class IKController : MonoBehaviour
{
    public IKSolverAbst[] chains;

    // Ces 2 méthodes devrait stoquer et utiliser le transform.
    // Détermine juste si l'objet se situe à gauche ou à droite
    public bool Inverse
    {
        get
        {
            return 0 > transform.localScale.x;
        }
    }

    public Vector3 Position
    {
        get
        {
            return transform.localPosition;
        }
    }
}