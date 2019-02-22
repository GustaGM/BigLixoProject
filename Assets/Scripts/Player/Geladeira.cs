using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Geladeira : Interactive {

    //
    private static int carneQTD = 10;
    //UI
    public GameObject button;
    public GameObject panel;
    /*CERVEJA,VODKA,WHISKY,VINHO,CACHACA,TEQUILA,SAKE,RUM */
    [SerializeField]
    private Dictionary<Item, int> itens = new Dictionary<Item, int>();
    private List<GameObject> botoes = new List<GameObject>();
    
    static Geladeira instance;
    public static Geladeira Instance {get{return instance;}}
    
    void Start () {
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DesativarMenu();
        }
        UIChange();
        


    }
#region MecanicasAddRev
    public void AdicionarItem(Item item) {
        //Debug.Log(bebida.GetComponent<Bebida>().nome);
        if(!itens.ContainsKey(item)){
            Debug.Log("Adicionou " + item.name);
            itens.Add(item, 1);
        }
        else{
            
            itens[item]++;
            //Debug.Log("Aumentou " + item.name + " valor: " + itens[item]);
        }
    }

    public void RemoverItem(Item item) {
        if(!itens.ContainsKey(item)){
            return;
        }
        
        itens[item]--;
        
        if(itens[item] < 1){
            itens.Remove(item);
        }
    }
    public GameObject PegarItem(GameObject item){
            return(item);
    }

     
     
    
#endregion
#region Ui

    public override void VerificacaoMenu() {
        CreateButtons();
        base.VerificacaoMenu();
    }
    private void UIChange()
    {
        //cervejaTEXT.text = bebidaQTD.ToString();
        //carneTEXT.text = carneQTD.ToString();
    }
    private void CreateButtons(){
        UndoButtons();
        botoes=new List<GameObject>();
        foreach(KeyValuePair<Item, int> kvp in itens){
           
            GameObject chave = kvp.Key.gameObject;
            int qtd = kvp.Value;
            Debug.Log(chave.name + "  " + qtd);
            GameObject botao = Instantiate(button,Vector3.zero,Quaternion.identity) as GameObject;
            botao.name = chave.name;
            botao.GetComponentsInChildren<Text>()[0].text=chave.name;
            botao.GetComponentsInChildren<Text>()[1].text=qtd.ToString();
            botao.transform.SetParent(panel.transform);
            botao.GetComponent<Button>().onClick.AddListener(delegate {PAC.Instance.GerenciarItem(chave);});
            botoes.Add(botao);


            /*
            for(int j=0;j<botoes.Count;j++){
                if(itens[i].name==botoes[j].name){
                    GameObject lastbutton = Instantiate(button,new Vector3(transform.position.x,transform.position.y, transform.position.z),Quaternion.identity) as GameObject;
                    lastbutton.name=itens[i].name;
                    lastbutton.GetComponentInChildren<Text>().text=itens[i].name;
                    lastbutton.transform.SetParent(panel.transform);
                    lastbutton.GetComponent<Button>().onClick.AddListener(delegate {PAC.Instace.GerenciarItem(itens[i]);});
                    botoes.Add(lastbutton);
                }

            }
            */
        }



        /* 
        for(int i=0;i<itens.Count;i++){
            GameObject lastbutton = Instantiate(button,new Vector3(transform.position.x,transform.position.y, transform.position.z),Quaternion.identity) as GameObject;
            lastbutton.name=itens[i].name;
            lastbutton.GetComponentInChildren<Text>().text=itens[i].name;
            lastbutton.transform.parent=panel.transform;
            Debug.Log(i);
            lastbutton.GetComponent<Button>().onClick.AddListener(delegate {PAC.Instace.GerenciarItem(itens[i]);});
            if(botoes.Find(x=> x.name.Contains(lastbutton.name))){
                Destroy(lastbutton);
            }
            else{
                botoes.Add(lastbutton);
            }
        }*/


    }
    private void UndoButtons(){
        for (int i=botoes.Count-1;i>=0;i--){
            GameObject x=botoes[i];
            botoes.Remove(botoes[i]);
            Destroy(x);
        }
    }
    public override void DesativarMenu(){
        UndoButtons();
        base.DesativarMenu();
    }
    #endregion
}
