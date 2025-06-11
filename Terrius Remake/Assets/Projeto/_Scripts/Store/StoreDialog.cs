using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreDialog : MonoBehaviour
{
    [SerializeField] private GameObject DialogInterface;
    [SerializeField] private TextMeshProUGUI dialogo_txt;
    private int indice;

    [SerializeField] private GameObject StoreInterface;

    private bool FirstStoreStart;

    [TextArea]
    public List<string> dialogos = new();

    void Start()
    {
        if (PlayerPrefs.HasKey("FirstStoreStart"))
        {
            FirstStoreStart = PlayerPrefs.GetInt("FirstStoreStart", 0) == 1;
        }
        else FirstStoreStart = true;
    }


    public void StartAnimationDialog()
    {
        if (FirstStoreStart)
        {
            //Todo o conteudo
            DialogInterface.SetActive(true);
            Dialog();

            FirstStoreStart = false;
            PlayerPrefs.SetInt("FirstStoreStart", FirstStoreStart ? 1 : 0);
        }
        else
        {
            StartStore();
        }
    }

    void StartStore()
    {
        StoreInterface.SetActive(true);
    }

    public void Dialog()
    {
        StartCoroutine(DialogoChar());
    }

    IEnumerator DialogoChar()
    {
        for (int i = 0; i < dialogos.Count; i++)
        {
            dialogo_txt.text = "";
            foreach (Char d in dialogos[indice])
            {
                dialogo_txt.text += d;
                yield return new WaitForSeconds(0.03f);
            }

            yield return new WaitForSeconds(1f);
            indice++;

            if (i >= dialogos.Count - 1)
            {
                DialogInterface.SetActive(false);
                StartStore();
            }
        }

    }
}
