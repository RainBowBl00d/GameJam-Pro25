using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LoadSettings : MonoBehaviour
{
    public TMP_InputField rr;
    public TMP_InputField pc;
    public TMP_Dropdown menu;
    public KeyNotesGame game;
    private void Start()
    {
        rr.text = "2";
        pc.text = "10";
    }

    public void StartGame()
    {
        float release = float.Parse(rr.text);
        int count = int.Parse(pc.text);
//        game.GenerateMiniGame(count, menu.value < 2, menu.value % 2 == 1, release);
        Debug.Log("game.GenerateMiniGame( " + count + ", " + (menu.value < 2) + ", " + (menu.value % 2 == 1) +", " + release);
    }
    
    
}
