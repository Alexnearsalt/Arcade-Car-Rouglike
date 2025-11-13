using UnityEngine;
using UnityEngine.InputSystem;

public class CarInput : MonoBehaviour, CarButtons.ICarActions
{
    [SerializeField] private CarController carController;

    private CarButtons _input;
    private CarButtons.CarActions _carActions;

    private void Awake()
    {
        if (carController == null)
            carController = GetComponent<CarController>();
        
        _input = new CarButtons();
        _carActions = _input.Car;
        _carActions.AddCallbacks(this);
    }

    private void OnEnable()
    {
        _carActions.Enable();
    }

    private void OnDisable()
    {
        _carActions.Disable();
    }

    private void OnDestroy()
    {
        _input.Dispose();
    }

    public void OnStartCar(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            carController?.ToggleEngine();
        }
    }

    public void OnSteer(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        carController?.SetSteer(value);
    }

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        carController?.SetAcceleration(value);
    }

    public void OnBrake(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        carController?.SetBrake(value);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnHandbrake(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        carController?.SetHandbrake(value);
    }

    public void OnDownShift(InputAction.CallbackContext context)
    {
        if (context.performed)
            carController?.ShiftDown();
    }

    public void OnUpShift(InputAction.CallbackContext context)
    {
        if (context.performed)
            carController?.ShiftUp();
    }

    public void OnLeftTurnSignal(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnRightTurnSignal(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnHazardLights(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnLowBeamLight(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnHighBeamLight(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSwitchCamera(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnRestart(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
