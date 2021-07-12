using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class InputLimiter : MonoBehaviour
{
    private InputField input;

    private void Start()
    {
        input = GetComponent<InputField>();
    }

    public void LimitGridSize(string value)
    {
        if (value != string.Empty)
        {
            int x = Convert.ToInt32(value);

            if (x < 2)
                input.text = "2";
            else if (x > 500)
                input.text = "500";
        }
    }

    public void LimitRate(string value)
    {
        if (value != string.Empty)
        {
            float x = Convert.ToSingle(value, CultureInfo.InvariantCulture);

            if (x < -1)
                input.text = "-1";
            else if (x > 1)
                input.text = "1";
        }
    }
}