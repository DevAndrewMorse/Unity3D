using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FPSLimiterTestScript
{
    private GameObject testObject; // The GameObject with FPSLimiter script

    [SetUp]
    public void Setup()
    {
        // Create a new GameObject for testing and attach the FPSLimiter script to it
        testObject = new GameObject("GameManager");
        testObject.AddComponent<FPSLimiter>();
    }

    [TearDown]
    public void Teardown()
    {
        // Destroy the test GameObject after each test
        GameObject.Destroy(testObject);
    }

    // Test to check if FPSLimiter sets the target frame rate correctly
    [UnityTest]
    public IEnumerator SetFrameRateTo60()
    {
        // Wait for one frame to let the FPSLimiter script execute
        yield return null;

        // Assert that Application.targetFrameRate is set to 60
        Assert.GreaterOrEqual(60, Application.targetFrameRate);
    }

    // Test to check if FPSLimiter can set a custom frame rate
    [UnityTest]
    public IEnumerator SetCustomFrameRate()
    {
        // Get the FPSLimiter component from the testObject
        FPSLimiter fpsLimiter = testObject.GetComponent<FPSLimiter>();

        // Set a custom frame rate
        fpsLimiter._framesPerSecond = 30;

        // Wait for one frame to let the FPSLimiter script execute
        yield return null;

        // Assert that Application.targetFrameRate is set to the custom value (30)
        Assert.GreaterOrEqual(30, Application.targetFrameRate);
    }
}
