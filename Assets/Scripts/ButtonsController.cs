using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{

    public List<Interruptor> btnlist;
    public List<Slider> slidlist;

    private Dictionary<Interruptor, bool> dicBtnsP1;
    private Dictionary<Slider, int> dicSldP1;

    private Dictionary<Interruptor, bool> dicBtnsP2;
    private Dictionary<Slider, int> dicSldP2;

    private ButtonsController instance = null;

    public List<Interruptor> getButtons(int player)
    {
        if (player == 1)
        {
            return btnlist.GetRange(0, 4);
        }
        else if (player == 2)
        {
            return btnlist.GetRange(3, 4);
        }
        else
        {
            Debug.Log("Error");
            return null;
        }
    }

    // Use this for initialization
    void Start()
    {

        instance = this;

        GameObject canvasObject = GameObject.FindGameObjectWithTag("MainCanvas");
        Transform user1 = canvasObject.transform.Find("user1");
        Transform user2 = canvasObject.transform.Find("user2");

        Transform[] userZone1 = { canvasObject.transform.Find("zonePlayer11") ,
                                    canvasObject.transform.Find("zonePlayer12"),
                                    canvasObject.transform.Find("zonePlayer13"),
                                    canvasObject.transform.Find("zonePlayer14")};

        Transform[] userZone2 = { canvasObject.transform.Find("zonePlayer21") ,
                                    canvasObject.transform.Find("zonePlayer22"),
                                    canvasObject.transform.Find("zonePlayer23"),
                                    canvasObject.transform.Find("zonePlayer24")};

        for (int i = 0; i < 8; i++)
        {
            if (i < 4)
            {
                if (userZone1[i].transform.GetChild(0) != null)
                {
                    if (userZone1[i].transform.GetChild(0).GetComponent(typeof(Interruptor)) != null)
                    {
                        btnlist.Add((Interruptor)userZone1[i].transform.GetChild(0).GetComponent(typeof(Interruptor)));
                    }
                    else if (userZone1[i].transform.GetChild(0).GetComponent(typeof(Slider)) != null)
                    {
                        slidlist.Add((Slider)userZone1[i].transform.GetChild(0).GetComponent(typeof(Slider)));
                    }
                    else
                    {
                        Debug.Log("Button missing player 1 zone " + i.ToString());
                    }
                }
            }

            else
            {
                if (userZone2[i].transform.GetChild(0) != null)
                {
                    if (userZone2[i].transform.GetChild(0).GetComponent(typeof(Interruptor)) != null)
                    {
                        btnlist.Add((Interruptor)userZone2[i].transform.GetChild(0).GetComponent(typeof(Interruptor)));
                    }
                    else if (userZone2[i].transform.GetChild(0).GetComponent(typeof(Slider)) != null)
                    {
                        slidlist.Add((Slider)userZone2[i].transform.GetChild(0).GetComponent(typeof(Slider)));
                    }
                    else
                    {
                        Debug.Log("Button missing player 1 zone " + i.ToString());
                    }
                }
            }
        }

        foreach (Interruptor btn in btnlist)
        {
            dicBtnsP1.Add(btn, false);
            dicBtnsP2.Add(btn, false);
        }
        foreach (Slider sld in slidlist)
        {
            dicSldP1.Add(sld, 0);
            dicSldP2.Add(sld, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public ButtonsController getInstance()
    {
        return instance;
    }

    public void onClick(Interruptor btn, int player, bool value)
    {
        if (player == 1)
        {
            dicBtnsP1[btn] = value;
        }
        else if (player == 2)
        {
            dicBtnsP2[btn] = value;
        }
        else
        {
            Debug.Log("Error");
        }
    }

    public void onSlide(Slider sld, int player, bool up)
    {
        if (player == 1)
        {
            dicSldP1[sld] += (up) ? 1 : (-1);
        }
        else if (player == 2)
        {
            dicSldP2[sld] += (up) ? 1 : (-1);
        }
        else
        {
            Debug.Log("Error");
        }
    }
}
