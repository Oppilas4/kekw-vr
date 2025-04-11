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

    private GameObject currentCustomerObj;
    public TextMeshProUGUI[] taskTexts;
    public TextMeshProUGUI[] priceTexts;
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
            for (int i = 0; i < currentCustomer.services.Count && i < taskTexts.Length && i < priceTexts.Length; i++)
            {
                string serviceName = currentCustomer.services[i];
                int servicePrice = servicePrices[serviceName];

                taskTexts[i].text = serviceName;
                priceTexts[i].text = servicePrice + "€";
            }
            Debug.Log($"New customer arrived! Wants: {string.Join(", ", currentCustomer.services)} | Will pay: {currentCustomer.totalPayment}e");

            // Wait until all tasks are marked green
            yield return new WaitUntil(() => AreAllTasksGreen());

            Debug.Log($"Customer done! Earned: {currentCustomer.totalPayment}e");

            Destroy(currentCustomerObj);

            yield return new WaitForSeconds(1f); // short delay before next customer
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
