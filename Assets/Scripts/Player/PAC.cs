using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PAC : MonoBehaviour {

    public LayerMask mesa;
    public LayerMask ground;
    public LayerMask churrasqueira;
    public LayerMask geladeira;
    public LayerMask Default;
    //
    private bool temMenuAtivo = false;
    //
    private GameObject itemAtual;
    public float bebisseReference;
    //
    public GameObject Minimenu;
    public GameObject OptionsMenu;
    public GameObject PacParticle;
    //
    private NavMeshAgent agent;
    private Renderer rend;
    private MecanicasAlcool MecAlc;
    private static RaycastHit hit;
    private static Vector3 clickPosition;
    //
    
    //
    static PAC instance;
    public static PAC Instance { get { return instance;} }
    //
    // Use this for initialization
    void Start () {
        instance = this;
        MecAlc=GetComponent<MecanicasAlcool>();
		agent = GetComponent<NavMeshAgent> ();
        rend = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        Acoes(NoQueClicou());
        bebisseReference=MecAlc.bebisse;
        //Debug.Log(bebisse);
        if(Input.GetKeyDown(KeyCode.Escape)){
            MiniMenuManager();
        }
        
       
       
    }

#region MiniMenu

    public void MiniMenuManager()
    {
        if (!TemMenuAtivo)
        {
            OpenMiniMenu();
        }
        else if (TemMenuAtivo)
        {
            CloseMiniMenu();
        }
    }
    public void OpenMiniMenu() {       
        Minimenu.SetActive(true);
        temMenuAtivo = true;
    }
    public void CloseMiniMenu() {
        Minimenu.SetActive(false);
        temMenuAtivo = false;
        if (OptionsMenu.activeSelf)
        {
            OptionsMenu.SetActive(false);
        }
    }
    public void ContinueButton() {
        CloseMiniMenu();
    }
    public void OptionsButton()
    {
        if (!OptionsMenu.activeSelf)
        {
            OptionsMenu.SetActive(true);
        }
        else
        {
            OptionsMenu.SetActive(false);
        }

    }
    public void QuitButton(){
        Application.Quit();
    }
    #endregion

#region AcoesRaycast
    void IrAoDestino(Vector3 clickPos){
		agent.SetDestination (clickPos);
	}

    public int NoQueClicou() {
        if (Input.GetMouseButtonDown(0) && !TemMenuAtivo)
        {
            clickPosition = -Vector3.one;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, churrasqueira))
            {
                return 3;
            }
            else if (Physics.Raycast(ray, out hit, 100f, geladeira))
            {
                return 1;
            }
            else if (Physics.Raycast(ray, out hit, 100f, mesa))
            {
                return 4;
            }
            else if (Physics.Raycast(ray, out hit, 100f, ground))
            {
                return 2;
            }
            
        }
        return 0;
    }
    private void Acoes(int tipo) {
        if (tipo == 2)
        {            
            clickPosition = hit.point;
            IrAoDestino(clickPosition);
            GameObject part = Instantiate(PacParticle, new Vector3(clickPosition.x,clickPosition.y+2,clickPosition.z), Quaternion.identity) as GameObject;
            Destroy(part, 1.5f);
        }
        else if (tipo == 3)//churas    
        {                    
            IrAoDestino(Churrasqueira.Instance.localDeAcessoPlayer.transform.position);
            Churrasqueira.Instance.VerificacaoMenu();
        }
        else if (tipo == 1)//gela
        {
            IrAoDestino(Geladeira.Instance.localDeAcessoPlayer.transform.position);
            Geladeira.Instance.VerificacaoMenu();
        }
        else if(tipo == 4)//mesa
        {
            IrAoDestino(hit.transform.gameObject.GetComponent<Mesa>().localDeAcessoPlayer.transform.position);
            hit.transform.gameObject.GetComponent<Mesa>().mesaTrigger.SetActive(true);
            hit.transform.gameObject.GetComponent<Mesa>().VerificacaoMenu();
        }



    }
    #endregion

#region OnTriggers
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("churrasqueira"))
        {
            Churrasqueira.Instance.menuPodeAtivar = true;
        }
        else if (other.gameObject.tag.Equals("geladeira"))
        {
            Geladeira.Instance.menuPodeAtivar = true;
        }
        else if (other.gameObject.tag.Equals("mesa"))
        {
            other.gameObject.GetComponentInParent<Mesa>().menuPodeAtivar = true;            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "churrasqueira")
        {
            Churrasqueira.Instance.menuPodeAtivar = false;
        }
        else if (other.gameObject.tag == "geladeira")
        {
            Geladeira.Instance.menuPodeAtivar = false;
        }
        else if(other.gameObject.tag == "mesa")
        {
            other.gameObject.GetComponentInParent<Mesa>().menuPodeAtivar = false;
            other.gameObject.GetComponentInParent<Mesa>().mesaTrigger.SetActive(false);
        }
    }
#endregion
    public void GerenciarItem(GameObject item){
        Debug.Log("EntrouItem   " +item.name );
        //itemAtual=Geladeira.Instance.PegarItem(item);
        if(item.GetComponent<Bebida>()){
            if(!MecAlc.bebendo){
                MecAlc.CalculoAlcool(item);
                Debug.Log("Alcool");
            }else{
                //exibir mensagem
                return;
            }
        }
        else{
            if(!PlayerInventario.full){
                Debug.Log("PegandoCarne");
                PlayerInventario.AddInventario(item);
            }else{
                //exibir mensagem
                return;
            }
        }
        Geladeira.Instance.RemoverItem(item.GetComponent<Item>());
        Geladeira.Instance.DesativarMenu();
        
    }
#region GETSET
    public bool TemMenuAtivo
    {
        get
        {
            return temMenuAtivo;
        }

        set
        {
            temMenuAtivo = value;
        }
    }

#endregion

}
