using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderTicket : MonoBehaviour
{
    public TextMeshProUGUI orderNumberText;
    public TextMeshProUGUI dishNameText;
    public TextMeshProUGUI steakTemperatureText;

    public void SetOrderInfo(int orderNumber, string dishName, OrderManager.SteakTemperature steakTemperature)
    {
        orderNumberText.text = "Order #" + orderNumber;
        dishNameText.text = "Dish: " + dishName;

        if (dishName == "Steak")
        {
            steakTemperatureText.text = "Steak temperature: " + steakTemperature.ToString();
        }
        else
        {
            steakTemperatureText.text = "";
        }
    }
}
