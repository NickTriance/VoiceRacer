using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float brakeInput;
    [SerializeField] private bool isBrakeing;
    private bool isHandbrake;
    [SerializeField] private float currentBrakeForce;
    private float currentSteerAngle;

    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForceMultiplier; 
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private bool manualInput;
    [SerializeField] private WheelCollider frontLeftCollider;
    [SerializeField] private WheelCollider frontRightCollider;
    [SerializeField] private WheelCollider rearLeftCollider;
    [SerializeField] private WheelCollider rearRightCollider;
    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform rearLeftTransform;
    [SerializeField] private Transform rearRightTransform;
    [SerializeField] private Vector3 com;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private GameObject brakeLight;
    [SerializeField] private int color_id;
    [SerializeField] private GameObject[] car_colors;
    [SerializeField] private float jumpForce;
    private GameObject carBody; 
    
    private float currentSpeed = 0f;
    private float topSpeed = 100f;
    private float pitch;



    private void Start() {
        
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com; //adjust the car's center of mass

        AudioManager.instance.Play("Engine Run");
        carBody = Instantiate(car_colors[GameManager.instance.prefs.CAR_COLOR_ID], this.transform.position, this.transform.rotation);
        carBody.transform.parent = this.transform;
    }

    private void FixedUpdate() {
        
        //dynamically generate pitch for engine audio
        currentSpeed = rb.velocity.magnitude;
        pitch = Mathf.Lerp(0.15f, 1f, currentSpeed/topSpeed);
        AudioManager.instance.AdjustEnginePitch(pitch);


        HandleMotor();
        HandleSteering();
        UpdateVisuals();

        if (manualInput) {
            GetInput();
        }
    }

    public void SetInput(float throttle, float brake, float steer) {

        //We don't want voice control to mess with manual inputs, so we'll just return out
        if (manualInput) {
            return;
        }

        verticalInput = throttle - brake; //combine steering and throttle into one input
        horizontalInput = steer;
        brakeInput = brake;

        isBrakeing = brake > 0;
    }

    public void Jump() {
        rb.AddForce(Vector3.up * jumpForce);
        AudioManager.instance.Play("Boing");
    }

    private void GetInput() {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBrakeing = Input.GetButton("Brake");
        isHandbrake = Input.GetButton("Handbrake");
        if (isBrakeing) {
            brakeInput = 1.0f;
        }
    }

    private void HandleMotor() {  
        rearLeftCollider.motorTorque = verticalInput * motorForce;
        rearRightCollider.motorTorque = verticalInput * motorForce;
        frontLeftCollider.motorTorque = verticalInput * motorForce;
        frontRightCollider.motorTorque = verticalInput * motorForce;
        currentBrakeForce = isBrakeing ? brakeInput : 0f;
        ApplyBreaking();       
    }

    private void ApplyBreaking() {
        currentBrakeForce*=brakeForceMultiplier;
        frontRightCollider.brakeTorque = currentBrakeForce;
        frontLeftCollider.brakeTorque = currentBrakeForce;
        rearLeftCollider.brakeTorque = currentBrakeForce;
        rearRightCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering() {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftCollider.steerAngle = currentSteerAngle;
        frontRightCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateVisuals() {
        UpdateWheels();
        brakeLight.SetActive(isBrakeing);
        if (isBrakeing) {
            carBody.GetComponent<CarBodyBrakelightController>().EnableBL();
        } else {
            carBody.GetComponent<CarBodyBrakelightController>().DisableBL();
        }
    }

    private void UpdateWheels() {
        UpdateSingleWheel(frontLeftCollider, frontLeftTransform);
        UpdateSingleWheel(frontRightCollider, frontRightTransform);
        UpdateSingleWheel(rearLeftCollider, rearLeftTransform);
        UpdateSingleWheel(rearRightCollider, rearRightTransform);
    }

    private void UpdateSingleWheel(WheelCollider collider, Transform trans) {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        trans.position = pos;
        trans.rotation = rot;
    }

    private void OnDisable() {
        AudioManager.instance.StopSound("Engine Run");
    }
}

