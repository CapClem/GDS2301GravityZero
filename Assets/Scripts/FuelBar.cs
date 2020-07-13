using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider jetPackFuel;

    //starting fuel
    public void setTotalFuelLevel(int fuel)
    {
        jetPackFuel.maxValue = fuel;
        jetPackFuel.value = 0;
    }

    //increment fuel level
    public void SetFuelLevel(int fuel)
    {
        jetPackFuel.value = fuel;
    }
}
