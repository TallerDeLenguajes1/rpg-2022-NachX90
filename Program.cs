//Bienvenida e inicialización
Console.WriteLine("############### BIENVENIDO ###############");
Console.Write("Ingrese la cantidad de luchadores: ");
int cantidadDeJugadores = Convert.ToInt32(Console.ReadLine());

//Generación de jugadores
List<Personaje> ListaDeJugadores = new List<Personaje>();
for (int i = 0; i < cantidadDeJugadores; i++)
{
    Personaje Jugador = new Personaje();
    Jugador.CrearCaracteristicasAleatorias();
    Jugador.CrearDatosAleatorios();
    ListaDeJugadores.Add(Jugador);
}

//Muestro los jugadores y su orden
Console.WriteLine("\n##### LUCHADORES #####");
foreach (Personaje Jugador in ListaDeJugadores)
{
    Console.WriteLine($"\nJUGADOR {ListaDeJugadores.IndexOf(Jugador)+1}:");
    Jugador.MostrarPersonaje();
}

//Hago que peleen
Console.WriteLine("\n##### BATALLAS #####");
for (int batalla=1; batalla<cantidadDeJugadores; batalla++)
{
    Console.WriteLine($"\nBatalla número {batalla}:");
    var Jugador1 = ListaDeJugadores.ElementAt(0);
    var Jugador2 = ListaDeJugadores.ElementAt(1);
    int Perdedor = Pelea(Jugador1, Jugador2);
    ListaDeJugadores.RemoveAt(Perdedor);
}

//Muestro al ganador
Console.WriteLine($"\n##### GANADOR #####");
ListaDeJugadores.ElementAt(0).MostrarGanador();

//Fin del programa
Console.WriteLine("\nGracias por usar el programa. Tenga un buen día! :)");

//Pelea entre 2 personajes
/*Función para que dos personajes peleen entre sí 3 rondas. En caso de muerte gana el sobreviviente, sino el que tenga mayor vida*/
int Pelea(Personaje Jugador1, Personaje Jugador2)
{
    //Pelearán 3 veces, salvo que alguien muera
    int ronda = 0;
    while (ronda < 3 && Jugador1.Salud > 0 && Jugador2.Salud > 0)
    {
        Console.WriteLine($"  {(NumerosOrdinalesEnSingularFemenino)ronda} ronda:");
        //Atacará primero quien sea más veloz y tenga más suerte
        if (Jugador1.Velocidad * Jugador1.RodarDodecaedro() >= Jugador2.Velocidad * Jugador2.RodarDodecaedro())
        {
            Jugador1.Atacar(Jugador2);
            if (Jugador2.Salud > 0)
            {
                Jugador2.Atacar(Jugador1);
            }
        }
        else
        {
            Jugador2.Atacar(Jugador1);
            if (Jugador1.Salud > 0)
            {
                Jugador1.Atacar(Jugador2);
            }
        }
        ronda++;
    }

    //Se define y cura al ganador
    if (Jugador1.Salud > Jugador2.Salud) //Gana Jugador1
    {
        Jugador1.Descansar();
        Console.WriteLine($"  {Jugador1.Nombre} gana la batalla.");
        return 1;
    }
    else if (Jugador1.Salud < Jugador2.Salud) //Gana Jugador2
    {
        Jugador2.Descansar();
        Console.WriteLine($"  {Jugador2.Nombre} gana la batalla.");
        return 0;
    }
    else //Hay empate. Gana quien tenga más stats y suerte
    {
        Console.WriteLine($"  Hubo un empate! Gana quien tenga más stats y suerte!");
        int StatsJugador1 = (Jugador1.Velocidad + Jugador1.Destreza + Jugador1.Fuerza + Jugador1.Nivel + Jugador1.Armadura) * Jugador1.RodarDodecaedro();
        int StatsJugador2 = (Jugador2.Velocidad + Jugador2.Destreza + Jugador2.Fuerza + Jugador2.Nivel + Jugador2.Armadura) * Jugador2.RodarDodecaedro();
        if (StatsJugador1 > StatsJugador2)
        {
            Jugador1.Descansar();
            Console.WriteLine($"  {Jugador1.Nombre} gana la batalla.");
            return 1;
        }
        else
        {
            Jugador2.Descansar();
            Console.WriteLine($"  {Jugador2.Nombre} gana la batalla.");
            return 0;
        }
    }
}

enum NumerosOrdinalesEnSingularFemenino
{
    Primera,
    Segunda,
    Tercera
}
