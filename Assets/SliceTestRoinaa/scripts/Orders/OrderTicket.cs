using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderTicket : MonoBehaviour
{
    public TextMeshProUGUI orderNumberText;
    public TextMeshProUGUI dishNameText;

    public void SetOrderInfo(int orderNumber, string dishName)
    {
        orderNumberText.text = "Order #" + orderNumber;
        dishNameText.text = "Dish: " + dishName;
    }
}
