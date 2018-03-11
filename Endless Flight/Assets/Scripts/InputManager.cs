using UnityEngine;
using XboxCtrlrInput;

public static class InputMapping {
    public static float MovementVerticalAxis(XboxController controller) {
        if(PCEnabled(controller)) {
            if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) {
                return 1f;
            }
            if(!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) {
                return -1f;
            }
        }
        if(Mathf.Abs(XCI.GetAxis(XboxAxis.LeftStickY,controller)) > 0.05) {
            return XCI.GetAxis(XboxAxis.LeftStickY,controller);
        }
        return 0;
    }

    public static float MovementHorizontalAxis(XboxController controller) {
        if(PCEnabled(controller)) {
            if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
                return -1f;
            }
            if(!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) {
                return 1f;
            }
        }
        if(Mathf.Abs(XCI.GetAxis(XboxAxis.LeftStickX,controller)) > 0.05) {
            return XCI.GetAxis(XboxAxis.LeftStickX,controller);
        }
        return 0f;
    }

    public static float TurnHorizontalAxis(XboxController controller) {
        if(PCEnabled(controller)) {
            if(Mathf.Abs(Input.GetAxis("Mouse X")) > 0.05) {
                return Input.GetAxis("Mouse X");
            }
        }
        if(Mathf.Abs(XCI.GetAxis(XboxAxis.RightStickX,controller)) > 0.05) {
            return XCI.GetAxis(XboxAxis.RightStickX,controller);
        }
        return 0;
    }

    public static float TurnVerticalAxis(XboxController controller) {
        if(PCEnabled(controller)) {
            if(Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.05) {
                return Input.GetAxis("Mouse Y");
            }
        }
        if(Mathf.Abs(XCI.GetAxis(XboxAxis.RightStickY,controller)) > 0.05) {
            return XCI.GetAxis(XboxAxis.RightStickY,controller);
        }
        return 0;
    }

    public static float Fire1(XboxController controller) {
        if((int)controller == 5) {
            if(Input.GetKeyDown(KeyCode.Mouse0)) {
                return 1f;
            }
        }
        if(XCI.GetAxis(XboxAxis.RightTrigger,controller) > 0f) {
            return XCI.GetAxis(XboxAxis.RightTrigger,controller);
        }
        return 0f;
    }

    private static bool PCEnabled(XboxController controller) {
        return ((int)controller == 5 || (int)controller == 0);
    }
}