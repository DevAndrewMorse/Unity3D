using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    #region Fields
    public int _framesPerSecond;
    #endregion Fields

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        // Set the target frame rate
        Application.targetFrameRate = _framesPerSecond;
    }
    #endregion Methods
}
