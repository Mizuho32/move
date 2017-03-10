using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MultiInput
{
    public enum Mode
    {
        Multi,
        Single
    }

    public enum Axis
    {
        Mouse_X,
        Mouse_Y,
        Mouse_ScrollWheel
    }

    public static string[] idToName;
    public static Mode mode;
    // Number of input
    public static int Count {
        get
        {
            return _mice.Length;
        }
    }
    public static ManyMouse[] Mice
    {
        get
        {
            return _mice;
        }
    }


    private static ManyMouse[] _mice;
    
    public static void Init(Mode mode)
    {
        MultiInput.mode = mode;

        if (mode == Mode.Single)
            return;

        var numMice = ManyMouseWrapper.MouseCount;
        if (numMice > 0)
        {
            _mice = new ManyMouse[numMice];
            idToName = new string[numMice];

            for(var i = 0; i < numMice; i++)
            {
                _mice[i] = ManyMouseWrapper.GetMouseByID(i);
                idToName[i] = _mice[i].DeviceName;
            }
        }
    }

    public static float GetAxis(int index, Axis axis)
    {
        if (index + 1 >= _mice.Length || mode == Mode.Single)
            return 0f;

        switch (axis)
        {
            case Axis.Mouse_X:
                return _mice[index].Delta.x;
            case Axis.Mouse_Y:
                return _mice[index].Delta.y;
            case Axis.Mouse_ScrollWheel:
                return _mice[index].MouseWheel;
            default:
                return 0f;
        }
    }

    public static float GetAxis(string name)
    {
        if (mode == Mode.Multi)
            return 0f;

        return Input.GetAxis(name);
    }

    public static bool GetMouseButton(int index, int number)
    {
        if (index + 1 >= _mice.Length || mode == Mode.Single)
            return false;

        if (number + 1 >= _mice[index].MouseButtons.Length)
            return false;

        return _mice[index].MouseButtons[number];
    }

    public static bool GetMouseButton(int number)
    {

        if (mode == Mode.Multi)
            return false;

        return Input.GetMouseButton(number);
    }
}
