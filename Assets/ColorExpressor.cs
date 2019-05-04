using UnityEngine;
using System.Collections;
using System;

public class ColorExpressor: IGeneExpresser {

    public ColorComponent ExpressionComponent { get; set; }

    public ColorExpressor(ColorComponent expressionComponent) {
        this.ExpressionComponent = expressionComponent;
    }

    public void Express(GameObject gameObject,int value)
    {
        var originalColor = gameObject.GetComponent<Renderer>().material.color;
        switch (this.ExpressionComponent) {
            case ColorComponent.Blue:
                originalColor.b = GetAlteredColor(value);
                break;
            case ColorComponent.Red:
                originalColor.r = GetAlteredColor(value);
                break;
            case ColorComponent.Green:
                originalColor.g = GetAlteredColor(value);
                break;
            default:
                break;
        }
        gameObject.GetComponent<Renderer>().material.color = originalColor;
    }

    float GetAlteredColor(int value) {
        //TODO find out if I want to actually do anything extra to the values
        return value / 256f;
    }
}
