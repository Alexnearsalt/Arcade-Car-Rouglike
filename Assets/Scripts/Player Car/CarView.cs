using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarView : MonoBehaviour
{
    [SerializeField] private CarController carController;

    [Header("UI")]
    [SerializeField] private TMP_Text currentGearTMP_UI;
    [SerializeField] private TMP_Text currentGearTMP_Dashboard;
    [SerializeField] private TMP_Text currentSpeedTMP_UI;
    [SerializeField] private TMP_Text currentSpeedTMP_Dashboard;
    [SerializeField] private Slider accelerationSlider;

    private void Awake()
    {
        if (carController == null)
            carController = FindObjectOfType<CarController>();
    }

    private void OnEnable()
    {
        if (carController == null) return;

        var model = carController.Model;
        model.OnGearChanged += HandleGearChanged;
        model.OnSpeedChanged += HandleSpeedChanged;
        model.OnAccelerationInputChanged += HandleAccelerationChanged;
    }

    private void OnDisable()
    {
        if (carController == null) return;

        var model = carController.Model;
        model.OnGearChanged -= HandleGearChanged;
        model.OnSpeedChanged -= HandleSpeedChanged;
        model.OnAccelerationInputChanged -= HandleAccelerationChanged;
    }

    private void HandleGearChanged(Enums.AutomaticGears gear)
    {
        string text = gear switch
        {
            Enums.AutomaticGears.Reverse => "R",
            Enums.AutomaticGears.Neutral => "N",
            Enums.AutomaticGears.Drive => "D",
            _ => "?"
        };

        if (currentGearTMP_UI != null) currentGearTMP_UI.text = text;
        if (currentGearTMP_Dashboard != null) currentGearTMP_Dashboard.text = text;
    }

    private void HandleSpeedChanged(float speed)
    {
        float absSpeed = Mathf.Abs(speed);
        string s = absSpeed.ToString("F0");

        if (currentSpeedTMP_UI != null) currentSpeedTMP_UI.text = s;
        if (currentSpeedTMP_Dashboard != null) currentSpeedTMP_Dashboard.text = s;
    }

    private void HandleAccelerationChanged(float acc)
    {
        if (accelerationSlider == null) return;

        bool gearActive =
            carController.Model.CurrentGear == Enums.AutomaticGears.Drive ||
            carController.Model.CurrentGear == Enums.AutomaticGears.Reverse;

        if (gearActive)
        {
            accelerationSlider.value =
                Mathf.Lerp(accelerationSlider.value, acc, Time.deltaTime * 15f);
        }
        else
        {
            accelerationSlider.value = 0f;
        }
    }
}
