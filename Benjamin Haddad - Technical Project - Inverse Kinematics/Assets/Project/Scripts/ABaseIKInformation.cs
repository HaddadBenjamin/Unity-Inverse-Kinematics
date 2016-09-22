using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections;

public abstract class ABaseIKInformation : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Light directionalLight;

    private AIK aIK;
    private ThirdPersonCamera myCamera;

    private bool enableIK = true;
    public Action<bool> enableIKListener;

    private float cameraDistanceAway;
    private float cameraDistanceUp;
    private float cameraDistanceRight;
    private float lightIntensity;
    
    private readonly Rect beginArea = new Rect(10.0f, 10.0f, 700.0f, 500.0f);
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

            if (null != this.AIK)
                this.AIK.EnableIK = this.EnableIK;

            if (null != this.enableIKListener)
                this.enableIKListener(this.EnableIK);
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
                this.MyCamera.distanceAway = cameraDistanceAway;
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
                this.MyCamera.distanceUp = cameraDistanceUp;
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

    public float CameraDistanceRight
    {
        get
        {
            return cameraDistanceRight;
        }

        set
        {
            float tmp = cameraDistanceRight;

            cameraDistanceRight = value;

            if (tmp != cameraDistanceRight)
                this.MyCamera.distanceRight = cameraDistanceRight;
        }
    }

    public ThirdPersonCamera MyCamera
    {
        get
        {
            return myCamera;
        }

        set
        {
            myCamera = value;
        }
    }

    public AIK AIK
    {
        get
        {
            return aIK;
        }

        set
        {
            aIK = value;
        }
    }
    #endregion

    #region Virtual Behaviour
    protected virtual void GUIInformation()
    {
        this.EnableIK = GUILayout.Toggle(this.EnableIK, "Activer / désactiver l'IK.");

        GUILayout.BeginHorizontal();
        GUILayout.Label("Charger une scène.");
        if (GUILayout.Button("La scène courante"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (GUILayout.Button("IK des pieds"))
            SceneManager.LoadScene("Foot IK");
        if (GUILayout.Button("IK des mains"))
            SceneManager.LoadScene("Hand IK");
        if (GUILayout.Button("IK du regard"))
            SceneManager.LoadScene("Look IK");
        if (GUILayout.Button("IK 3D sans Animator"))
            SceneManager.LoadScene("IK System 3D");
        if (GUILayout.Button("IK 2D"))
            SceneManager.LoadScene("IK 2D Generic");
        GUILayout.EndHorizontal();

        GUILayout.Label("Afin de rendre plus visible le comportement des animations IK, vous pouvez modifier les paramètres ci-dessous.");

        if (null != this.myCamera)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Distance de la caméra par rapport au joueur.");
            this.CameraDistanceAway = GUILayout.HorizontalSlider(this.CameraDistanceAway, -20.0f, 20.0f);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Distance up de la caméra par rapport au joueur.");
            this.CameraDistanceUp = GUILayout.HorizontalSlider(this.CameraDistanceUp, 0.05f, 15.0f, GUILayout.Width(100.0f));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Distance right de la caméra par rapport au joueur.");
            this.CameraDistanceRight = GUILayout.HorizontalSlider(this.CameraDistanceRight, -10.0f, 10.0f, GUILayout.Width(100.0f));
            GUILayout.EndHorizontal();
        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("Intensité de la lumière.");
        this.LightIntensity = GUILayout.HorizontalSlider(this.LightIntensity, 0.0f, 2.0f);
        GUILayout.EndHorizontal();
    }
    #endregion

    #region Unity Behaviour
    void Awake()
    {
        this.AIK = GetComponent<AIK>();

        if (null != this.AIK)
            this.EnableIK = this.AIK.EnableIK;

        this.MyCamera = Camera.main.GetComponent<ThirdPersonCamera>();

        if (null != this.myCamera)
        {
            this.CameraDistanceAway = this.MyCamera.distanceAway;
            this.CameraDistanceUp = this.MyCamera.distanceUp;
            this.CameraDistanceRight = this.MyCamera.distanceRight;
        }

        this.LightIntensity = this.directionalLight.intensity;
    }

    void OnGUI()
    {
        GUILayout.BeginArea(this.beginArea, GUIContent.none);

        GUI.color = Color.green;
        this.GUIInformation();

        GUILayout.EndArea();
    }
    #endregion
}