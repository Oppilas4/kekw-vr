using System.Collections.Generic;
using UnityEngine;

public class HotObjectManager : MonoBehaviour
{
    private static List<IHotObject> hotObjects = new List<IHotObject>();

    public static void RegisterHotObject(IHotObject hotObject)
    {
        if (!hotObjects.Contains(hotObject))
        {
            hotObjects.Add(hotObject);
        }
    }

    public static void UnregisterHotObject(IHotObject hotObject)
    {
        hotObjects.Remove(hotObject);
    }

    public static bool IsObjectHot(IHotObject hotObject)
    {
        return hotObject.IsHot();
    }

    // Add more methods as needed

    // Example: Update hot states based on some global condition
    public static void UpdateHotStates()
    {
        foreach (var hotObject in hotObjects)
        {
            // Update hot states based on some condition
            bool newHotState = SomeGlobalCondition(); // Replace with actual logic
            hotObject.SetHot(newHotState);
        }
    }

    private static bool SomeGlobalCondition()
    {
        // Implement some global condition to determine hot states
        return false; // Replace with actual logic
    }
}
