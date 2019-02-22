using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Interactive : MonoBehaviour {
    [SerializeField]
    protected GameObject menu;
    [SerializeField]
    protected GameObject cameraInteractive;
    protected bool[] slots;
    public Transform localDeAcessoPlayer;
    public bool menuPodeAtivar = false;

    public GameObject Menu{
        get{return menu;}
    }
    void Start()
    {
       
    }
    public virtual void DesativarMenu()
    {        
        menuPodeAtivar = false;
        if (!menuPodeAtivar )
        {
            PAC.Instance.MiniMenuManager();
            menu.SetActive(false);
            cameraInteractive.SetActive(false);
        }

    }
    public virtual void VerificacaoMenu() {
        if (menuPodeAtivar)
        {
            PAC.Instance.TemMenuAtivo = true;
            menu.SetActive(true);
            cameraInteractive.SetActive(true);
        }
        
    }

}
