using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CompletedDishArea : MonoBehaviour
{
    public UnityEvent _calculateDish = new UnityEvent();

    private void OnEnable()
    {
        // Subscribe to the _orderReady event when the script is enabled
        FindObjectOfType<OrderBell>()._orderReady.AddListener(OnOrderReady);
    }

    private void OnDisable()
    {
        // Unsubscribe from the _orderReady event when the script is disabled to prevent memory leaks
        FindObjectOfType<OrderBell>()._orderReady.RemoveListener(OnOrderReady);
    }

    private void OnOrderReady()
    {
        // Method to be executed when the _orderReady event happens
        CheckForDish();
    }

    public void CheckForDish()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2, transform.rotation);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Dish"))
            {
                Debug.Log("dishDetected");
                // Trigger the _calculateDish event if a dish is found
                _calculateDish.Invoke();
                return; // No need to continue checking once a dish is found
            }
        }
    }
}
