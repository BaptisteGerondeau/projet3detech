using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrdersController : MonoBehaviour {

    public Order currP1;
    public Order currP2;

    private List<Order> ordersP1;
    private List<Order> ordersP2;

    private ButtonsController btnsCtrl;
    // Use this for initialization
    void Start () {
        btnsCtrl = ((ButtonsController)GameObject.FindGameObjectWithTag("MainCanvas").GetComponent(typeof(ButtonsController))).getInstance();

        Order orderP11 =  new Order(1, new OrdersElement("Interruptor", btnsCtrl.getButtons(2)[2], 1));
        Order orderP12 = new Order(1, new OrdersElement("Interruptor", btnsCtrl.getButtons(1)[2], 1));
        Order orderP21 = new Order(1, new OrdersElement("Interruptor", btnsCtrl.getButtons(1)[2], 1));
        Order orderP22 = new Order(1, new OrdersElement("Interruptor", btnsCtrl.getButtons(2)[2], 1));

        ordersP1.Add(orderP11);
        ordersP1.Add(orderP12);
        ordersP2.Add(orderP21);
        ordersP2.Add(orderP22);

        currP1 = ordersP1[0];
        currP2 = ordersP2[0];


    }
	
	// Update is called once per frame
	void Update () {
		if (checkCurrentOrder(1) && checkCurrentOrder(2))
        {
            Debug.Log("ORDERS completed !");
        } else if (checkCurrentOrder(1))
        {
            Debug.Log("P1 order completed");
        } else if (checkCurrentOrder(2))
        {
            Debug.Log("P2 order completed");
        }
	}

    void setOrders(int level)
    {

    }

    bool checkAction(OrdersElement el, int value)
    {
        el.setValue(value);
        return el.CheckValidity();
    }

    bool checkCurrentOrder(int player)
    {
        if (player == 1)
        {
            return this.currP1.element.CheckValidity();
        } if (player == 2)
        {
            return this.currP2.element.CheckValidity();
        } else
        {
            return false;
        }
    }
}

public class Order
{
    public int player;
    public OrdersElement element;

    public Order(int ply, OrdersElement el)
    {
        player = ply;
        el = element;
    }

}

public class OrdersElement
{
    public string type;
    public Interruptor btn;
    public Slider sld;
    public int exValue;
    public int value;

    public OrdersElement(string type, Interruptor btn, int expectedValue)
    {
        this.type = type;
        this.btn = btn;
        this.exValue = expectedValue;
    }

    public OrdersElement(string type, Slider sld, int expectedValue)
    {
        this.type = type;
        this.sld = sld;
        this.exValue = expectedValue;
    }

    public void setValue(int value)
    {
        this.value = value;
    }

    public bool CheckValidity()
    {
        if (exValue != -1)
        {
            return (value == exValue);
        }
        else
        {
            Debug.Log("Not part of the order");
            return false;
        }
       
    }

}
