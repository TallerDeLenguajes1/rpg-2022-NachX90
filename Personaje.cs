public class Personaje
{
    Random aleatorio = new Random();

    //Listas de datos
    public List<string> Tipos = new List<string>()
    {
        "Orco",
        "Mago",
        "Enano",
        "Elfo"
    };
    public List<string> Apodos = new List<string>()
    {
        "Luronk",
        "Lugrub",
        "Yam",
        "Ouhgan",
        "Gularzob",
        "Hagob",
        "Narfu",
        "Pretkag",
        "Golub",
        "Nughilug",
        "Buimghig",
        "Kagan",
        "Xoruk",
        "Haguk",
        "Xnaurl",
        "Lugdum",
        "Shura",
        "Fupgugh",
        "Nash",
        "Khor"
    };

    //Función auxiliar extraída del TP6
    int CalcularEdad()
    {
        DateTime FechaActual = DateTime.Now;
        if (FechaDeNacimiento.Month >= FechaActual.Month && FechaDeNacimiento.Day > FechaActual.Day)
        {
            return (FechaActual.Year - FechaDeNacimiento.Year - 1);
        }
        else
        {
            return (FechaActual.Year - FechaDeNacimiento.Year);
        }
    }

    //Función para crear una fecha aleatoria entre 2 fechas
    DateTime NacerAlgunDia()
    {
        FechaDeNacimiento = new DateTime(1723, 1, 1);                               //Fijo la menor fecha posible
        int MaximoSumaFecha = (DateTime.Today - FechaDeNacimiento).Days;            //Máximo número de días a sumar
        FechaDeNacimiento = FechaDeNacimiento.AddDays(aleatorio.Next(MaximoSumaFecha));   //Sumo un número aleatorio de días a la fecha base
        return (FechaDeNacimiento);
    }

    //Datos del personaje
    public int Velocidad;           //aleatorio 1~10
    public int Destreza;            //aleatorio 1~5
    public int Fuerza;              //aleatorio 1~10
    public int Nivel;               //aleatorio 1~10
    public int Armadura;            //aleatorio 1~10
    public string? Nombre;          //a elección
    string? Tipo;                   //un valor de la lista "Tipos"
    string? Apodo;                  //un valor de la lista "Apodos"
    DateTime FechaDeNacimiento;     //aleatorio entre 1/1/1723 y la fecha actual
    int Edad;                       //automático 0~300
    public int Salud = 100;         //100
    public int Experiencia = 0;     //experiencia inicial
    public int BatallasGanadas = 0; //no necesita explicación xD

    //Generador de personaje
    public void CrearCaracteristicasAleatorias()
    {
        Velocidad = aleatorio.Next(1,11);
        Destreza = aleatorio.Next(1,6);
        Fuerza = aleatorio.Next(1,11);
        Nivel = aleatorio.Next(1,11);
        Armadura = aleatorio.Next(1,11);
    }
    public void CrearDatosAleatorios()
    {
        Console.Write("Ingrese el nombre del luchador: ");
        Nombre = Console.ReadLine();
        Tipo = Tipos[aleatorio.Next(0,Tipos.Count)];
        Apodo = Apodos[aleatorio.Next(0,Apodos.Count)];
        FechaDeNacimiento = NacerAlgunDia();
        Edad = CalcularEdad();
    }

    //Mostrar personaje
    public void MostrarPersonaje()
    {
        Console.WriteLine($"  Nombre:\t\t{Nombre}");
        Console.WriteLine($"  Tipo:\t\t\t{Tipo}");
        Console.WriteLine($"  Apodo:\t\t{Apodo}");
        Console.WriteLine($"  Fecha de Nacimiento:\t{FechaDeNacimiento.ToShortDateString()}");
        Console.WriteLine($"  Edad:\t\t\t{Edad} años");
        Console.WriteLine($"  Fuerza:\t\t{Fuerza} ptos");
        Console.WriteLine($"  Destreza:\t\t{Destreza} ptos");
        Console.WriteLine($"  Velocidad:\t\t{Velocidad} ptos");
        Console.WriteLine($"  Armadura:\t\t{Armadura} ptos");
        Console.WriteLine($"  Nivel:\t\t{Nivel}");
    }

    //Función de lanzar dado
    public int RodarDodecaedro()
    {
        return (aleatorio.Next(1, 21));
    }

    //Un ataque de un jugador a otro
    public void Atacar(Personaje Defensor)
    {
        Console.Write($"    {Nombre} ataca a {Defensor.Nombre}. ");
        int PoderDeDisparo = Destreza * Fuerza * Nivel;
        int EfectividadDeDisparo = aleatorio.Next(1, 101);
        int ValorDeAtaque = PoderDeDisparo * EfectividadDeDisparo / 100;
        int PoderDeDefensa = Defensor.Armadura * Defensor.Velocidad;
        int MaximoDanoProvocable = 4;
        int DanoProvocado = (ValorDeAtaque - PoderDeDefensa) / MaximoDanoProvocable;
        if (DanoProvocado > 0)
        {
            if (DanoProvocado >= 60)
            {
                Console.Write("Golpe crítico! ");
            }
            Console.WriteLine($"{Defensor.Nombre} pierde {DanoProvocado} ptos. de vida.");
            Experiencia += DanoProvocado;
            Defensor.Salud -= DanoProvocado;
            if (Defensor.Salud <= 0)
            {
                Console.WriteLine($"    {Defensor.Nombre} no puede continuar.");
            }
        }
        else
        {
            Console.WriteLine($"{Defensor.Nombre} esquivó el golpe.");
        }
    }

    //Cura al ganador de la batalla y mejora sus stats
    public void Descansar()
    {
        if (Salud < 85)
        {
            Salud += 15;
        }
        else
        {
            Salud = 100;
        }
        if (Nivel < 10)
        {
            Nivel++;
        }
        BatallasGanadas++;
    }
    public void MostrarGanador()
    {
        MostrarPersonaje();
        Console.WriteLine($"  Salud:\t\t{Salud} %");
        Console.WriteLine($"  Experiencia:\t\t{Experiencia} px");
        Console.WriteLine($"  Batallas ganadas:\t{BatallasGanadas}");
    }
}
