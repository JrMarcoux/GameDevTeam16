using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class keyBindScript : MonoBehaviour {

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public TMPro.TextMeshProUGUI up, left, down, right, fire, chgCharacter, chgTarget;

    private GameObject currentKey;

    GameObject eventSystem;

    void Start () {
        eventSystem = GameObject.Find("EventSystem");

        keys.Add("Up", KeyCode.W);
        keys.Add("Down", KeyCode.S);
        keys.Add("Left", KeyCode.A);
        keys.Add("Right", KeyCode.D);
        keys.Add("Fire", KeyCode.Space);
        keys.Add("ChgCharacter", KeyCode.LeftShift);
        keys.Add("ChgTarget", KeyCode.LeftControl);

        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();

        up.text = keys["Up"].ToString();
        down.text = keys["Down"].ToString();
        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        fire.text = keys["Fire"].ToString();
        chgCharacter.text = keys["ChgCharacter"].ToString();
        chgTarget.text = keys["ChgTarget"].ToString();
    }
	
	void Update () {
		
	}

    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = e.keyCode.ToString();
                eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
                currentKey = null;
            }
            else if (Input.GetKey(KeyCode.Joystick1Button0))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button0);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button1))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button1);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button2))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button2);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button3))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button3);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button4))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button4);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button5))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button5);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button6))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button6);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button7))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button7);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button8))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button8);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button9))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button9);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button10))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button10);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button11))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button11);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button12))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button12);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button13))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button13);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button14))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button14);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button15))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button15);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button16))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button16);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button17))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button17);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button18))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button18);
            }
            else if (Input.GetKey(KeyCode.Joystick1Button19))
            {
                setJoystickKeyCode(KeyCode.Joystick1Button19);
            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                setJoystickKeyCode(KeyCode.Mouse0);
            }
            else if (Input.GetKey(KeyCode.Mouse1))
            {
                setJoystickKeyCode(KeyCode.Mouse1);
            }
            else if (Input.GetKey(KeyCode.Mouse2))
            {
                setJoystickKeyCode(KeyCode.Mouse2);
            }
            else if (Input.GetKey(KeyCode.Mouse3))
            {
                setJoystickKeyCode(KeyCode.Mouse3);
            }
            else if (Input.GetKey(KeyCode.Mouse4))
            {
                setJoystickKeyCode(KeyCode.Mouse4);
            }
            else if (Input.GetKey(KeyCode.Mouse5))
            {
                setJoystickKeyCode(KeyCode.Mouse5);
            }
            else if (Input.GetKey(KeyCode.Mouse6))
            {
                setJoystickKeyCode(KeyCode.Mouse6);
            }
        }      
    }

    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }

    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }

    public void setJoystickKeyCode(KeyCode button)
    {
        keys[currentKey.name] = button;
        currentKey.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = button.ToString();
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        currentKey = null;
    }
}
