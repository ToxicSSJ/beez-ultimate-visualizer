using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CoordUtil {

    public const double RADIANS_TO_DEGREES = 180.0 / Math.PI;
    public const double DEGREES_TO_RADIANS = Math.PI / 180.0;

    public static double[] xyzToLatLonRadians(double[] xyz) {

        double x = xyz[0];
        double y = xyz[1];
        double z = xyz[2];
        double[] answer = new double[3];
        double a = 6378137.0; //semi major axis
        double b = 6356752.3142; //semi minor axis

        double eSquared; //first eccentricity squared
        double rSubN; //radius of the curvature of the prime vertical
        double ePrimeSquared;//second eccentricity squared
        double W = Math.Sqrt((x * x + y * y));

        eSquared = (a * a - b * b) / (a * a);
        ePrimeSquared = (a * a - b * b) / (b * b);
        
        if (x >= 0) {
            answer[1] = Math.Atan(y / x);
        } else if (x < 0 && y >= 0) {
            answer[1] = Math.Atan(y / x) + Math.PI;
        } else {
            answer[1] = Math.Atan(y / x) - Math.PI;
        }

        double tanBZero = (a * z) / (b * W);
        double BZero = Math.Atan((tanBZero));
        double tanPhi = (z + (ePrimeSquared * b * (Math.Pow(Math.Sin(BZero), 3)))) / (W - (a * eSquared * (Math.Pow(Math.Cos(BZero), 3))));
        double phi = Math.Atan(tanPhi);
        answer[0] = phi;

        rSubN = (a * a) / Math.Sqrt(((a * a) * (Math.Cos(phi) * Math.Cos(phi)) + ((b * b) * (Math.Sin(phi) * Math.Sin(phi)))));
        answer[2] = (W / Math.Cos(phi)) - rSubN;

        return answer;

    }

    public static double[] xyzToLatLonDegrees(double[] xyz) {

        double[] degrees = xyzToLatLonRadians(xyz);

        degrees[0] = degrees[0] * RADIANS_TO_DEGREES;
        degrees[1] = degrees[1] * RADIANS_TO_DEGREES;

        return degrees;

    }

    public static double[] getXYZfromLatLonRadians(double latitude, double longitude, double height) {

        double a = 6378137.0;
        double b = 6356752.3142;
        double cosLat = Math.Cos(latitude);
        double sinLat = Math.Sin(latitude);

        double rSubN = (a * a) / Math.Sqrt(((a * a) * (cosLat * cosLat) + ((b * b) * (sinLat * sinLat))));

        double X = (rSubN + height) * cosLat * Math.Cos(longitude);
        double Y = (rSubN + height) * cosLat * Math.Sin(longitude);
        double Z = ((((b * b) / (a * a)) * rSubN) + height) * sinLat;

        return new double[] { X, Y, Z };

    }

    public static double[] getXYZfromLatLonDegrees(double latitude, double longitude, double height) {

        double[] degrees = getXYZfromLatLonRadians(
                latitude * DEGREES_TO_RADIANS,
                longitude * DEGREES_TO_RADIANS,
                height);

        return degrees;

    }

    public static double[] getPointXYZfromLatLongDegress(double[] dataset) {
        return getXYZfromLatLonDegrees(dataset[0], dataset[1], dataset[2]);
    }

}
