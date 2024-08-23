using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public enum Mode
    {
        LookAt,LookAtInverted,CamerForward, CamerForwardInverted
    }
    [SerializeField]private Mode mode;

    /// <summary>
    /// ����UI������������ַ�ʽ
    /// </summary>
    void Update()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
               break;
            case Mode.LookAtInverted:
                transform.LookAt(transform.position - Camera.main.transform.position + transform.position);
                break;
            case Mode.CamerForward:
                transform.forward= Camera.main.transform.forward;
                break;
            case Mode.CamerForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
            default:
                break;

        }
    }
}
