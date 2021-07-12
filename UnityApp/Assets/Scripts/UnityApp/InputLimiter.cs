using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Limits the inputs
/// </summary>
public class InputLimiter : MonoBehaviour
{
    /// Input field to limit
    private InputField input;

    /// <summary>
    /// Start method
    /// </summary>
    private void Start()
    {
        /// Assigns the input field
        input = GetComponent<InputField>();
    }

    /// <summary>
    /// Limits the grid size based on the input given
    /// </summary>
    /// <param name="value">Value to limit</param>
    public void LimitGridSize(string value)
    {
        /// If the input is not empty, check and limit within limits
        if (value != string.Empty)
        {
            int x = Convert.ToInt32(value);

            if (x < 2)
                input.text = "2";
            else if (x > 500)
                input.text = "500";
        }
    }

    /// <summary>
    /// Limits the event rate based on the input given
    /// </summary>
    /// <param name="value">Value to limit</param>
    public void LimitRate(string value)
    {
        /// If the input is not empty, check and limit within limits
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