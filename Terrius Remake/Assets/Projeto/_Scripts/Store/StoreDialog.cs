using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class StoreDialog : MonoBehaviour
{
    [SerializeField] private GameObject DialogInterface;
    [SerializeField] private TextMeshProUGUI dialogo_txt;
    private int indice;

    [SerializeField] private GameObject StoreInterface;
    public float velocidade = 0.05f;
    public float pausa = 2.5f;
    
    private bool escrevendo = false;
    private Coroutine traçoCoroutine;

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
        StartCoroutine(MostrarDialogo());
    }

    
    IEnumerator MostrarDialogo()
    {
        for (int i = 0; i < dialogos.Count; i++)
        {
            escrevendo = true;
            dialogo_txt.text = "";

            if (traçoCoroutine != null)
                StopCoroutine(traçoCoroutine);

            foreach (char d in dialogos[indice])
            {
                dialogo_txt.text += d;
                dialogo_txt.text += "|"; // mostra com o traço
                yield return new WaitForSeconds(velocidade);
                dialogo_txt.text = dialogo_txt.text.TrimEnd('|'); // remove traço para próxima letra
            }

            // Após terminar de escrever, inicia piscar traço
            traçoCoroutine = StartCoroutine(PiscarTraco());
            escrevendo = false;

            yield return new WaitForSeconds(pausa);
            indice++;

            if (i >= dialogos.Count - 1)
            {
                DialogInterface.GetComponent<Animator>().SetTrigger("exit");
                StartStore();
            }
        }
    }

    IEnumerator PiscarTraco()
    {
        while (true)
        {
            dialogo_txt.text += "|";
            yield return new WaitForSeconds(0.5f);
            dialogo_txt.text = dialogo_txt.text.TrimEnd('|');
            yield return new WaitForSeconds(0.5f);
        }
    }
}
