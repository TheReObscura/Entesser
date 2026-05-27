
using NUnit.Framework.Internal.Commands;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class GameInput : MonoBehaviour
{

    private PlayerInput playerInput;
    public static GameInput instance { get; private set; }

    private void Awake()
    {
        instance = this;
        playerInput = new PlayerInput();
        playerInput.Enable();
    }
    public void EnableGameplayInput() => playerInput.Enable();
    public void DisableGameplayInput() => playerInput.Disable();
    public Vector2 GetMovementVector() => playerInput.Player.Move.ReadValue<Vector2>();
    public bool IsRunning() => playerInput.Player.Sprint.IsPressed();
    public bool IsCrouching() => playerInput.Player.Crouch.IsPressed();
    public bool IsDashing() => playerInput.Player.Dash.WasPressedThisFrame();
    public bool IsFireActive() => playerInput.Player.Fire.WasPressedThisFrame();
    public bool IsBookActive() => playerInput.Player.Book.WasPressedThisFrame();
    public bool IsSphereActive() => playerInput.Player.Sphere.WasPressedThisFrame();
    public bool IsDebugXP() => playerInput.Player.DebugXP.WasPressedThisFrame();
    public bool IsInteract() => playerInput.Player.Interact.WasPressedThisFrame();
    public bool IsDebugSpawn() =>playerInput.Player.DebugSpawn.WasPressedThisFrame();
    public Vector2 GetScroll()
    {
        return playerInput.Player.Scroll.ReadValue<Vector2>();
    }

}