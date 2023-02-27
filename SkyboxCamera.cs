using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SkyboxCamera : MonoBehaviour
{
    private Camera mainCamera;
    private Camera skyCam;

    [Range(0f, float.PositiveInfinity)]
    [Tooltip("Factor by which movement of the Skybox Camera will be scaled down.")]
    [SerializeField] private float scaleFactor = 10f;

    [Tooltip("If set, the skybox camera will not move on the X axis.")]
    [SerializeField] private bool clampXPos = false;

    [Tooltip("If set, the skybox camera will not move on the Y axis.")]
    [SerializeField] private bool clampYPos = false;

    [Tooltip("If set, the skybox camera will not move on the Z axis.")]
    [SerializeField] private bool clampZPos = false;



    private Vector3 lastMainPos;
    private Vector3 scaleVector;
    private float scaleMultiplier;
    private void Start() {

        //assign references
        mainCamera = Camera.main;
        skyCam = GetComponent<Camera>();

        //catch if the user forgot to tag the main camera
        if (mainCamera == null) {
            Debug.LogError("Could not find Main Camera. Ensure that it exists and is tagged 'MainCamera'.");
            this.gameObject.SetActive(false);
        }

        lastMainPos = Camera.main.transform.position;

        //sync camera position with main camera

        //Assign our local start position as the main camera's scaled down world position
        Vector3 startPos = new Vector3(
            lastMainPos.x / scaleFactor,
            lastMainPos.y / scaleFactor,
            lastMainPos.z / scaleFactor
        );

        this.transform.localPosition = startPos;
        this.transform.rotation = mainCamera.transform.rotation; //we do not need to scale rotation, so we can just copy it over

        scaleMultiplier = 1.0f / scaleFactor; 
        scaleVector = new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier);
    }

    private void Update() {

        //copy rotation from main camera
        this.transform.rotation = mainCamera.transform.rotation;

        if (clampXPos && clampYPos && clampZPos) {
            return;
        }

        //calculate deltas
        Vector3 delta = new Vector3(
            lastMainPos.x - mainCamera.transform.position.x,
            lastMainPos.y - mainCamera.transform.position.y,
            lastMainPos.z - mainCamera.transform.position.z
        );

        if (clampXPos) 
            delta.x = 0f;
        if (clampYPos)
            delta.y = 0f;
        if (clampZPos)
            delta.z = 0f;
            
        //scale the delta
        Vector3 scaledDelta = Vector3.Scale(delta, scaleVector);

        //move camera by scaled delta
        this.transform.localPosition = new Vector3(
            this.transform.localPosition.x + scaledDelta.x,
            this.transform.localPosition.y + scaledDelta.y,
            this.transform.localPosition.z + scaledDelta.z 
        );

        //reassign last position for main camera.
        lastMainPos = mainCamera.transform.position;
    }

    private void LateUpdate() {
        //keep field of view syncronized between both cameras.
        skyCam.fieldOfView = mainCamera.fieldOfView;
    }
}
