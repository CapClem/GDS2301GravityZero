using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider jetPackFuel;
    public Slider DashFuel;

    //starting fuel
    public void setTotalFuelLevel(float fuel)
    {
        jetPackFuel.maxValue = fuel;
        jetPackFuel.value = 0;
    }

    public void setTotalDashLevel(float fuel)
    {
        DashFuel.maxValue = fuel;
        DashFuel.value = 0;
    }

    //increment fuel level
    public void SetFuelLevel(int fuel)
    {
        jetPackFuel.value = fuel;
    }

    public void SetDashLevel(float fuel)
    {
        DashFuel.value = fuel;
    }
}
