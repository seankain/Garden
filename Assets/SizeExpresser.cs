using UnityEngine;
using System.Collections;
using System;

public class SizeExpresser: IGeneExpresser {

    private float maxScaleValue = 1.0f;

    public SizeExpresser(float max) {

    }

    public void Express(GameObject gameObject,int value)
    {
        var factor = maxScaleValue - (value / 256f);
        var scale = new Vector3
        {
            x = factor,
            y = factor,
            z = factor
        };
        gameObject.transform.localScale = scale;
    }
}
