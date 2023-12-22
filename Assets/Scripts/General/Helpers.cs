using UnityEngine;

public static class Helpers
{
    public static bool ArePositionsAlmostEqual(Vector3 pos1, Vector3 pos2, float tolerance)
    {
        return Vector3.SqrMagnitude(pos1 - pos2) < tolerance * tolerance;
    }
}
