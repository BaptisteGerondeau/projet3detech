using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interruptor : MonoBehaviour
{

    private bool _state = false;

    public int player = -1;
    public Button mybtn;
    private ButtonsController master;
    // Use this for initialization
    void Start()
    {

        if (mybtn == null)
        {
            if (GetComponentInChildren<Button>() != null)
            {
                mybtn = GetComponentInChildren<Button>();
            }
        }

        mybtn.onClick.AddListener(TaskOnClick);

        master = ((ButtonsController)GameObject.FindGameObjectWithTag("MainCanvas").GetComponent(typeof(ButtonsController))).getInstance();
    }

    // Update is called once per frame
    void TaskOnClick()
    {
        _state = !_state;
        master.onClick(this, player, _state);
    }
}
