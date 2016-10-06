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
    
    private readonly Rect beginArea = new Rect(10.0f, 10.0f, 900.0f, 800.0f);
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
        this.EnableIK = GUILayout.Toggle(this.EnableIK, "Enable / Disable IK.");

        GUILayout.BeginHorizontal();
        GUILayout.Label("Load scene.");
        if (GUILayout.Button("Current scene"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (GUILayout.Button("Foot IK"))
            SceneManager.LoadScene("Foot IK");
        if (GUILayout.Button("Hand IK"))
            SceneManager.LoadScene("Hand IK");
        if (GUILayout.Button("Look IK"))
            SceneManager.LoadScene("Look IK");
        if (GUILayout.Button("3D IK without animator"))
            SceneManager.LoadScene("IK System 3D");
        if (GUILayout.Button("IK Cyclic Cordinate Descent 2D"))
            SceneManager.LoadScene("IK Cyclic Cordinate Descent 2D");
        if (GUILayout.Button("Analytic Two-Bone IK in 2D"))
            SceneManager.LoadScene("Analytic Two-Bone IK in 2D");
        GUILayout.EndHorizontal();

        GUILayout.Label("If you want to see better the behaviour of IK animations you should modify the following parameters.");

        if (null != this.myCamera)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Camera distance from player.");
            this.CameraDistanceAway = GUILayout.HorizontalSlider(this.CameraDistanceAway, -20.0f, 20.0f);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Camera up distance from player.");
            this.CameraDistanceUp = GUILayout.HorizontalSlider(this.CameraDistanceUp, 0.05f, 15.0f, GUILayout.Width(100.0f));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Camera right distance from player.");
            this.CameraDistanceRight = GUILayout.HorizontalSlider(this.CameraDistanceRight, -10.0f, 10.0f, GUILayout.Width(100.0f));
            GUILayout.EndHorizontal();
        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("Light intensity.");
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