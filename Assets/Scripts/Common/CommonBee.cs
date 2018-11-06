using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonBee {

    public double x { get; set; }
    public double y { get; set; }
    public double z { get; set; }

    public CommonBee(double x, double y, double z) {

        this.x = x;
        this.y = y;
        this.z = z;

    }

    public double[] toPoint3D() {
        return new double[] { x, y, z };
    }

    public double[] toCartesianPoint3D() {
        double[] xyz = CoordUtil.getXYZfromLatLonDegrees(x, y, z);
        return xyz;
    }

}
