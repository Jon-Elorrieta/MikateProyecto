using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckController : MonoBehaviour
{
    //Location de las cartas en el diseno del juego
    public List<GameObject> cardPrefabs;
    public List<GameObject> cardPrefabs2;
    public List<GameObject> cardPrefabs3;
    public List<GameObject> cardPrefabs4;
    public List<GameObject> cardMedio;
    public List<GameObject> deck;
  

    public List<ClickDetector> listaClickDetectors = new List<ClickDetector>();
   

    //Lista de todos los Sprites (Cartas)
    public List<Sprite> spriteList = new List<Sprite>();
    public SpriteRenderer spriteR;
    //Lista de las cartas repartidas en este caso 36
    public List<int> selectedIndices = new List<int>();

        //Listas de Spite, Imagenes de las cartas. Esta linkado con los GameObject para que se muestren por pantalla
       public List<Sprite> jugador1 = new List<Sprite>();
       public List<Sprite> jugador2 = new List<Sprite>();
       public List<Sprite> jugador3 = new List<Sprite>();
      public List<Sprite> jugador4 = new List<Sprite>();
      public List<Sprite> cartasSobrantes = new List<Sprite>();
        public List<Sprite> cartasMedio = new List<Sprite>();
    int cont=0;
    int cont2=0;
    int cont3=0;
    int cont4=0;
    int cont5 = 0;
    private int jugadorActualIndex = 0;
    private int jugadorActualCartasIndex = 0;
    private int turnoActual = 1;


    // Start is called before the first frame update
    void Start()
    {


        RellenarCartasYJugadores();
        SelectCartasSobrantes();
      
        CapturarClick();



    }

    void CapturarClick()
    {
        foreach (GameObject carta in cardPrefabs)
        {
            ClickDetector clickDetector = carta.GetComponent<ClickDetector>();
            if (clickDetector != null)
            {
                listaClickDetectors.Add(clickDetector);
            }
        }
        foreach (ClickDetector clickDetector in listaClickDetectors)
        {
            clickDetector.OnCartaClicked += OnCartaClicked;
        }

        foreach (GameObject carta2 in cardPrefabs2)
        {
            ClickDetector clickDetector = carta2.GetComponent<ClickDetector>();
            if (clickDetector != null)
            {
                listaClickDetectors.Add(clickDetector);
            }
        }
        foreach (ClickDetector clickDetector in listaClickDetectors)
        {
            clickDetector.OnCartaClicked += OnCartaClicked;
        }
    }

    private void OnCartaClicked(ClickDetector clickDetector)
    {
        //List<GameObject> cartasJugadorActual = GetCartasJugador(turnoActual);

        //Debug.Log("Se hizo click en la carta: " + clickDetector.name);

        Sprite SpriteCartaSelec = clickDetector.GetComponent<SpriteRenderer>().sprite;

        if (jugador1.Contains(clickDetector.GetComponent<SpriteRenderer>().sprite))
        {
            // Aquí puedes hacer lo que necesites con la carta que se hizo click
            if (clickDetector != null)
        {
           // clickDetector.Invoke(gameObject);
        // Agregar la carta seleccionada a la lista de cartas del medio
        cartasMedio.Add(SpriteCartaSelec);
        // Borrar la carta seleccionada de la mano del jugador
        jugador1.Remove(SpriteCartaSelec);
         clickDetector.GetComponent<SpriteRenderer>().sprite = null;
            }
            AvanzarTurno();
        }
        if (jugador2.Contains(clickDetector.GetComponent<SpriteRenderer>().sprite))
        {
            // Aquí puedes hacer lo que necesites con la carta que se hizo click
            if (clickDetector != null)
            {

                // clickDetector.Invoke(gameObject);
                // Agregar la carta seleccionada a la lista de cartas del medio
                cartasMedio.Add(SpriteCartaSelec);
                // Borrar la carta seleccionada de la mano del jugador
                jugador1.Remove(SpriteCartaSelec);
                clickDetector.GetComponent<SpriteRenderer>().sprite = null;
            }
            AvanzarTurno();
        }

    }
   
        
          

    private void AvanzarTurno()
    {
        turnoActual++;
        if (turnoActual > 4)
        {
            turnoActual = 1;
        }
    }
    private List<Sprite> GetJugadorActual(int turnoActual) {
    switch (turnoActual) {
        case 1:
            return jugador1;
        case 2:
            return jugador2;
        case 3:
            return jugador3;
        case 4:
            return jugador4;
        default:
            return null;
    }
}
    private List<GameObject> GetCartasJugador(int turnoActual)
    {
        switch (turnoActual)
        {
            case 1:
                return cardPrefabs;
            case 2:
                return cardPrefabs2;
            case 3:
                return cardPrefabs3;
            case 4:
                return cardPrefabs4;
            default:
                return null;
        }
    }
    



    void RellenarCartasYJugadores(){
      
        //Se rellenan las Listas de los jugadores con cartas random sin reperirse
        foreach (GameObject cardPrefab in cardPrefabs)
        {
            //GameObject cardLocation = Instantiate(cardPrefab);

            for (int i = 0; i < 4; i++){
            if (i % 4 == 0){
            jugador1.Add(SelectRandomSpriteSinRepetir());
            
            }else if (i % 4 == 1)
            {
            jugador2.Add(SelectRandomSpriteSinRepetir());
            }else if (i % 4 == 2)
            {
            jugador3.Add(SelectRandomSpriteSinRepetir());
            }
            else if (i % 4 == 3)
            {
            jugador4.Add(SelectRandomSpriteSinRepetir());
            }            
              }
 
         
        }



        ////Mostrar cartas por pantalla - Jugador 1, Abajo
        for (int i = 0; i < cardPrefabs.Count; i++)
        {
            GameObject cardLocation = cardPrefabs[i];
            cardLocation.GetComponent<SpriteRenderer>().sprite = jugador1[cont];
            cont = cont + 1;
        }
        ////Mostrar cartas por pantalla - Jugador 2, Arriba
        for (int i = 0; i < cardPrefabs2.Count; i++)
        {
            GameObject cardLocation2 = cardPrefabs2[i];
            cardLocation2.GetComponent<SpriteRenderer>().sprite = jugador2[cont2];
            cont2 = cont2 + 1;
        }
        ////Mostrar cartas por pantalla - Jugador 3, Izquierda
        for (int i = 0; i < cardPrefabs3.Count; i++)
        {
            GameObject cardLocation3 = cardPrefabs3[i];
            cardLocation3.GetComponent<SpriteRenderer>().sprite = jugador3[cont3];
            cont3 = cont3 + 1;
        }
        ////Mostrar cartas por pantalla - Jugador 4, Derecha
        for (int i = 0; i < cardPrefabs4.Count; i++)
        {
            GameObject cardLocation4 = cardPrefabs4[i];
            cardLocation4.GetComponent<SpriteRenderer>().sprite = jugador4[cont4];
            cont4 = cont4 + 1;
        }




    }

    public void ActualizarCartasMid()
    {
        if (cartasMedio.Count != 0)
        {
            GameObject cardLocation5 = cardMedio[0];
            cardLocation5.GetComponent<SpriteRenderer>().sprite = cartasMedio[cartasMedio.Count - 1];
        }
    }

    public void CargarCartas(){
      
        foreach (GameObject cardPrefab in cardPrefabs)
        {
            GameObject cardLocation = Instantiate(cardPrefab);
            cardLocation.GetComponent<SpriteRenderer>().sprite = SelectRandomSpriteSinRepetir();
            //spriteR.sprite = SelectRandomObject();
            //deck.Add(card);
        }  
    }

       public Sprite SelectRandomObject(){
        int randomIndex = Random.Range(0, spriteList.Count);
        return spriteList[randomIndex];      
    }

     public Sprite SelectRandomSpriteSinRepetir(){
     int selectedIndex = -1;
        do
        {
            selectedIndex = Random.Range(0, spriteList.Count);
        }
        while (selectedIndices.Contains(selectedIndex));

        selectedIndices.Add(selectedIndex);
        return spriteList[selectedIndex];
    }
    
   
    public void SelectCartasSobrantes(){
    for (int i = 0; i < spriteList.Count; i++)
    {
        if (!selectedIndices.Contains(i))
        {
            cartasSobrantes.Add(spriteList[i]);
        }
    }
     }

  


// Update is called once per frame
void Update()
    {
       ActualizarCartasMid();

        if (turnoActual == 1)
        {
            // Es el turno del jugador 1, espera a que seleccione una carta
            // y haga clic en ella en la función OnClickCarta()

        }
        else if(turnoActual == 2)
        {
            // Es el turno de un bot, selecciona una carta aleatoria
            List<Sprite> cartasJugadorActual = GetJugadorActual(turnoActual);
            List<GameObject> PoscartasJugadorActual = GetCartasJugador(turnoActual);

            //Si la carta del medio es un 4 ...
            if (cartasMedio[cartasMedio.Count - 1].name == "bastos4" | cartasMedio[cartasMedio.Count - 1].name == "oros4" | cartasMedio[cartasMedio.Count - 1].name == "copas4" | cartasMedio[cartasMedio.Count - 1].name == "espadas4")
           {
             
            //Debug.Log("Carta1: " + cartasJugadorActual[0].name + "Carta2: " + cartasJugadorActual[1].name + "Carta3: " + cartasJugadorActual[2].name);
           
            }

       
            //De momento selecciona una carta random de las 3 que tiene en la mano y hace click encima de una para que entre en la funcion ClickDetector y ponga la carta en el medio
            int indiceCartaSeleccionada = Random.Range(0, 3);          
            GameObject cartaSeleccionada = PoscartasJugadorActual[indiceCartaSeleccionada];
            cartaSeleccionada.GetComponent<ClickDetector>().OnMouseDown();

        }
        else if (turnoActual == 3)
        {

        }
        else if (turnoActual == 4)
            {

            }

    }
     
  
}

 

   

