using System.Net;
using System.Text.Json;

List<Personaje> ListaDeLuchadores = new List<Personaje>();

/*************************
*** COMIENZO DEL JUEGO ***
*************************/
BienvenidaEIntroduccion();
MostrarGanadoresHistoricos();
CargarOGenerarLuchadores();

//Mostrar luchadores ordenados por nivel
Mensajes.Titulo("LUCHADORES");
PausaHastaTecla();
ListaDeLuchadores.Sort((l1, l2) => l1.Nivel.CompareTo(l2.Nivel));
foreach (Personaje Luchador in ListaDeLuchadores)
{
    Mensajes.TerminarLinea($"LUCHADOR {ListaDeLuchadores.IndexOf(Luchador) + 1}");
    Luchador.MostrarPersonaje();
    PausaHastaTecla();
}

//Batallas eliminatorias
Mensajes.Titulo("BATALLAS");
PausaHastaTecla();
int CantidadDeLuchadores = ListaDeLuchadores.Count;
for (int batalla = 1; batalla < CantidadDeLuchadores; batalla++)
{
    Mensajes.TerminarLinea($"Batalla número {batalla}");
    int Perdedor = Pelea(ListaDeLuchadores[0], ListaDeLuchadores[1]);
    ListaDeLuchadores.RemoveAt(Perdedor);
    PausaHastaTecla();
}
Mensajes.Titulo("FIN DE LAS BATALLAS!!!");
PausaHastaTecla();

//Mostrar y premiar ganador
Mensajes.Ganador();
PausaHastaTecla();
ListaDeLuchadores[0].MostrarGanador();
PausaHastaTecla();
Console.WriteLine();
Mensajes.CentrarTexto("¡Felicidades! Eres el nuevo dueño del TRONO DE HIERRO");
Mensajes.CentrarTexto("el Reino de Sliborg te jura lealtad");
Console.ForegroundColor = ConsoleColor.Red;
Mensajes.Titulo("¡¡¡LARGA VIDA AL REY!!!");
Console.ForegroundColor = ConsoleColor.White;
PausaHastaTecla();
ConectarALaApi();

//Guardar el ganador
string TextoAGuardar = $"{ListaDeLuchadores[0].Nombre},{ListaDeLuchadores[0].Nivel},{ListaDeLuchadores[0].BatallasGanadas}%TAB%TAB,{ListaDeLuchadores[0].Experiencia}%TAB,{DateTime.Today.ToShortDateString()}";
HelperDeArchivos.GuardarArchivoCsv("ganadores.csv", TextoAGuardar);

AgradecerYDespedir();


/************************
******* FUNCIONES *******
************************/

/*Función para que dos luchadores peleen entre sí 3 rondas, salvo que alguien muera antes. En caso de muerte gana el sobreviviente, sino el que tenga mayor vida. En caso de empate se define por una combinación de stats y suerte. Devuelve 0 si el ganador es Jugador1. Devuelve 1 si el ganador es Jugador2 */
int Pelea(Personaje Jugador1, Personaje Jugador2)
{
    Random aleatorio = new();
    //Pelearán 3 veces, salvo que alguien muera
    int ronda = 0;
    while (ronda < 3 && Jugador1.Salud > 0 && Jugador2.Salud > 0)
    {
        Console.WriteLine($"    ■ {(NumerosOrdinalesEnSingularFemenino)ronda} ronda:");
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
    string Ganador;
    int Perdedor;
    if (Jugador1.Salud > Jugador2.Salud) //Gana Jugador1
    {
        Jugador1.Descansar();
        Ganador = Jugador1.Nombre;
        Perdedor = 1;
    }
    else if (Jugador1.Salud < Jugador2.Salud) //Gana Jugador2
    {
        Jugador2.Descansar();
        Ganador = Jugador2.Nombre;
        Perdedor = 0;
    }
    else //Hay empate. Gana quien tenga más stats y suerte
    {
        Console.WriteLine($"\tHubo un empate! Gana quien tenga más stats y suerte!");
        int StatsJugador1 = (Jugador1.Velocidad + Jugador1.Destreza + Jugador1.Fuerza + Jugador1.Nivel + Jugador1.Armadura) * Jugador1.RodarDodecaedro();
        int StatsJugador2 = (Jugador2.Velocidad + Jugador2.Destreza + Jugador2.Fuerza + Jugador2.Nivel + Jugador2.Armadura) * Jugador2.RodarDodecaedro();
        if (StatsJugador1 > StatsJugador2)
        {
            Jugador1.Descansar();
            Ganador = Jugador1.Nombre;
            Perdedor = 1;
        }
        else
        {
            Jugador2.Descansar();
            Ganador = Jugador2.Nombre;
            Perdedor = 0;
        }
    }
    Console.WriteLine();
    Mensajes.CentrarTexto($"~~~ {Ganador} gana la batalla! ~~~");
    Mensajes.CentrarTexto($"Aquí tienes {Jugador1.Premios[aleatorio.Next(Jugador1.Premios.Count)]} por ganar la batalla ;)");
    return Perdedor;
}

/*Impide que el programa se siga ejecutando hasta que se presione una tecla. El caracter no es mostrado.*/
void PausaHastaTecla()
{
    Console.ReadKey(true);
}

/*Saluda al usuario, muestra una pequeña introducción y el nombre del programador.*/
void BienvenidaEIntroduccion()
{
    Mensajes.Bienvenido();
    PausaHastaTecla();
    Mensajes.CentrarTexto("SLIBORG es un reino oculto entre montañas en alguna parte del planeta tierra.");
    Mensajes.CentrarTexto("Por alguna razón, los minerales que componen estas montañas permitieron que las");
    Mensajes.CentrarTexto("criaturas que lo habitan tengan capacidades extraodinarias como fuerza extrema,");
    Mensajes.CentrarTexto("poderes mágicos y vidas más largas.\n");
    PausaHastaTecla();
    Mensajes.CentrarTexto("Para sorpresa de estos seres, el rey, quien no tenía herederos, murió tras una");
    Mensajes.CentrarTexto("enfermedad que desordenó su cabeza llevándolo a hacer cosas muy locas!");
    Mensajes.CentrarTexto("Su último deseo fue que el nuevo rey de Sliborg sea un habitante fuerte, con");
    Mensajes.CentrarTexto("destreza y suerte. Es por ello que organizó un torneo dónde los más hábiles se");
    Mensajes.CentrarTexto("enfrentarían hasta encontrar al nuevo rey.\n");
    PausaHastaTecla();
    Mensajes.CentrarTexto("Varios seres se presentaron y se decidió hacer una lista ordenándolos según la");
    Mensajes.CentrarTexto("cantidad de batallas que habían ganado en nombre del reino. A este número le");
    Mensajes.CentrarTexto("llamaron \"Nivel\".\n");
    PausaHastaTecla();
    Mensajes.CentrarTexto("El torneo consiste en batallas entre 2 jugadores. Cada batalla tiene un máximo");
    Mensajes.CentrarTexto("de 3 rondas en las cuales los jugadores harán de atacante y defensor, salvo");
    Mensajes.CentrarTexto("que alguno se debilite anteriormente. Ganará quien tenga mayor salud al");
    Mensajes.CentrarTexto("finalizar la batalla. En caso de que ambos jugadores tengan la misma salud, el");
    Mensajes.CentrarTexto("ganador se definirá según la suma de sus Stats multiplicado por el valor de un");
    Mensajes.CentrarTexto("dado Dodecaedro lanzado al azar (valores del 1 al 12)\n");
    PausaHastaTecla();
    Mensajes.CentrarTexto("¡¡¡¿¿¿Un dado???!!!\n");
    PausaHastaTecla();
    Mensajes.CentrarTexto("Si, ¡el rey estaba loco! ¡Pero esto no es todo! Además ordenó darle al ganador");
    Mensajes.CentrarTexto("de cada batalla un objeto al azar de la \"Caja de Pandora\". Una caja con");
    Mensajes.CentrarTexto("objetos pensados especialmente para este torneo. ¡Algunos serán muy útiles!,");
    Mensajes.CentrarTexto("otros no tanto, y como es esperar, algunos completamente inútiles.\n");
    PausaHastaTecla();
    Mensajes.CentrarTexto("¿Verdad que estaba loco no? JAJAJA\n");
    PausaHastaTecla();
    Mensajes.CentrarTexto("Les deseamos la mayor de las suertes a estos valientes luchadores!");
    PausaHastaTecla();
    Mensajes.Titulo("Rey de Sliborg - Juego de roles | por Javier Nacchio");
    PausaHastaTecla();
    Mensajes.CentrarTexto("Trabajo final integrador de la materia Taller de Lenguajes I");
    Mensajes.CentrarTexto("Facultad de Ciencias Exactas y Tecnología - Universidad Nacional de Tucumán");
    PausaHastaTecla();
}

/*Muestra un historal de ganadores si el archivo existe y el usuario lo permite. En caso de no existir el archivo, se omite la lectura para evitar errores.*/
void MostrarGanadoresHistoricos()
{
    string opcion;
    if (File.Exists("ganadores.csv"))
    {
        do
        {
            Mensajes.TerminarLinea("Ingrese una opción:");
            Console.WriteLine("\t[1] Ver historial de ganadores");
            Console.WriteLine("\t[2] Jugar");
            Console.Write("Esperando entrada: ");
            opcion = Console.ReadLine();
        } while (opcion != "1" && opcion != "2");
    }
    else
    {
        opcion = "2";
    }
    switch (opcion)
    {
        case "1":
            //Mostrar historial de ganadores
            Mensajes.TerminarLinea("Historial de ganadores");
            Console.WriteLine("Nombre\t\t|Nivel\t|Batallas Ganadas\t|Experiencia\t|Fecha");
            string TextoAMostrar = HelperDeArchivos.AbrirArchivoCsv("ganadores.csv");
            TextoAMostrar = TextoAMostrar.Replace(",", "\t|");
            Console.WriteLine(TextoAMostrar.Replace("%TAB", "\t"));
            PausaHastaTecla();
            break;
        default:
            break;
    }
}

/*Permite al usuario seleccionar si quiere crear nuevos luchadores o usar los últimos cargados.*/
void CargarOGenerarLuchadores()
{
    string opcion;
    if (File.Exists("jugadores.json"))
    {
        do
        {
            Mensajes.TerminarLinea("Ingrese una opción:");
            Console.WriteLine("\t[1] Cargar últimos luchadores");
            Console.WriteLine("\t[2] Generar nuevos luchadores");
            Console.Write("Esperando entrada: ");
            opcion = Console.ReadLine();
        } while (opcion != "1" && opcion != "2");
    }
    else
    {
        opcion = "2";
    }
    switch (opcion)
    {
        case "1":
            //Lectura de jugadores
            string TextoLeido = HelperDeArchivos.AbrirArchivoJson("jugadores.json");
            ListaDeLuchadores = JsonSerializer.Deserialize<List<Personaje>>(TextoLeido);
            Console.WriteLine($"Se cargaron {ListaDeLuchadores.Count} luchadores");
            break;
        case "2":
            //Generación de jugadores
            Console.Write("\nIngrese la cantidad de luchadores: ");
            int cantidadDeJugadores = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < cantidadDeJugadores; i++)
            {
                Personaje Jugador = new Personaje();
                Jugador.CrearCaracteristicasAleatorias();
                Jugador.CrearDatosAleatorios();
                ListaDeLuchadores.Add(Jugador);
            }
            //Guardado
            string ListaSerializadaAGuardar = JsonSerializer.Serialize(ListaDeLuchadores);
            HelperDeArchivos.GuardarArchivoJson("jugadores.json", ListaSerializadaAGuardar);
            break;
        default:
            Console.WriteLine("Ups! Parece que hubo un error :(");
            break;
    }
}

/*Conecta con la api de FreeToGame para mostrarle un juego al ganador.*/
static void ConectarALaApi()
{
    Random aleatorio = new();
    var Url = @"https://www.freetogame.com/api/games";
    var Conexion = (HttpWebRequest)WebRequest.Create(Url);
    Conexion.Method = "GET";
    Conexion.ContentType = "application/json";
    Conexion.Accept = "application/json";

    Console.WriteLine();
    Mensajes.CentrarTexto("Aquí te dejamos una copia de un juego gratuito para que te diviertas un rato :D");
    Mensajes.CentrarTexto("Si, gratuito. El dólar está caro :( Pero eso no nos impide divertirnos! ;)");
    Console.WriteLine();

    try
    {
        using (WebResponse RespuestaWeb = Conexion.GetResponse())
        {
            using (Stream StreamWeb = RespuestaWeb.GetResponseStream())
            {
                if (StreamWeb == null) return;
                using (StreamReader SR = new StreamReader(StreamWeb))
                {
                    string TextoLeido = SR.ReadToEnd();
                    var ListaDeJuegosGratuitos = JsonSerializer.Deserialize<List<JuegoGratuito>>(TextoLeido);
                    ListaDeJuegosGratuitos[aleatorio.Next(ListaDeJuegosGratuitos.Count)].MostrarJuego();
                }
            }
        }
    }
    catch (WebException Excepcion)
    {
        Mensajes.CentrarTexto("Ups! Parece que no hay premio para ti! xD (no se pudo acceder a la API)");
    }
}

/*Agradece al usuario y lo despide.*/
void AgradecerYDespedir()
{
    PausaHastaTecla();
    Mensajes.Titulo("Gracias por usar el programa. Tenga un buen día! :D");
    Mensajes.CentrarTexto("Presione una tecla para salir");
    PausaHastaTecla();
}

enum NumerosOrdinalesEnSingularFemenino
{
    Primer,
    Segunda,
    Tercer
}
