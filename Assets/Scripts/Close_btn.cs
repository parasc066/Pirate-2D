using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Close_btn : MonoBehaviour
{
    // Start is called before the first frame update


    public Button closeButton;
    void Start()
    {
        closeButton.onClick.AddListener(CloseApp);

    }//start

    // Update is called once per frame
    void Update()
    {

    }//update

    public void CloseApp()
    {
        Application.Quit();
    }


}
