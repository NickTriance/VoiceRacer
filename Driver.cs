using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] private GameObject car;
    [SerializeField] private float steerStep;
    [SerializeField] private float throttleStep;
    [SerializeField] private float brakeStep;

    private CarController carController;

    private float currentThrottle = 0f;
    private float currentBrake = 0f;
    private float currentSteer = 0f;
    private bool reverse;


    private void Start() {
        carController = car.GetComponent<CarController>();
        reverse = false;
    }

    private void Update() {
        ApplyInput();
    }

    public void Accelerate() {

        currentThrottle+=throttleStep;
        currentBrake = 0f;

        if (currentThrottle > 1.0f) {
            currentThrottle = 1.0f;
        }
    }

    public void Brake() {
        currentBrake+=brakeStep;
        currentThrottle = 0f;

        if (currentBrake > 1.0f) {
            currentBrake = 1.0f;
        }
    }

    public void Stop() {
        currentBrake = 1.0f;
    }

    public void Steer(Constants.DIRECTIONS dir) {
        switch(dir) {
            case Constants.DIRECTIONS.STRAIGHT:
                currentSteer = 0f;
                break;
            case Constants.DIRECTIONS.LEFT:
                currentSteer-=steerStep;
                break;
            case Constants.DIRECTIONS.RIGHT:
                currentSteer+=steerStep;
                break;
        }

        currentSteer = Mathf.Clamp(currentSteer, -1.0f, 1.0f);
    }

    public void Lift() {
        if (currentThrottle > 0f) {
            currentThrottle-=throttleStep;
        } else if (currentBrake > 0f) {
            currentBrake-=brakeStep;
        }
    }

    public void Coast() {
        currentThrottle = 0f;
        currentBrake = 0f;
    }

    public void Forward() {
        reverse = false;
        Accelerate();
    }
    
    public void Reverse() {
        reverse = true;
        Accelerate();
    }

    public void Jump() {
        carController.Jump();
    }

    public void Handbrake() {
        Stop();
    }

    public void Honk() {
        AudioManager.instance.Play("Horn");
    }

    private void ApplyInput() {
        
        float throttleInput = currentThrottle;
        if (reverse) {
            throttleInput*=-1;
        }
        carController.SetInput(throttleInput, currentBrake, currentSteer);
    }
}
