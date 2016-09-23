using UnityEngine;
using System.Collections;

public static class Vector3Extension
{
    /// <summary>
    /// Calcul l'angle entre 2 vecteurs compris entre [-180 et 180].
    /// Signed signifie la valeur de l'angle de retour (négatif ou positif) alors qu'unsigned signifie que l'angle sera forcèment positif.
    /// </summary>
    /// <returns></returns>
    public static float SignedAngle(this Vector3 a, Vector3 b)
    {
        return Vector3.Angle(a, b) * GetSignOfAngle(a, b);
    }

    /// <summary>
    ///  Permet d'obtenir le signe de l'angle entre 2 vecteurs.
    /// </summary>
    /// <returns></returns>
    public static float GetSignOfAngle(this Vector3 a, Vector3 b)
    {
        return Mathf.Sign(Vector3.Dot(Vector3.back, Vector3.Cross(a, b)));
    }
}