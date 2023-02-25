using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public string nombre;
    public List<Sprite> cartasSeleccionadas = new List<Sprite>();
    // Start is called before the first frame update

    public Jugador(string nombre)
    {
        this.nombre = nombre;
    }
}
/*

    public class SpriteSelector : MonoBehaviour{
    public Sprite[] spriteList;
    private List<int> selectedIndices = new List<int>();
    private List<Jugador> jugadores = new List<Jugador>();

    public Sprite SelectRandomSprite(Jugador jugador)
    {
        int selectedIndex = -1;
        do
        {
            selectedIndex = Random.Range(0, spriteList.Length);
        }
        while (selectedIndices.Contains(selectedIndex));

        selectedIndices.Add(selectedIndex);
        jugador.cartasSeleccionadas.Add(spriteList[selectedIndex]);
        return spriteList[selectedIndex];
    }

    public void AddJugador(Jugador jugador)
    {
        jugadores.Add(jugador);
    }
   
} */