using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadButton : MonoBehaviour
{

    public int ButtonValue;
    public Keypad Keypad;

    public void OnClick()
    {
        Keypad.RegisterButtonClick(ButtonValue);
    }
}
