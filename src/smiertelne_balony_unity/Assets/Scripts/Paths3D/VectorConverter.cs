using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VectorConvOptions {
    XtoX,
    XtoY,
    XtoZ,
    YtoY,
    YtoX,
    YtoZ,
    ZtoZ,
    ZtoX,
    ZtoY
}

public static class VectorConverter {

    public static Vector2[] ConvertToVectors2( Vector3[] vectors, VectorConvOptions aAxis = VectorConvOptions.XtoX, VectorConvOptions bAxis = VectorConvOptions.ZtoY ) {
        Vector2[] v2s = new Vector2[vectors.Length];
        int i = 0;
        foreach ( Vector3 v3 in vectors ) {
            v2s[i] = ConvertToVector2( v3, aAxis, bAxis );
            i++;
        }
        return v2s;
    }

    public static Vector2 ConvertToVector2( Vector3 vector, VectorConvOptions aAxis = VectorConvOptions.XtoX, VectorConvOptions bAxis = VectorConvOptions.ZtoY ) {
        if ( vector == null ) { return Vector2.zero; }
        float x = ((aAxis == VectorConvOptions.ZtoX) ? vector.z : (aAxis == VectorConvOptions.YtoX) ? vector.y : vector.x);
        float y = ((bAxis == VectorConvOptions.ZtoY) ? vector.z : (bAxis == VectorConvOptions.XtoY) ? vector.x : vector.y);
        return new Vector2( x, y );
    }
	
}
