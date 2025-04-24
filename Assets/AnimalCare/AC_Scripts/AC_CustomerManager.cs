using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AC_CustomerManager : MonoBehaviour
{
    [Header("Customer Prefab")]
    public GameObject customerPrefab;
    public Transform spawnPoint;

    [Header("Service Settings")]
    private Dictionary<string, int> servicePrices = new Dictionary<string, int>()
    {
        { "Trimming", 20 },
        { "Washing", 30 },
        { "Caring", 10 }
    };

    private Dictionary<string, int> serviceSlotIndices = new Dictionary<string, int>()
    {
        { "Washing", 0 },
        { "Trimming", 1 },
        { "Caring", 2 }
    };

    private GameObject currentCustomerObj;
    public TextMeshProUGUI[] taskTexts;
    public TextMeshProUGUI[] priceTexts;
    public AC_ChecklistManager checklistManager;
    public GameObject dog;
    void Start()
    {
        StartCoroutine(ServeNextCustomer());
    }

    IEnumerator ServeNextCustomer()
    {
        while (true)
        {
            // Instantiate a new customer prefab
            currentCustomerObj = Instantiate(customerPrefab, spawnPoint.position, spawnPoint.rotation);
            dog.SetActive(true);
            // Generate a random service order for this customer
            Customer currentCustomer = GenerateRandomCustomer();

            // Clear all task and price text fields first
            foreach (var text in taskTexts)
            {
                text.color = Color.black;
                text.text = "";
            }
            foreach (var price in priceTexts)
            {
                price.text = "";
            }

            // Show services and prices
            foreach (string serviceName in currentCustomer.services)
            {
                if (serviceSlotIndices.TryGetValue(serviceName, out int index) &&
                    index < taskTexts.Length && index < priceTexts.Length)
                {
                    taskTexts[index].text = serviceName;
                    priceTexts[index].text = servicePrices[serviceName] + "€";
                }
            }
            // Convert service names to checklist indices
            List<int> validTaskIndices = new List<int>();
            foreach (string serviceName in currentCustomer.services)
            {
                if (serviceSlotIndices.TryGetValue(serviceName, out int index))
                {
                    validTaskIndices.Add(index);
                }
            }

            // Tell checklist manager what tasks are valid
            checklistManager.SetValidTasks(validTaskIndices);

            Debug.Log($"New customer arrived! Wants: {string.Join(", ", currentCustomer.services)} | Will pay: {currentCustomer.totalPayment}e");

            // Wait until all tasks are marked green
            yield return new WaitUntil(() => AreAllTasksGreen());

            Debug.Log($"Customer done! Earned: {currentCustomer.totalPayment}e");
            yield return new WaitForSeconds(3f);
            Destroy(currentCustomerObj);
            dog.SetActive(false);
            yield return new WaitForSeconds(3f); // short delay before next customer
        }
    }

    Customer GenerateRandomCustomer()
    {
        List<string> possibleServices = new List<string>(servicePrices.Keys);
        List<string> selectedServices = new List<string>();

        int serviceCount = Random.Range(1, 4); // choose 1 to 3 services

        while (selectedServices.Count < serviceCount)
        {
            string randomService = possibleServices[Random.Range(0, possibleServices.Count)];
            if (!selectedServices.Contains(randomService))
                selectedServices.Add(randomService);
        }

        int totalCost = 0;
        foreach (string service in selectedServices)
        {
            totalCost += servicePrices[service];
        }

        return new Customer(selectedServices, totalCost);
    }

    public class Customer
    {
        public List<string> services;
        public int totalPayment;

        public Customer(List<string> services, int payment)
        {
            this.services = services;
            this.totalPayment = payment;
        }
    }
    bool AreAllTasksGreen()
    {
        foreach (var text in taskTexts)
        {
            if (text.text != "" && text.color != Color.green)
            {
                return false;
            }
        }
        return true;
    }
}
