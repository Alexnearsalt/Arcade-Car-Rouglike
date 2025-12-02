using UnityEngine;
using UnityEngine.InputSystem;

public class ESCMenuInput : MonoBehaviour, GameActions.IUIActions
{
    [SerializeField] private GameObject escMenu;

    private GameActions _input;
    private bool _isEscMenuActive;

    private void Awake()
    {
        _input = new GameActions();
        _input.UI.AddCallbacks(this);
        _input.UI.Enable();
        
        if (escMenu == null)
            escMenu = gameObject;

        escMenu.SetActive(false);
        _isEscMenuActive = false;
    }

    private void OnDestroy()
    {
        _input.Dispose();
    }

    public void OnOpenMenu(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        _isEscMenuActive = !_isEscMenuActive;
        escMenu.SetActive(_isEscMenuActive);
    }
}