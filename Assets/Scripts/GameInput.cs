using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteraction;
    public event EventHandler OnOperateInteraction;

    private GameControl gameControl;
    private void Awake()
    {
        gameControl=new GameControl();
        gameControl.Player.Enable();
        gameControl.Player.Interact.performed += Interact_Performed;
        gameControl.Player.Operate.performed += Operate_Performed;
    }

    private void Operate_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnOperateInteraction?.Invoke(this,EventArgs.Empty);
    }

    private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteraction?.Invoke(this,EventArgs.Empty);
    }

    /// <summary>
    /// 获取并单位化移动方向向量
    /// </summary>
    public Vector3 GetMovementDirectionNormalized()
    {
        Vector2 inputVector2= gameControl.Player.Move.ReadValue<Vector2>();
        Vector3 direction = new(inputVector2.x, 0, inputVector2.y);
        direction = direction.normalized;
        return direction;
    }
}
