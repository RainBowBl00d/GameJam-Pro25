using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DodgeGameHelper : MonoBehaviour
{
    [SerializeField] DodgeGame dodgeGame; // Reference to your game script
    [SerializeField] TMP_InputField field1, field2, field3, field5, field6; // Input fields
    [SerializeField] Slider field4;

    public float rOF, bulletSpeed, time, sineWeight, sineFrequency, sineAmplitude;

    // Assign rate of fire (rOF) from input field
    public void SetROF()
    {
        if (float.TryParse(field1.text, out float value))
        {
            rOF = value;
        }
        else
        {
            Debug.LogError("Invalid input for rOF!");
        }
    }

    // Assign bullet speed from input field
    public void SetBulletSpeed()
    {
        if (float.TryParse(field2.text, out float value))
        {
            bulletSpeed = value;
        }
        else
        {
            Debug.LogError("Invalid input for bullet speed!");
        }
    }

    // Assign time from input field
    public void SetTime()
    {
        if (float.TryParse(field3.text, out float value))
        {
            time = value;
        }
        else
        {
            Debug.LogError("Invalid input for time!");
        }
    }

    // Assign sine weight from input field
    public void SetSineWeight()
    {

        sineWeight = field4.value;

    }

    // Assign sine frequency from input field
    public void SetSineFrequency()
    {
        if (float.TryParse(field5.text, out float value))
        {
            sineFrequency = value;
        }
        else
        {
            Debug.LogError("Invalid input for sine frequency!");
        }
    }

    // Assign sine amplitude (hardcoded example)
    public void SetSineAmp()
    {
        if (float.TryParse(field5.text, out float value))
        {
            sineAmplitude = value;
        }
        else
        {
            Debug.LogError("Invalid input for sine Amp!");
        }
    }

    // Start the game with the current parameters
    public void Play()
    {
        Debug.Log("Game Started");
        //dodgeGame.GenerateGame(rOF, bulletSpeed, time, sineWeight, sineFrequency, sineAmplitude);
    }
}
