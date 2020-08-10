using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class F_BankUnload : MonoBehaviour
{

    string animaticMusicBank = "{56da35c0-e554-41ae-84fc-2cd40f51ea8e}";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RuntimeManager.UnloadBank(animaticMusicBank);
        }
    }

}
