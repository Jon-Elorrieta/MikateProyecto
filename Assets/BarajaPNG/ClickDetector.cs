using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClickDetector : MonoBehaviour
{
    //public delegate void ClickCallback(GameObject clickedObject);
    //public event ClickCallback OnClick;

    public event Action<ClickDetector> OnCartaClicked;

    //private void OnMouseDown()
    //{
        //if (OnClick != null)
        //{
            //OnClick(gameObject);
        //}
    //}
    public void OnMouseDown()
    {
        if (OnCartaClicked != null)
        {
            OnCartaClicked(this);
        }
    }

}
