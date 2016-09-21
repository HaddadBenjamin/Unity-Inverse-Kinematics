using UnityEngine;
using System.Collections;

public class HandIKInformation : ABaseIKInformation
{
    #region Unity Behaviour
    void Awake()
    {
        base.Initialize();
    }
    #endregion

    #region Override Behaviour
    protected override void GUIInformation()
    {
        base.GUIInformation();

        GUILayout.Label("Activer / désactiver l'IK pour tenir l'objet en face de votre personnage.");
    }
    #endregion
}
