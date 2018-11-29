using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Diagnostics;

public class NumberConverter
{
	public static int ConvertToInt(string stringValue)
	{
		if (stringValue.Length > 0) {
			int numericalValue;
			if (stringValue [0] == '-') {
				numericalValue = -1 * convertStringToInt (stringValue.Substring (1));
			} else {
				numericalValue = convertStringToInt (stringValue);
			}
			return numericalValue;
		}
		return -1;
	}

	public static double ConvertToDouble (string stringValue)
	{
		double numericalValue;
		if (stringValue [0] == '-') {
			numericalValue = -1 * Convert.ToDouble (stringValue.Substring (1));
		} else {
			numericalValue = Convert.ToDouble (stringValue.Substring(1));
		}
		return numericalValue;
	}

    public static float ConvertToFloat (string stringValue)
    {
        return (float)ConvertToDouble (stringValue);
    }

	private static int convertStringToInt (string stringValue)
	{
		int finalValue = 0;
		int power = stringValue.Length - 1;

		for (int i = 0; i < stringValue.Length; i ++) {
			finalValue += (stringValue[i] - '0') * ((int)Math.Pow(10, power));
			power--;
		}
		return finalValue;
	}
}




