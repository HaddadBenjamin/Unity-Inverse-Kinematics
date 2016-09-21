using UnityEngine;
using System;
  
[RequireComponent(typeof(Animator))]  
[RequireComponent(typeof(CapsuleCollider))]  
public sealed class FootIK : AIK
{
    #region Fields
    public LayerMask raycastLayerMask;

    public Transform leftFoot;
    public Vector3 leftFootOffset;
    private float leftFootWeight = 0.0f;

    public Transform rightFoot;
    public Vector3 rightFootOffset;
    private float rightFootWeight = 0.0f;
	
	private Vector3 leftFootPosition;
	private Vector3 rightFootPosition;

    private CapsuleCollider myCapsuleCollider;
	private Vector3 newColliderCenter;
	private float newColliderHeight;

    private float transformWeigth = 1.0f;
    private const float moveColliderCenterAndHeightSpeedAndTransformWeightSpeed = 10;
    #endregion

    #region Unity Behaviour
    void Start () 
	{
        this.myCapsuleCollider = GetComponent<CapsuleCollider>();

		this.newColliderCenter = this.myCapsuleCollider.center;
        this.newColliderHeight = this.myCapsuleCollider.height;
	}

    void Update()
    {
        if (EnableIK)
        {
            if (base.MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Idle"))
                this.IdleUpdateCollider();
            else if (base.MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Walk") ||
                     base.MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Run"))
                this.WalkRunUpdateCollider();
        }
        else
        {
            this.myCapsuleCollider.center = new Vector3(
                0.0f,
                Mathf.Lerp(this.myCapsuleCollider.center.y, this.newColliderCenter.y, Time.deltaTime * moveColliderCenterAndHeightSpeedAndTransformWeightSpeed), 
                0.0f);

            this.myCapsuleCollider.height = Mathf.Lerp(
                this.myCapsuleCollider.height, 
                this.newColliderHeight, 
                Time.deltaTime * moveColliderCenterAndHeightSpeedAndTransformWeightSpeed);
        }
    }
    #endregion

    #region Override Behaviour
    protected override void IKBehaviour()
    {
        if (this.transformWeigth != 1.0f)
        {
            this.transformWeigth = Mathf.Lerp(this.transformWeigth, 1.0f, Time.deltaTime * moveColliderCenterAndHeightSpeedAndTransformWeightSpeed);

            if (this.transformWeigth >= 0.99)
                this.transformWeigth = 1.0f;
        }
        if (base.MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Idle") &&
            this.myCapsuleCollider.attachedRigidbody.velocity.magnitude < 0.1f)
        {
            base.MyAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, this.transformWeigth);
            base.MyAnimator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, this.transformWeigth);
            base.MyAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot, this.transformWeigth);
            base.MyAnimator.SetIKRotationWeight(AvatarIKGoal.RightFoot, this.transformWeigth);

            this.IdleIK();
        }
        else if (base.MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Walk") ||
                 base.MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Run"))
        {
            base.MyAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, this.transformWeigth * this.leftFootWeight);
            base.MyAnimator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, this.transformWeigth * this.leftFootWeight);
            base.MyAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot, this.transformWeigth * this.rightFootWeight);
            base.MyAnimator.SetIKRotationWeight(AvatarIKGoal.RightFoot, this.transformWeigth * this.rightFootWeight);

            this.WalkRunIK();
        }
    }

    protected override void ResetIK()
    {
        if (this.transformWeigth != 0.0f)
        {
            this.transformWeigth = Mathf.Lerp(this.transformWeigth, 0.0f, Time.deltaTime * moveColliderCenterAndHeightSpeedAndTransformWeightSpeed);

            if (this.transformWeigth <= 0.01)
                this.transformWeigth = 0.0f;
        }

        base.MyAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, this.transformWeigth);
        base.MyAnimator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, this.transformWeigth);
        base.MyAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot, this.transformWeigth);
        base.MyAnimator.SetIKRotationWeight(AvatarIKGoal.RightFoot, this.transformWeigth);
    }
    #endregion

    #region Intern Behaviour
    private void IdleIK()
    {
		RaycastHit hit;

        this.leftFootPosition = base.MyAnimator.GetIKPosition(AvatarIKGoal.LeftFoot);

		if (Physics.Raycast(this.leftFootPosition + Vector3.up, Vector3.down, out hit, 2.0f, this.raycastLayerMask))
		{
			//Debug.DrawLine(hit.point, hit.point + hit.normal, Color.yellow);

			base.MyAnimator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + this.leftFootOffset);
			base.MyAnimator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(Vector3.ProjectOnPlane(this.leftFoot.forward, hit.normal),  hit.normal));

            this.leftFootPosition = hit.point;
		}

        this.rightFootPosition = base.MyAnimator.GetIKPosition(AvatarIKGoal.RightFoot);

		if (Physics.Raycast(this.rightFootPosition + Vector3.up, Vector3.down, out hit, 2.0f, this.raycastLayerMask))
		{
			//Debug.DrawLine(hit.point, hit.point + hit.normal, Color.green);

			base.MyAnimator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + this.rightFootOffset);
			base.MyAnimator.SetIKRotation(AvatarIKGoal.RightFoot,Quaternion.LookRotation(Vector3.ProjectOnPlane(this.rightFoot.forward, hit.normal),  hit.normal));

            this.rightFootPosition = hit.point;
		}
	}

	private void WalkRunIK()
    {
		RaycastHit hit;

        this.leftFootPosition = base.MyAnimator.GetIKPosition(AvatarIKGoal.LeftFoot);

		if (Physics.Raycast(this.leftFootPosition + Vector3.up, Vector3.down, out hit, 2.0f, this.raycastLayerMask))
		{
			//Debug.DrawLine(hit.point, hit.point + hit.normal, Color.yellow);

			base.MyAnimator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + this.leftFootOffset);
			base.MyAnimator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(Vector3.ProjectOnPlane(this.leftFoot.forward, hit.normal),  hit.normal));

            this.leftFootPosition = hit.point;
		}

        this.rightFootPosition = base.MyAnimator.GetIKPosition(AvatarIKGoal.RightFoot);

		if (Physics.Raycast(this.rightFootPosition + Vector3.up, Vector3.down, out hit, 2.0f, this.raycastLayerMask))
		{
			//Debug.DrawLine(hit.point, hit.point + hit.normal, Color.green);

			base.MyAnimator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + this.rightFootOffset);
			base.MyAnimator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(Vector3.ProjectOnPlane(this.rightFoot.forward, hit.normal),  hit.normal));

			rightFootPosition = hit.point;
		}				
	}
	
	void IdleUpdateCollider () 
	{	
		float dif = this.leftFootPosition.y - this.rightFootPosition.y;

		if (dif < 0.0f)
            dif *= -1.0f;

		this.myCapsuleCollider.center = new Vector3(0.0f, Mathf.Lerp(this.myCapsuleCollider.center.y, this.newColliderCenter.y + dif, Time.deltaTime) ,0.0f);
        this.myCapsuleCollider.height = Mathf.Lerp(this.myCapsuleCollider.height, this.newColliderHeight - (dif / 2), Time.deltaTime);
	}

	void WalkRunUpdateCollider () 
	{
		RaycastHit hit;
		Vector3 myGround = Vector3.zero;
		Vector3 dif = Vector3.zero;

		if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 3.0f, this.raycastLayerMask))
			myGround = hit.point;
		if (Physics.Raycast(transform.position + 
            (((transform.forward * (this.myCapsuleCollider.radius))) + 
            (this.myCapsuleCollider.attachedRigidbody.velocity * Time.deltaTime)) + 
            Vector3.up, Vector3.down, out hit, 2.0f, this.raycastLayerMask))
		{
            //Debug.DrawLine(transform.position + (((transform.forward * (myCapsuleCollider.radius))) + (myCapsuleCollider.attachedRigidbody.velocity * Time.deltaTime)) + Vector3.up, hit.point, Color.red);

            dif = hit.point - myGround;

			if (dif.y < 0.0f)
                dif *= -1.0f;
		}
		if (dif.y < 0.5f)
        {
			this.myCapsuleCollider.center = new Vector3(0.0f, Mathf.Lerp(this.myCapsuleCollider.center.y, this.newColliderCenter.y + dif.y, Time.deltaTime * moveColliderCenterAndHeightSpeedAndTransformWeightSpeed), 0.0f);
            this.myCapsuleCollider.height = Mathf.Lerp(this.myCapsuleCollider.height, this.newColliderHeight - (dif.y / 2.0f), Time.deltaTime * moveColliderCenterAndHeightSpeedAndTransformWeightSpeed);
		}
        else
        {
			this.myCapsuleCollider.center = new Vector3(0.0f, Mathf.Lerp(this.myCapsuleCollider.center.y, this.newColliderCenter.y, Time.deltaTime * moveColliderCenterAndHeightSpeedAndTransformWeightSpeed), 0.0f);
            this.myCapsuleCollider.height = Mathf.Lerp(this.myCapsuleCollider.height, this.newColliderHeight, Time.deltaTime * moveColliderCenterAndHeightSpeedAndTransformWeightSpeed);
		}
	}
    #endregion
}