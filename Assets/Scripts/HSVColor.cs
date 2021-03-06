﻿using UnityEngine;

/// <summary>
/// Hue Saturation Value Struct with Color conversion functions.
/// Based on code by Jonathan Czeck : http://wiki.unity3d.com/index.php?title=HSBColor
/// Added constraints when setting values
/// @gaelBourhis
/// </summary>
[System.Serializable]
public struct HSVColor
{
    public float h {get; private set;}
    public float s {get; private set;}
    public float v {get; private set;}
    public float a {get; private set;}


    public HSVColor(float _h, float _s, float _v, float _a)
    {
    
        h = Loop(_h);
        s = Clamp(_s);
        v = Clamp(_v);
        a = Clamp(_a);
    }

    public HSVColor(float _h, float _s, float _v)
    {
        h = Loop(_h);
        s = Clamp(_s);
        v = Clamp(_v);
        a = 1f;
    }

    public HSVColor(Color col)
    {
        HSVColor temp = FromColor(col);
        h = temp.h;
        s = temp.s;
        v = temp.v;
        a = temp.a;
    }

    public static HSVColor FromColor(Color color)
    {
        HSVColor ret = new HSVColor(0f, 0f, 0f, color.a);

        float r = color.r;
        float g = color.g;
        float b = color.b;

        float max = Mathf.Max(r, Mathf.Max(g, b));

        if (max <= 0)
        {
            return ret;
        }

        float min = Mathf.Min(r, Mathf.Min(g, b));
        float dif = max - min;

        if (max > min)
        {
            if (g == max)
            {
                ret.h = (b - r) / dif * 60f + 120f;
            }
            else if (b == max)
            {
                ret.h = (r - g) / dif * 60f + 240f;
            }
            else if (b > g)
            {
                ret.h = (g - b) / dif * 60f + 360f;
            }
            else
            {
                ret.h = (g - b) / dif * 60f;
            }
            if (ret.h < 0)
            {
                ret.h = ret.h + 360f;
            }
        }
        else
        {
            ret.h = 0;
        }

        ret.h *= 1f / 360f;
        ret.s = (dif / max) * 1f;
        ret.v = max;

        return ret;
    }

    public static Color ToColor(HSVColor hsvColor)
    {
        float r = hsvColor.v;
        float g = hsvColor.v;
        float b = hsvColor.v;
        if (hsvColor.s != 0)
        {
            float max = hsvColor.v;
            float dif = hsvColor.v * hsvColor.s;
            float min = hsvColor.v - dif;

            float h = hsvColor.h * 360f;

            if (h < 60f)
            {
                r = max;
                g = h * dif / 60f + min;
                b = min;
            }
            else if (h < 120f)
            {
                r = -(h - 120f) * dif / 60f + min;
                g = max;
                b = min;
            }
            else if (h < 180f)
            {
                r = min;
                g = max;
                b = (h - 120f) * dif / 60f + min;
            }
            else if (h < 240f)
            {
                r = min;
                g = -(h - 240f) * dif / 60f + min;
                b = max;
            }
            else if (h < 300f)
            {
                r = (h - 240f) * dif / 60f + min;
                g = min;
                b = max;
            }
            else if (h <= 360f)
            {
                r = max;
                g = min;
                b = -(h - 360f) * dif / 60 + min;
            }
            else
            {
                r = 0;
                g = 0;
                b = 0;
            }
        }

        return new Color(Mathf.Clamp01(r), Mathf.Clamp01(g), Mathf.Clamp01(b), hsvColor.a);
    }

    public Color ToColor()
    {
        return ToColor(this);
    }

    public static float Loop(float val)
    {
        return val - Mathf.Floor(val);
    }

    public static float Clamp(float val)
    {
        val = Mathf.Clamp01(val);
        return val;
    }

    public override string ToString()
    {
        return "H:" + h + " S:" + s + " V:" + v;
    }

    public static HSVColor Lerp(HSVColor a, HSVColor b, float t)
    {
        float h, s;

        //check special case black (color.v==0): interpolate neither hue nor saturation!
        //check special case grey (color.s==0): don't interpolate hue!
        if (a.v == 0)
        {
            h = b.h;
            s = b.s;
        }
        else if (b.v == 0)
        {
            h = a.h;
            s = a.s;
        }
        else
        {
            if (a.s == 0)
            {
                h = b.h;
            }
            else if (b.s == 0)
            {
                h = a.h;
            }
            else
            {
                // works around bug with LerpAngle
                float angle = Mathf.LerpAngle(a.h * 360f, b.h * 360f, t);
                while (angle < 0f)
                    angle += 360f;
                while (angle > 360f)
                    angle -= 360f;
                h = angle / 360f;
            }
            s = Mathf.Lerp(a.s, b.s, t);
        }
        return new HSVColor(h, s, Mathf.Lerp(a.v, b.v, t), Mathf.Lerp(a.a, b.a, t));
    }

    public static void Test()
    {
        HSVColor color;

        color = new HSVColor(Color.red);
        Debug.Log("red: " + color);

        color = new HSVColor(Color.green);
        Debug.Log("green: " + color);

        color = new HSVColor(Color.blue);
        Debug.Log("blue: " + color);

        color = new HSVColor(Color.grey);
        Debug.Log("grey: " + color);

        color = new HSVColor(Color.white);
        Debug.Log("white: " + color);

        color = new HSVColor(new Color(0.4f, 1f, 0.84f, 1f));
        Debug.Log("0.4, 1f, 0.84: " + color);

        Debug.Log("164,82,84   .... 0.643137f, 0.321568f, 0.329411f  :" + ToColor(new HSVColor(new Color(0.643137f, 0.321568f, 0.329411f))));
    }
}
