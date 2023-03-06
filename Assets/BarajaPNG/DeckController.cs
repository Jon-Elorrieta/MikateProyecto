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

    public List<int> selectedIndicesTodas = new List<int>();

    //Listas de Spite, Imagenes de las cartas. Esta linkado con los GameObject para que se muestren por pantalla
    public List<Sprite> jugador1 = new List<Sprite>();
       public List<Sprite> jugador2 = new List<Sprite>();
       public List<Sprite> jugador3 = new List<Sprite>();
      public List<Sprite> jugador4 = new List<Sprite>();
      public List<Sprite> cartasSobrantes = new List<Sprite>();
        public List<Sprite> cartasMedio = new List<Sprite>();
    public List<Sprite> cartasMano = new List<Sprite>();
    int cont=0;
    int cont2=0;
    int cont3=0;
    int cont4=0;
    int cont5 = 0;
    private int jugadorActualIndex = 0;
    private int jugadorActualCartasIndex = 0;
    private int turnoActual = 1;

    public List<int> listaNumerosCartas = new List<int>(); //lista de números de cartas



// Start is called before the first frame update
void Start()
    {

        DisableBotCardsClickDetection();
        RellenarCartasYJugadores();
        SelectCartasSobrantes();
      
        CapturarClick();



    }

    void DisableBotCardsClickDetection()
    {
        //No funciona , hay que corregir esto.
        cardPrefabs[0].GetComponent<ClickDetector>().enabled = false;
        cardPrefabs[1].GetComponent<ClickDetector>().enabled = false;
        cardPrefabs[2].GetComponent<ClickDetector>().enabled = false;

        cardPrefabs2[0].GetComponent<ClickDetector>().enabled = false;
        cardPrefabs2[1].GetComponent<ClickDetector>().enabled = false;
        cardPrefabs2[2].GetComponent<ClickDetector>().enabled = false;

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
        ////Mostrar cartas por pantalla - Jugador 2, Izquierda
        for (int i = 0; i < cardPrefabs2.Count; i++)
        {
            GameObject cardLocation2 = cardPrefabs2[i];
            cardLocation2.GetComponent<SpriteRenderer>().sprite = jugador2[cont2];
            cont2 = cont2 + 1;
        }
        ////Mostrar cartas por pantalla - Jugador 3, Arriba
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


        for (int i = 0; i < spriteList.Count; i++)
        {
            listaNumerosCartas.Add(i + 1); //asigna un número consecutivo a cada carta
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

    //public void CargarCartas(){
      
    //    foreach (GameObject cardPrefab in cardPrefabs)
    //    {
    //        GameObject cardLocation = Instantiate(cardPrefab);
    //        cardLocation.GetComponent<SpriteRenderer>().sprite = SelectRandomSpriteSinRepetir();
    //        //spriteR.sprite = SelectRandomObject();
    //        //deck.Add(card);
    //    }  
    //}

    //   public Sprite SelectRandomObject(){
    //    int randomIndex = Random.Range(0, spriteList.Count);
    //    return spriteList[randomIndex];      
    //}

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

    int ObtenerNumeroCarta(Sprite carta)
    {
        switch (carta.name)
        {
            case "bastos1": return 1;
            case "bastos2": return 2;
            case "bastos3": return 3;
            case "bastos4": return 4;
            case "bastos5": return 5;
            case "bastos6": return 6;
            case "bastos7": return 7;
            case "bastos10": return 8;
            case "bastos11": return 9;
            case "bastos12": return 10;
            case "oros1": return 11;
            case "oros2": return 12;
            case "oros3": return 13;
            case "oros4": return 14;
            case "oros5": return 15;
            case "oros6": return 16;
            case "oros7": return 17;
            case "oros10": return 18;
            case "oros11": return 19;
            case "oros12": return 20;
            case "copas1": return 21;
            case "copas2": return 22;
            case "copas3": return 23;
            case "copas4": return 24;
            case "copas5": return 25;
            case "copas6": return 26;
            case "copas7": return 27;
            case "copas10": return 28;
            case "copas11": return 29;
            case "copas12": return 30;
            case "espadas1": return 31;
            case "espadas2": return 32;
            case "espadas3": return 33;
            case "espadas4": return 34;
            case "espadas5": return 35;
            case "espadas6": return 36;
            case "espadas7": return 37;
            case "espadas10": return 38;
            case "espadas11": return 39;
            case "espadas12": return 30;
            // Aquí se agregarían los casos para cada una de las cartas
            default: return 0; // En caso de que el nombre del Sprite no coincida con ninguna carta
        }
    }
    string ObtenerTextoCarta(int carta)
    {
        switch (carta)
        {
            case 1: return "bastos1";
            case 2: return "bastos2";
            case 3: return "bastos3";
            case 4: return "bastos4";
            case 5: return "bastos5";
            case 6: return "bastos6";
            case 7: return "bastos7";
            case 8: return "bastos10";
            case 9: return "bastos11";
            case 10: return "bastos12";
            case 11: return "oros1";
            case 12: return "oros2";
            case 13: return "oros3";
            case 14: return "oros4";
            case 15: return "oros5";
            case 16: return "oros6";
            case 17: return "oros7";
            case 18: return "oros10";
            case 19: return "oros11";
            case 20: return "oros12";
            case 21: return "copas1";
            case 22: return "copas2";
            case 23: return "copas3";
            case 24: return "copas4";
            case 25: return "copas5";
            case 26: return "copas6";
            case 27: return "copas7";
            case 28: return "copas10";
            case 29: return "copas11";
            case 30: return "copas12";
            case 31: return "espadas1";
            case 32: return "espadas2";
            case 33: return "espadas3";
            case 34: return "espadas4";
            case 35: return "espadas5";
            case 36: return "espadas6";
            case 37: return "espadas7";
            case 38: return "espadas10";
            case 39: return "espadas11";
            case 40: return "espadas12";
            // Aquí se agregarían los casos para cada una de las cartas
            default: return "no coincide con ninguna carta"; // En caso de que el nombre del Sprite no coincida con ninguna carta
        }
    }


   

    public int C1(int numeroCartaMedio, Sprite a, Sprite b, Sprite c)
    {
        int CartaMano1 = ObtenerNumeroCarta(a);
        int CartaMano2 = ObtenerNumeroCarta(b);
        int CartaMano3 = ObtenerNumeroCarta(c);
        bool cond = true;
        int TotCartas = cartasMedio.Count - 1;

        //Comparamos si la carta de la mano es el mismo numero pero de diferente palo y tira esa carta
        //Bastos 
        {
        if (numeroCartaMedio <= 10){

            if (CartaMano1 == numeroCartaMedio + 10)
            {
                return CartaMano1;
            }
            if (CartaMano2 == numeroCartaMedio + 10)
            {
                return CartaMano2;
            }
            if (CartaMano3 == numeroCartaMedio + 10)
            {
                return CartaMano3;
            }


            if (CartaMano1 == numeroCartaMedio + 20)
            {
                return CartaMano1;
            }
            if (CartaMano2 == numeroCartaMedio + 20)
            {
                return CartaMano2;
            }
            if (CartaMano3 == numeroCartaMedio + 20)
            {
                return CartaMano3;
            }

            if (CartaMano1 == numeroCartaMedio + 30)
            {
                return CartaMano1;
            }
            if (CartaMano2 == numeroCartaMedio + 30)
            {
                return CartaMano2;
            }
            if (CartaMano3 == numeroCartaMedio + 30)
            {
                return CartaMano3;
            }
        }
       
        //Oros
        if (numeroCartaMedio > 10 && numeroCartaMedio <= 20)
        {
            if (CartaMano1 == numeroCartaMedio - 10)
            {
                return CartaMano1;
            }
            if (CartaMano2== numeroCartaMedio - 10)
            {
                return CartaMano2;
            }
            if (CartaMano3 == numeroCartaMedio - 10)
            {
                 return CartaMano3;
            }

            if (CartaMano1 == numeroCartaMedio + 10)
            {
                return CartaMano1;
            }
            if (CartaMano2 == numeroCartaMedio + 10)
            {
                return CartaMano2;
            }
            if (CartaMano3 == numeroCartaMedio + 10)
            {
                return CartaMano3;
            }

            if (CartaMano1 == numeroCartaMedio + 20)
            {
                return CartaMano1;
            }
            if (CartaMano2 == numeroCartaMedio + 20)
            {
                return CartaMano2;
            }
            if (CartaMano3 == numeroCartaMedio + 20)
            {
                return CartaMano3;
            }

        }
           
            //Copas
        if (numeroCartaMedio > 20 && numeroCartaMedio <= 30)
        {
            if (CartaMano1 == numeroCartaMedio - 20)
            {
                return CartaMano1;
            }
            if (CartaMano2 == numeroCartaMedio - 20)
            {
                return CartaMano2;
            }
            if (CartaMano3 == numeroCartaMedio - 20)
            {
                return CartaMano3;
            }

            if (CartaMano1 == numeroCartaMedio - 10)
            {
                return CartaMano1;
            }
            if (CartaMano2 == numeroCartaMedio - 10)
            {
                return CartaMano2;
            }
            if (CartaMano3 == numeroCartaMedio - 10)
            {
                return CartaMano3;
            }

            if (CartaMano1 == numeroCartaMedio + 10)
            {
                return CartaMano1;
            }
            if (CartaMano2 == numeroCartaMedio + 10)
            {
                return CartaMano2;
            }
            if (CartaMano3 == numeroCartaMedio + 10)
            {
                return CartaMano3;
            }

        }
            
            //Espadas
         if (numeroCartaMedio > 30)
        {
            if (CartaMano1 == numeroCartaMedio - 10)
            {
                return CartaMano1;
            }
            if (CartaMano2 == numeroCartaMedio - 10)
            {
                return CartaMano2;
            }
            if (CartaMano3 == numeroCartaMedio - 10)
            {
                return CartaMano3;
            }

            if (CartaMano1 == numeroCartaMedio - 20)
            {
                return CartaMano1;
            }
            if (CartaMano2 == numeroCartaMedio - 20)
            {
                return CartaMano2;
            }
            if (CartaMano3 == numeroCartaMedio - 20)
            {
                return CartaMano3;
            }

            if (CartaMano1 == numeroCartaMedio - 30)
            {
                return CartaMano1;
            }
            if (CartaMano2 == numeroCartaMedio - 30)
            {
                return CartaMano2;
            }
            if (CartaMano3 == numeroCartaMedio - 30)
            {
                return CartaMano3;
            }
        }


            //Validamos si el numero del medio es un 1,2,3,4 ... y le decimos que tirar al bot
            //Bastos1 = 1 , Oros1 = 11, Copas1 = 21, Espadas1 = 31. Lo mismo con el resto de palos.
            //Bastos2 = 2 , Oros2 = 12, Copas2 = 22, Espadas2 = 32...
            if (numeroCartaMedio == 1 || numeroCartaMedio == 11 || numeroCartaMedio == 21 || numeroCartaMedio == 31)
            {
                if (CartaMano1 == 2 || CartaMano1 == 12 || CartaMano1 == 22 || CartaMano1 == 32)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 3 || CartaMano1 == 13 || CartaMano1 == 23 || CartaMano1 == 33)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 8 || CartaMano1 == 18 || CartaMano1 == 28 || CartaMano1 == 38)
                {
                    return CartaMano1;
                }


                if (CartaMano2 == 2 || CartaMano2 == 12 || CartaMano2 == 22 || CartaMano2 == 32)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 3 || CartaMano2 == 13 || CartaMano2 == 23 || CartaMano2 == 33)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 8 || CartaMano2 == 18 || CartaMano2 == 28 || CartaMano2 == 38)
                {
                    return CartaMano2;
                }


                if (CartaMano3 == 2 || CartaMano3 == 12 || CartaMano3 == 22 || CartaMano3 == 32)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 3 || CartaMano3 == 13 || CartaMano3 == 23 || CartaMano3 == 33)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 8 || CartaMano3 == 18 || CartaMano3 == 28 || CartaMano3 == 38)
                {
                    return CartaMano3;
                }



            }

            if (numeroCartaMedio == 2 || numeroCartaMedio == 12 || numeroCartaMedio == 22 || numeroCartaMedio == 32)
            {
                if (CartaMano1 == 1 || CartaMano1 == 11 || CartaMano1 == 21 || CartaMano1 == 31)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 2 || CartaMano1 == 12 || CartaMano1 == 22 || CartaMano1 == 32)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 3 || CartaMano1 == 13 || CartaMano1 == 23 || CartaMano1 == 33)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 4 || CartaMano1 == 14 || CartaMano1 == 24 || CartaMano1 == 34)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 5 || CartaMano1 == 15 || CartaMano1 == 25 || CartaMano1 == 35)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 6 || CartaMano1 == 16 || CartaMano1 == 26 || CartaMano1 == 36)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 7 || CartaMano1 == 17 || CartaMano1 == 27 || CartaMano1 == 37)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 8 || CartaMano1 == 18 || CartaMano1 == 28 || CartaMano1 == 38)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 9 || CartaMano1 == 19 || CartaMano1 == 29 || CartaMano1 == 39)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 10 || CartaMano1 == 20 || CartaMano1 == 30 || CartaMano1 == 40)
                {
                    return CartaMano1;
                }


                if (CartaMano2 == 1 || CartaMano2 == 11 || CartaMano2 == 21 || CartaMano2 == 31)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 2 || CartaMano2 == 12 || CartaMano2 == 22 || CartaMano2 == 32)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 3 || CartaMano2 == 13 || CartaMano2 == 23 || CartaMano2 == 33)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 4 || CartaMano2 == 14 || CartaMano2 == 24 || CartaMano2 == 34)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 5 || CartaMano2 == 15 || CartaMano2 == 25 || CartaMano2 == 35)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 6 || CartaMano2 == 16 || CartaMano2 == 26 || CartaMano2 == 36)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 7 || CartaMano2 == 17 || CartaMano2 == 27 || CartaMano2 == 37)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 8 || CartaMano2 == 18 || CartaMano2 == 28 || CartaMano2 == 38)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 9 || CartaMano2 == 19 || CartaMano2 == 29 || CartaMano2 == 39)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 10 || CartaMano2 == 20 || CartaMano2 == 30 || CartaMano2 == 40)
                {
                    return CartaMano2;
                }

                if (CartaMano3 == 1 || CartaMano3 == 11 || CartaMano3 == 21 || CartaMano3 == 31)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 2 || CartaMano3 == 12 || CartaMano3 == 22 || CartaMano3 == 32)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 3 || CartaMano3 == 13 || CartaMano3 == 23 || CartaMano3 == 33)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 4 || CartaMano3 == 14 || CartaMano3 == 24 || CartaMano3 == 34)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 5 || CartaMano3 == 15 || CartaMano3 == 25 || CartaMano3 == 35)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 6 || CartaMano3 == 16 || CartaMano3 == 26 || CartaMano3 == 36)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 7 || CartaMano3 == 17 || CartaMano3 == 27 || CartaMano3 == 37)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 8 || CartaMano3 == 18 || CartaMano3 == 28 || CartaMano3 == 38)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 9 || CartaMano3 == 19 || CartaMano3 == 29 || CartaMano3 == 39)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 10 || CartaMano3 == 20 || CartaMano3 == 30 || CartaMano3 == 40)
                {
                    return CartaMano3;
                }


            }
            if (numeroCartaMedio == 3 || numeroCartaMedio == 13 || numeroCartaMedio == 23 || numeroCartaMedio == 33)
            {
                if (CartaMano1 == 3 || CartaMano1 == 13 || CartaMano1 == 23 || CartaMano1 == 33)
                {
                    return CartaMano1;
                }
                if (CartaMano2 == 3 || CartaMano2 == 13 || CartaMano2 == 23 || CartaMano2 == 33)
                {
                    return CartaMano2;
                }
                if (CartaMano3 == 3 || CartaMano3 == 13 || CartaMano3 == 23 || CartaMano3 == 33)
                {
                    return CartaMano3;
                }
            }
            if (numeroCartaMedio == 4 || numeroCartaMedio == 14 || numeroCartaMedio == 24 || numeroCartaMedio == 34)
            {
                if (CartaMano1 == 1 || CartaMano1 == 11 || CartaMano1 == 21 || CartaMano1 == 31)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 2 || CartaMano1 == 12 || CartaMano1 == 22 || CartaMano1 == 32)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 3 || CartaMano1 == 13 || CartaMano1 == 23 || CartaMano1 == 33)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 4 || CartaMano1 == 14 || CartaMano1 == 24 || CartaMano1 == 34)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 5 || CartaMano1 == 15 || CartaMano1 == 25 || CartaMano1 == 35)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 6 || CartaMano1 == 16 || CartaMano1 == 26 || CartaMano1 == 36)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 7 || CartaMano1 == 17 || CartaMano1 == 27 || CartaMano1 == 37)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 8 || CartaMano1 == 18 || CartaMano1 == 28 || CartaMano1 == 38)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 9 || CartaMano1 == 19 || CartaMano1 == 29 || CartaMano1 == 39)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 10 || CartaMano1 == 20 || CartaMano1 == 30 || CartaMano1 == 40)
                {
                    return CartaMano1;
                }


                if (CartaMano2 == 1 || CartaMano2 == 11 || CartaMano2 == 21 || CartaMano2 == 31)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 2 || CartaMano2 == 12 || CartaMano2 == 22 || CartaMano2 == 32)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 3 || CartaMano2 == 13 || CartaMano2 == 23 || CartaMano2 == 33)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 4 || CartaMano2 == 14 || CartaMano2 == 24 || CartaMano2 == 34)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 5 || CartaMano2 == 15 || CartaMano2 == 25 || CartaMano2 == 35)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 6 || CartaMano2 == 16 || CartaMano2 == 26 || CartaMano2 == 36)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 7 || CartaMano2 == 17 || CartaMano2 == 27 || CartaMano2 == 37)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 8 || CartaMano2 == 18 || CartaMano2 == 28 || CartaMano2 == 38)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 9 || CartaMano2 == 19 || CartaMano2 == 29 || CartaMano2 == 39)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 10 || CartaMano2 == 20 || CartaMano2 == 30 || CartaMano2 == 40)
                {
                    return CartaMano2;
                }

                if (CartaMano3 == 1 || CartaMano3 == 11 || CartaMano3 == 21 || CartaMano3 == 31)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 2 || CartaMano3 == 12 || CartaMano3 == 22 || CartaMano3 == 32)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 3 || CartaMano3 == 13 || CartaMano3 == 23 || CartaMano3 == 33)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 4 || CartaMano3 == 14 || CartaMano3 == 24 || CartaMano3 == 34)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 5 || CartaMano3 == 15 || CartaMano3 == 25 || CartaMano3 == 35)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 6 || CartaMano3 == 16 || CartaMano3 == 26 || CartaMano3 == 36)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 7 || CartaMano3 == 17 || CartaMano3 == 27 || CartaMano3 == 37)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 8 || CartaMano3 == 18 || CartaMano3 == 28 || CartaMano3 == 38)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 9 || CartaMano3 == 19 || CartaMano3 == 29 || CartaMano3 == 39)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 10 || CartaMano3 == 20 || CartaMano3 == 30 || CartaMano3 == 40)
                {
                    return CartaMano3;
                }


            }
            if (numeroCartaMedio == 5 || numeroCartaMedio == 15 || numeroCartaMedio == 25 || numeroCartaMedio == 35)
            {
                if (CartaMano1 == 1 || CartaMano1 == 11 || CartaMano1 == 21 || CartaMano1 == 31)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 2 || CartaMano1 == 12 || CartaMano1 == 22 || CartaMano1 == 32)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 3 || CartaMano1 == 13 || CartaMano1 == 23 || CartaMano1 == 33)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 5 || CartaMano1 == 15 || CartaMano1 == 25 || CartaMano1 == 35)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 6 || CartaMano1 == 16 || CartaMano1 == 26 || CartaMano1 == 36)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 7 || CartaMano1 == 17 || CartaMano1 == 27 || CartaMano1 == 37)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 8 || CartaMano1 == 18 || CartaMano1 == 28 || CartaMano1 == 38)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 9 || CartaMano1 == 19 || CartaMano1 == 29 || CartaMano1 == 39)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 10 || CartaMano1 == 20 || CartaMano1 == 30 || CartaMano1 == 40)
                {
                    return CartaMano1;
                }


                if (CartaMano2 == 1 || CartaMano2 == 11 || CartaMano2 == 21 || CartaMano2 == 31)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 2 || CartaMano2 == 12 || CartaMano2 == 22 || CartaMano2 == 32)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 3 || CartaMano2 == 13 || CartaMano2 == 23 || CartaMano2 == 33)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 5 || CartaMano2 == 15 || CartaMano2 == 25 || CartaMano2 == 35)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 6 || CartaMano2 == 16 || CartaMano2 == 26 || CartaMano2 == 36)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 7 || CartaMano2 == 17 || CartaMano2 == 27 || CartaMano2 == 37)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 8 || CartaMano2 == 18 || CartaMano2 == 28 || CartaMano2 == 38)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 9 || CartaMano2 == 19 || CartaMano2 == 29 || CartaMano2 == 39)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 10 || CartaMano2 == 20 || CartaMano2 == 30 || CartaMano2 == 40)
                {
                    return CartaMano2;
                }

                if (CartaMano3 == 1 || CartaMano3 == 11 || CartaMano3 == 21 || CartaMano3 == 31)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 2 || CartaMano3 == 12 || CartaMano3 == 22 || CartaMano3 == 32)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 3 || CartaMano3 == 13 || CartaMano3 == 23 || CartaMano3 == 33)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 5 || CartaMano3 == 15 || CartaMano3 == 25 || CartaMano3 == 35)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 6 || CartaMano3 == 16 || CartaMano3 == 26 || CartaMano3 == 36)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 7 || CartaMano3 == 17 || CartaMano3 == 27 || CartaMano3 == 37)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 8 || CartaMano3 == 18 || CartaMano3 == 28 || CartaMano3 == 38)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 9 || CartaMano3 == 19 || CartaMano3 == 29 || CartaMano3 == 39)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 10 || CartaMano3 == 20 || CartaMano3 == 30 || CartaMano3 == 40)
                {
                    return CartaMano3;
                }


            }
            if (numeroCartaMedio == 6 || numeroCartaMedio == 16 || numeroCartaMedio == 26 || numeroCartaMedio == 36)
            {
                if (CartaMano1 == 1 || CartaMano1 == 11 || CartaMano1 == 21 || CartaMano1 == 31)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 2 || CartaMano1 == 12 || CartaMano1 == 22 || CartaMano1 == 32)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 3 || CartaMano1 == 13 || CartaMano1 == 23 || CartaMano1 == 33)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 6 || CartaMano1 == 16 || CartaMano1 == 26 || CartaMano1 == 36)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 7 || CartaMano1 == 17 || CartaMano1 == 27 || CartaMano1 == 37)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 8 || CartaMano1 == 18 || CartaMano1 == 28 || CartaMano1 == 38)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 9 || CartaMano1 == 19 || CartaMano1 == 29 || CartaMano1 == 39)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 10 || CartaMano1 == 20 || CartaMano1 == 30 || CartaMano1 == 40)
                {
                    return CartaMano1;
                }


                if (CartaMano2 == 1 || CartaMano2 == 11 || CartaMano2 == 21 || CartaMano2 == 31)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 2 || CartaMano2 == 12 || CartaMano2 == 22 || CartaMano2 == 32)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 3 || CartaMano2 == 13 || CartaMano2 == 23 || CartaMano2 == 33)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 6 || CartaMano2 == 16 || CartaMano2 == 26 || CartaMano2 == 36)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 7 || CartaMano2 == 17 || CartaMano2 == 27 || CartaMano2 == 37)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 8 || CartaMano2 == 18 || CartaMano2 == 28 || CartaMano2 == 38)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 9 || CartaMano2 == 19 || CartaMano2 == 29 || CartaMano2 == 39)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 10 || CartaMano2 == 20 || CartaMano2 == 30 || CartaMano2 == 40)
                {
                    return CartaMano2;
                }

                if (CartaMano3 == 1 || CartaMano3 == 11 || CartaMano3 == 21 || CartaMano3 == 31)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 2 || CartaMano3 == 12 || CartaMano3 == 22 || CartaMano3 == 32)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 3 || CartaMano3 == 13 || CartaMano3 == 23 || CartaMano3 == 33)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 6 || CartaMano3 == 16 || CartaMano3 == 26 || CartaMano3 == 36)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 7 || CartaMano3 == 17 || CartaMano3 == 27 || CartaMano3 == 37)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 8 || CartaMano3 == 18 || CartaMano3 == 28 || CartaMano3 == 38)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 9 || CartaMano3 == 19 || CartaMano3 == 29 || CartaMano3 == 39)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 10 || CartaMano3 == 20 || CartaMano3 == 30 || CartaMano3 == 40)
                {
                    return CartaMano3;
                }


            }
            if (numeroCartaMedio == 7 || numeroCartaMedio == 17 || numeroCartaMedio == 27 || numeroCartaMedio == 37)
            {
                if (CartaMano1 == 2 || CartaMano1 == 12 || CartaMano1 == 22 || CartaMano1 == 32)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 3 || CartaMano1 == 13 || CartaMano1 == 23 || CartaMano1 == 33)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 4 || CartaMano1 == 14 || CartaMano1 == 24 || CartaMano1 == 34)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 5 || CartaMano1 == 15 || CartaMano1 == 25 || CartaMano1 == 35)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 6 || CartaMano1 == 16 || CartaMano1 == 26 || CartaMano1 == 36)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 7 || CartaMano1 == 17 || CartaMano1 == 27 || CartaMano1 == 37)
                {
                    return CartaMano1;
                }

                if (CartaMano2 == 2 || CartaMano2 == 12 || CartaMano2 == 22 || CartaMano2 == 32)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 3 || CartaMano2 == 13 || CartaMano2 == 23 || CartaMano2 == 33)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 4 || CartaMano2 == 14 || CartaMano2 == 24 || CartaMano2 == 34)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 5 || CartaMano2 == 15 || CartaMano2 == 25 || CartaMano2 == 35)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 6 || CartaMano2 == 16 || CartaMano2 == 26 || CartaMano2 == 36)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 7 || CartaMano2 == 17 || CartaMano2 == 27 || CartaMano2 == 37)
                {
                    return CartaMano2;
                }

                if (CartaMano3 == 2 || CartaMano3 == 12 || CartaMano3 == 22 || CartaMano3 == 32)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 3 || CartaMano3 == 13 || CartaMano3 == 23 || CartaMano3 == 33)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 4 || CartaMano3 == 14 || CartaMano3 == 24 || CartaMano3 == 34)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 5 || CartaMano3 == 15 || CartaMano3 == 25 || CartaMano3 == 35)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 6 || CartaMano3 == 16 || CartaMano3 == 26 || CartaMano3 == 36)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 7 || CartaMano3 == 17 || CartaMano3 == 27 || CartaMano3 == 37)
                {
                    return CartaMano3;
                }


            }
            if (numeroCartaMedio == 10 || numeroCartaMedio == 20 || numeroCartaMedio == 30 || numeroCartaMedio == 40)
            {
                if (CartaMano1 == 1 || CartaMano1 == 11 || CartaMano1 == 21 || CartaMano1 == 31)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 2 || CartaMano1 == 12 || CartaMano1 == 22 || CartaMano1 == 32)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 3 || CartaMano1 == 13 || CartaMano1 == 23 || CartaMano1 == 33)
                {
                    return CartaMano1;
                }              
                if (CartaMano1 == 8 || CartaMano1 == 18 || CartaMano1 == 28 || CartaMano1 == 38)
                {
                    return CartaMano1;
                }
                if (CartaMano1 == 10 || CartaMano1 == 20 || CartaMano1 == 30 || CartaMano1 == 40)
                {
                    return CartaMano1;
                }

                if (CartaMano2 == 1 || CartaMano2 == 11 || CartaMano2 == 21 || CartaMano2 == 31)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 2 || CartaMano2 == 12 || CartaMano2 == 22 || CartaMano2 == 32)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 3 || CartaMano2 == 13 || CartaMano2 == 23 || CartaMano2 == 33)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 8 || CartaMano2 == 18 || CartaMano2 == 28 || CartaMano2 == 38)
                {
                    return CartaMano2;
                }
                if (CartaMano2 == 10 || CartaMano2 == 20 || CartaMano2 == 30 || CartaMano2 == 40)
                {
                    return CartaMano2;
                }

                if (CartaMano3 == 1 || CartaMano3 == 11 || CartaMano3 == 21 || CartaMano3 == 31)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 2 || CartaMano3 == 12 || CartaMano3 == 22 || CartaMano3 == 32)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 3 || CartaMano3 == 13 || CartaMano3 == 23 || CartaMano3 == 33)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 8 || CartaMano3 == 18 || CartaMano3 == 28 || CartaMano3 == 38)
                {
                    return CartaMano3;
                }
                if (CartaMano3 == 10 || CartaMano3 == 20 || CartaMano3 == 30 || CartaMano3 == 40)
                {
                    return CartaMano3;
                }


            }


       


        }
      
            return -1;       //Esta puesto por defecto en -1, me da error aqui necesito pasarle el nuermo de la carta selecionada en los if de arriba
        
    }
       

    

    public GameObject LogicaJuego(List<GameObject> PoscartasJugadorActual)
    {
        //GameObject CartaActualMedio = cartasMedio[cartasMedio.Count - 1];

        int numeroCartaMedio = ObtenerNumeroCarta(cartasMedio[cartasMedio.Count - 1]);

        //Sprite carta1 = cartasMedio[cartasMedio.Count - 1];
        ////Sprite carta = CartaActualMedio.GetComponent<SpriteRenderer>().sprite; 
        //int numeroCarta = cartasMedio.IndexOf(carta1) + 1;

      

        //CartaMano1,2,3
        Sprite a = PoscartasJugadorActual[0].GetComponent<SpriteRenderer>().sprite;
        Sprite b = PoscartasJugadorActual[1].GetComponent<SpriteRenderer>().sprite;
        Sprite c = PoscartasJugadorActual[2].GetComponent<SpriteRenderer>().sprite;

        string aName = PoscartasJugadorActual[0].GetComponent<SpriteRenderer>().sprite.name;
        string bName = PoscartasJugadorActual[1].GetComponent<SpriteRenderer>().sprite.name;
        string cName = PoscartasJugadorActual[2].GetComponent<SpriteRenderer>().sprite.name;

        int cartaRecibida = C1(numeroCartaMedio, a, b, c);

        Debug.Log(cartaRecibida);
     

        if (ObtenerTextoCarta(cartaRecibida) == aName)
        {
            Debug.Log(PoscartasJugadorActual[0]);
            return PoscartasJugadorActual[0];
        }else if (ObtenerTextoCarta(cartaRecibida) == bName)
        {
            Debug.Log(PoscartasJugadorActual[1]);
            return PoscartasJugadorActual[1];
        }else if (ObtenerTextoCarta(cartaRecibida) == cName)
        {
            Debug.Log(PoscartasJugadorActual[2]);
            return PoscartasJugadorActual[2];
        }
        else
        {
            return null;
        }


        
       

    //Tengo el numero de la carta recibida, necesito saber a que Sprite(Carta) que me estoy refiriendo para poder anadirlo a la list del medio


    //if (numeroCartaMedio == 1)
    //    {
    //        //CartaMedio 1 = Carta Alta sin efectos
           
    //    }
        //}else if (cartaMedio = 2){
        //    //CartaMedio 2 = Se puede tirar a todo menos al 3, carta comodin



        //}
        //else if (cartaMedio = 3)
        //{


        //}
        //else if (cartaMedio = 4)
        //{


        //}
        //else if (cartaMedio = 5)
        //{


        //}
        //else if (cartaMedio = 6)
        //{


        //}
        //else if (cartaMedio = 7)
        //{


        //}
        //else if (cartaMedio = 8)
        //{


        //}
        //else if (cartaMedio = 9)
        //{


        //}
        //else if (cartaMedio = 10)
        //{


        //}
    }
    //public void PensamientoBot(List <GameObject> PoscartasJugadorActual)
    //{
    //    Sprite aEncontrado;
    //    Sprite bEncontrado;
    //    Sprite cEncontrado;
    //    Sprite a = PoscartasJugadorActual[0].GetComponent<SpriteRenderer>().sprite;
    //    Sprite b = PoscartasJugadorActual[1].GetComponent<SpriteRenderer>().sprite;
    //    Sprite c = PoscartasJugadorActual[2].GetComponent<SpriteRenderer>().sprite;

    //    if (a > cartasMedio[cartasMedio.Count - 1].name)
    //    {
    //        return a;
    //    }else if (b > cartasMedio[cartasMedio.Count - 1].name)
    //    {
    //        return b;
    //    }else if (a > cartasMedio[cartasMedio.Count - 1].name)
    //    {
    //        return c;
    //    }

    //    foreach (GameObject cartaEnMedio in cartasEnMedio)
    //    {
    //        // Calcular la similitud entre la carta del bot y la carta en el medio
    //        float similitud = CalcularSimilitud(a, cartasMedio);

    //        // Actualizar la mejor carta si esta tiene mayor similitud
    //        if (similitud > mejorSimilitud)
    //        {
    //            mejorCarta = carta;
    //            mejorSimilitud = similitud;
    //        }
    //    }



    //}
    //float CalcularSimilitud(GameObject carta1, GameObject carta2)
    //{
    //    int valor1 = carta1.GetComponent<Carta>().valor;
    //    int valor2 = carta2.GetComponent<Carta>().valor;

    //    return Mathf.Abs(valor1 - valor2);
    //}



    // Update is called once per frame
    void Update()
    {
       ActualizarCartasMid();

        if (turnoActual == 1)
        {
            List<GameObject> ActivarClickJ1 = GetCartasJugador(turnoActual);
            ActivarClickJ1[0].GetComponent<ClickDetector>().enabled = true;
            ActivarClickJ1[1].GetComponent<ClickDetector>().enabled = true;
            ActivarClickJ1[2].GetComponent<ClickDetector>().enabled = true;
            // Es el turno del jugador 1, espera a que seleccione una carta
            // y haga clic en ella en la función OnClickCarta()

        }
        else if (turnoActual == 2)
        {
            // Es el turno de un bot, selecciona una carta aleatoria
            List<Sprite> cartasJugadorActual = GetJugadorActual(turnoActual);
            List<GameObject> PoscartasJugadorActual = GetCartasJugador(turnoActual);



            //PensamientoBot(PoscartasJugadorActual);
            GameObject cartaSeleccionada = LogicaJuego(PoscartasJugadorActual);
            cartaSeleccionada.GetComponent<ClickDetector>().OnMouseDown();

            //Si la carta del medio es un 4 ...
            // if (cartasMedio[cartasMedio.Count - 1].name == "bastos4" | cartasMedio[cartasMedio.Count - 1].name == "oros4" | cartasMedio[cartasMedio.Count - 1].name == "copas4" | cartasMedio[cartasMedio.Count - 1].name == "espadas4")
            //{

            // //Debug.Log("Carta1: " + cartasJugadorActual[0].name + "Carta2: " + cartasJugadorActual[1].name + "Carta3: " + cartasJugadorActual[2].name);

            // }


            //De momento selecciona una carta random de las 3 que tiene en la mano y hace click encima de una para que entre en la funcion ClickDetector y ponga la carta en el medio
            //int indiceCartaSeleccionada = Random.Range(0, 3);
            //GameObject cartaSeleccionada = PoscartasJugadorActual[indiceCartaSeleccionada];
            //cartaSeleccionada.GetComponent<ClickDetector>().OnMouseDown();

        }
        DisableBotCardsClickDetection();
        //else if (turnoActual == 3)
        //{

        //}
        //else if (turnoActual == 4)
        //    {

        //    }

    }
     
  
}

 

   

