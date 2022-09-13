using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGravity
{
    void ApplyGravity(Vector3 planetPosition, float planetMass);
}
