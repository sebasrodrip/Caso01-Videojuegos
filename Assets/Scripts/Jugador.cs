using System;

[Serializable]
public class Jugador
{
    public int Puntos { get; private set; }

    public void Incrementar()
    {
        Puntos++;
    }

    public void Resetear()
    {
        Puntos = 0;
    }
}
