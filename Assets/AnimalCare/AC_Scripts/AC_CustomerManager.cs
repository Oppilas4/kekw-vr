using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
        { "Washing", 50 },
        { "Feeding", 5 }
    };

    private Dictionary<string, int> serviceSlotIndices = new Dictionary<string, int>()
    {
        { "Washing", 0 },
        { "Trimming", 1 },
        { "Feeding", 2 }
    };
    private Dictionary<string, float> serviceDurations = new Dictionary<string, float>()
    {
        { "Trimming", 60f },
        { "Washing", 100f },
        { "Feeding", 20f }
    };
    private GameObject currentCustomerObj;
    public TextMeshProUGUI[] taskTexts;
    public TextMeshProUGUI[] priceTexts;
    public AC_ChecklistManager checklistManager;
    public AC_DogMovement dog;
    public TextMeshProUGUI timeLimitText;
    public GameObject dripping;
    ParticleSystem waterDripping;
    public AudioSource dingSound;
    public AudioSource failSound;
    public TextMeshProUGUI resultText;
    void Start()
    {
        waterDripping = dripping.GetComponent<ParticleSystem>();
        StartCoroutine(ServeNextCustomer());
    }

    IEnumerator ServeNextCustomer()
    {
        while (true)
        {
            resultText.text = "";
           // Instantiate a new customer prefab
           currentCustomerObj = Instantiate(customerPrefab, spawnPoint.position, spawnPoint.rotation);
            dog.gameObject.SetActive(true);
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

            // Start countdown based on total task time
            float totalCustomerTime = 0f;
            foreach (string service in currentCustomer.services)
            {
                totalCustomerTime += serviceDurations[service];
            }
            Debug.Log($"Customer time limit: {totalCustomerTime} seconds");

            // Start parallel coroutines
            bool tasksCompleted = false;
            Coroutine timerCoroutine = StartCoroutine(CustomerTimer(totalCustomerTime, () =>
            {
                if (!tasksCompleted)
                {
                    resultText.text = "Time ran out! Customer is leaving.";
                    failSound.Play();
                    dog.MoveToDoor();
                    waterDripping.Stop();
                    dripping.SetActive(false);
                }
            }));
            yield return new WaitUntil(() => AreAllTasksGreen() || !dog.gameObject.activeSelf);
            // Mark tasks as completed only if within time
            if (AreAllTasksGreen())
            {
                tasksCompleted = true;
                dingSound.Play();
                resultText.text = $"Customer done! Earned: {currentCustomer.totalPayment}e";
            }

            // Cleanup
            StopCoroutine(timerCoroutine); // Stop timer if it hasn't finished
            timeLimitText.text = "";
            dog.MoveToDoor();
            yield return new WaitUntil(() => !dog.gameObject.activeSelf);
            Destroy(currentCustomerObj);
            checklistManager.CheckScoreForReward();
            yield return new WaitForSeconds(3f); // short delay before next customer
        }
    }

    Customer GenerateRandomCustomer()
    {
        List<string> possibleServices = new List<string>(servicePrices.Keys);
        List<string> selectedServices = new List<string>();

        int serviceCount = Random.Range(1, 4); // choose 1 to 3 services

        // Keep generating until Feeding is not the only service
        do
        {
            selectedServices.Clear();
            while (selectedServices.Count < serviceCount)
            {
                string randomService = possibleServices[Random.Range(0, possibleServices.Count)];
                if (!selectedServices.Contains(randomService))
                    selectedServices.Add(randomService);
            }
        }
        while (selectedServices.Count == 1 && selectedServices.Contains("Feeding"));

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
    IEnumerator CustomerTimer(float duration, System.Action onTimeout)
    {
        float timeLeft = duration;

        while (timeLeft > 0)
        {
            int minutes = Mathf.FloorToInt(timeLeft / 60f);
            int seconds = Mathf.FloorToInt(timeLeft % 60f);
            string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
            timeLimitText.text = "Time Left: " + timeString;

            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }

        timeLimitText.text = "Time Left: 00:00";
        onTimeout?.Invoke();
    }
}
