using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections;

public abstract class ABaseIKInformation : MonoBehaviour
{
    #region Fields
    private ThirdPersonCamera myCamera;
    private bool enableIK = true;
    public Action<bool> EnableIKListener;
    private float cameraDistanceAway;
    private float cameraDistanceUp;
    private float lightIntensity;
    [SerializeField]
    private Light directionalLight;
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

            if (null != this.EnableIKListener)
                this.EnableIKListener(this.EnableIK);
        }
    }

    public float CameraDistanceAway
    {
        get
        {
            return cameraDistanceAway;
        }

        set
        {
            float tmp = CameraDistanceAway;

            cameraDistanceAway = value;

            if (tmp != cameraDistanceAway)
                this.myCamera.distanceAway = cameraDistanceAway;
        }
    }

    public float CameraDistanceUp
    {
        get
        {
            return cameraDistanceUp;
        }

        set
        {
            float tmp = cameraDistanceUp;

            cameraDistanceUp = value;

            if (tmp != cameraDistanceUp)
                this.myCamera.distanceUp = cameraDistanceUp;
        }
    }

    public float LightIntensity
    {
        get
        {
            return lightIntensity;
        }

        set
        {
            float tmp = lightIntensity;

            lightIntensity = value;

            if (lightIntensity != tmp)
                directionalLight.intensity = lightIntensity;
        }
    }
    #endregion

    #region Virtual Behaviour
    protected virtual void GUIInformation()
    {
        this.EnableIK = GUILayout.Toggle(this.EnableIK, "Activer / désactiver l'IK.");

        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("Recharger la scène courante"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (GUILayout.Button("Foot IK"))
            SceneManager.LoadScene("Foot IK");
        if (GUILayout.Button("Hand IK <b>BUILD SETTINGS MISSING</b>"))
            SceneManager.LoadScene("Hand IK");
        if (GUILayout.Button("Look IK <b>BUILD SETTINGS MISSING</b>"))
            SceneManager.LoadScene("Look IK");
        GUILayout.EndHorizontal();

        GUILayout.Label("Afin de le rendre plus visible vous pouvez modifier les paramètres de la caméra ci-dessous.");

        GUILayout.BeginHorizontal();
        GUILayout.Label("Distance de la caméra par rapport au joueur.");
        this.CameraDistanceAway = GUILayout.HorizontalSlider(this.CameraDistanceAway, -20.0f, 20.0f);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Distance up de la caméra par rapport au joueur.");
        this.CameraDistanceUp = GUILayout.HorizontalSlider(this.CameraDistanceUp, 0.05f, 15.0f);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Intensité de la lumière, ce paramètre peut vous permettre de mieux voir les effets de l'IK.");
        this.LightIntensity = GUILayout.HorizontalSlider(this.LightIntensity, 0.0f, 8.0f);
        GUILayout.EndHorizontal();
    } 
    #endregion

    #region Unity Behaviour

    void OnGUI()
    {
        this.GUIInformation();
    }
    #endregion

    #region Protected Behaviour
    protected void Initialize()
    {
        // Permet d'appeler son listener si il y en a.
        this.EnableIK = this.EnableIK;

        this.myCamera = Camera.main.GetComponent<ThirdPersonCamera>();

        this.CameraDistanceAway = this.myCamera.distanceAway;
        this.CameraDistanceUp = this.myCamera.distanceUp;
        this.LightIntensity = this.directionalLight.intensity;
    }
    #endregion
}
