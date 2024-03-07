using UnityEngine;
using static OrderManager;

[System.Serializable]
public class Order
{
    public int orderId;
    public string dishName;
    public float expirationTime;
    public SteakTemperature steakTemperature;
    public GameObject orderTicketObject;
}
