using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIController : MonoBehaviour
{
    public GameObject horizontalLayout;
    public SettingsController settingsController;

    List<GameObject> items = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < horizontalLayout.transform.childCount; i++)
        {
            items.Add(horizontalLayout.transform.GetChild(i).gameObject);
        }
    }

    public void ItemMouseOver(GameObject item)
    {
        foreach (GameObject current in items)
        {
            if (current == item)
            {
                current.GetComponent<MenuItemController>().HighliteItem();
            }
            else
            {
                current.GetComponent<MenuItemController>().ShrinkItem();
            }
        }
    }

    public void ItemMouseLeave()
    {
        foreach (GameObject current in items)
        {
            current.GetComponent<MenuItemController>().ResetItem();
        }
    }

    public void QuiteGame()
    {
        Debug.Log("Quiting game");
        Application.Quit();
    }

    public void OpenSettings()
    {
        foreach (GameObject current in items)
        {
            current.GetComponent<MenuItemController>().HideItem();
        }
        settingsController.ShowContainer();
    }

    public void HideSetting()
    {
        ItemMouseLeave();
        settingsController.HideContainer();
    }
}
