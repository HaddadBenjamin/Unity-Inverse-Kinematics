using UnityEngine;
using System.Collections;

public class HandIKInformation : ABaseIKInformation
{
    #region Override Behaviour
    protected override void GUIInformation()
    {
        base.GUIInformation();

        GUILayout.Label("Activer / désactiver l'IK pour tenir l'objet en face de votre personnage.");
    }
    #endregion
}
