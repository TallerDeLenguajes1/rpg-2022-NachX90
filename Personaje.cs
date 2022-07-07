using System.Text.Json.Serialization;
public class Personaje
{
    Random aleatorio = new Random();

    //Listas de datos
    public List<string> Nombres = new List<string>()
    {
        "Shadowsnarl",
        "Battleblow",
        "Gold Scream",
        "Silentfury",
        "Grand Bane",
        "Sharpheart",
        "Phoenix Pelt",
        "Iron Mane",
        "Phoenix Hammer",
        "Hellroar",
        "Dragon Blood",
        "Dragonshadow",
        "Wild Rage",
        "Dead Roar",
        "Rockscar",
        "Nightstare",
        "Thunderhand",
        "Deadchaser",
        "Red Sworn",
        "Embergaze",
        "Silverpelt",
        "Night Hand",
        "Moltenblow",
        "Bloodlust",
        "Moltenhand",
        "Proud Sorrow",
        "Firesorrow",
        "Nighthide",
        "Reckless",
        "Ironsword",
        "Wolf Strike",
        "Ironblade",
        "Goregrim",
        "Raven Snarl",
        "Swiftsorrow",
        "Phoenixrage",
        "Vengeful",
        "Eternal Hunger",
        "Ember Sworn",
        "Storm Sworn",
        "Stouthair",
        "Wolf Sword",
        "Phoenix Blow",
        "Grandmane",
        "Fireshadow",
        "Rockthorn",
        "Steelvisage",
        "Rock Scar",
        "Bear Pelt",
        "Swift Bolt",
        "Rage Brow",
        "Lightmight",
        "Brightsorrow",
        "Death Visage",
        "Shadowstare",
        "Greathide",
        "Battle Fury",
        "Silentgaze",
        "Giantstride",
        "Fuseblade",
        "Blood Might",
        "Single Head",
        "Bear Stare",
        "Fire Hand",
        "Frostheart",
        "Rumblefang",
        "Rage Grimace",
        "Bear Head",
        "Great Gaze",
        "Steel Might",
        "Stout Flayer",
        "Death Heart",
        "Frostblood",
        "Spiritroar",
        "Moltensnarl",
        "Bright Snarl",
        "Gold Fury",
        "Warsnarl",
        "Stone Fist",
        "Eternal Hunger",
        "Swift Head",
        "Molten Sorrow",
        "Demonsong",
        "Giant Bow",
        "Dragon Cleaver",
        "Singlebolt",
        "Stoneshield",
        "Shieldheart",
        "Ragescar",
        "Heartless",
        "Shieldcleaver",
        "Stone Thorn",
        "Dragonrage",
        "Fist Grim",
        "Ravenrage",
        "Ironshield",
        "Dragonblade",
        "Silenthand"
    };
    public List<string> Tipos = new List<string>()
    {
        "Orco",
        "Mago",
        "Elfo",
        "Guerrero",
        "Demonio",
        "Dragón",
        "Aldeano",
        "Hada",
        "Enano",
        "Centauro"
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
    public List<string> Premios = new List<string>()
    {
        "una manzana verde",
        "un durazno podrido",
        "una porción de torta",
        "un zapallo amarillo",
        "una botella de vino",
        "una curita",
        "una cortadora de plasma",
        "un arco explosivo",
        "un segador de almas",
        "una espada maestra",
        "una linterna (las pilas vienen por separado)",
        "un oso de felpa",
        " una palanca",
        "una bomba",
        "un generador de portales",
        "una Espada del Dragón",
        "un silbato",
        "una Espada del Caos",
        "un Sable de Luz Oscuro",
        "una motosierra",
        "un Manipulador de Masas de Energía de Punto Cero",
        "una pistola de rayos láser",
        "un muñeco vudú",
        "un martillo",
        "un látigo",
        "una espada mellada",
        "una varita mágica",
        "un escudo de madera",
        "una guadaña",
        "un ibuprofeno",
        "una Espada de Energía",
        "una lanza",
        "un sombrero de Michael Jackson",
        "una minigun",
        "un cuchillo",
        "dos milanesas de berenjena",
        "una porción de pizza",
        "una Machine Gun",
        "un kilo de pan duro"
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
        FechaDeNacimiento = new DateTime(1723, 1, 1);                                   //Fijo la menor fecha posible
        int MaximoSumaFecha = (DateTime.Today - FechaDeNacimiento).Days;                //Fijo la máxima cantidad de días a sumar
        FechaDeNacimiento = FechaDeNacimiento.AddDays(aleatorio.Next(MaximoSumaFecha)); //Sumo un número aleatorio de días a la fecha base
        return (FechaDeNacimiento);
    }

    //Datos del personaje
    public int Velocidad { get; set; }                  //aleatorio 1~10
    public int Destreza { get; set; }                   //aleatorio 1~5
    public int Fuerza { get; set; }                     //aleatorio 1~10
    public int Nivel { get; set; }                      //aleatorio 1~10
    public int Armadura { get; set; }                   //aleatorio 1~10
    public string? Nombre { get; set; }                 //a elección
    public string? Tipo { get; set; }                   //un valor de la lista "Tipos"
    public string? Apodo { get; set; }                  //un valor de la lista "Apodos"
    public DateTime FechaDeNacimiento { get; set; }     //aleatorio entre 1/1/1723 y la fecha actual
    public int Edad { get; set; }                       //automático 0~300
    public int Salud = 100;                             //100
    public int Experiencia = 0;                         //experiencia inicial
    public int BatallasGanadas = 0;                     //no necesita explicación xD

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
        Nombre = Nombres[aleatorio.Next(Nombres.Count)];
        Tipo = Tipos[aleatorio.Next(Tipos.Count)];
        Apodo = Apodos[aleatorio.Next(Apodos.Count)];
        FechaDeNacimiento = NacerAlgunDia();
        Edad = CalcularEdad();
    }

    //Mostrar personaje
    public void MostrarPersonaje()
    {
        Console.WriteLine($"\tNombre:\t\t\t{Nombre}");
        Console.WriteLine($"\tTipo:\t\t\t{Tipo}");
        Console.WriteLine($"\tApodo:\t\t\t{Apodo}");
        Console.WriteLine($"\tFecha de Nacimiento:\t{FechaDeNacimiento.ToShortDateString()}");
        Console.WriteLine($"\tEdad:\t\t\t{Edad} años");
        Console.WriteLine($"\tFuerza:\t\t\t{Fuerza} ptos");
        Console.WriteLine($"\tDestreza:\t\t{Destreza} ptos");
        Console.WriteLine($"\tVelocidad:\t\t{Velocidad} ptos");
        Console.WriteLine($"\tArmadura:\t\t{Armadura} ptos");
        Console.WriteLine($"\tNivel:\t\t\t{Nivel}");
    }

    //Función de lanzar dado
    public int RodarDodecaedro()
    {
        return (aleatorio.Next(1, 13));
    }

    //Un ataque de un jugador a otro
    public void Atacar(Personaje Defensor)
    {
        Console.Write($"\t{Nombre} ataca a {Defensor.Nombre}. ");
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
                Console.WriteLine($"\t{Defensor.Nombre} no puede continuar.");
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
        Console.WriteLine($"\t\t\t\tNombre:\t\t\t{Nombre}");
        Console.WriteLine($"\t\t\t\tTipo:\t\t\t{Tipo}");
        Console.WriteLine($"\t\t\t\tApodo:\t\t\t{Apodo}");
        Console.WriteLine($"\t\t\t\tFecha de Nacimiento:\t{FechaDeNacimiento.ToShortDateString()}");
        Console.WriteLine($"\t\t\t\tEdad:\t\t\t{Edad} años");
        Console.WriteLine($"\t\t\t\tFuerza:\t\t\t{Fuerza} ptos");
        Console.WriteLine($"\t\t\t\tDestreza:\t\t{Destreza} ptos");
        Console.WriteLine($"\t\t\t\tVelocidad:\t\t{Velocidad} ptos");
        Console.WriteLine($"\t\t\t\tArmadura:\t\t{Armadura} ptos");
        Console.WriteLine($"\t\t\t\tNivel:\t\t\t{Nivel}");
        Console.WriteLine($"\t\t\t\tSalud:\t\t\t{Salud} %");
        Console.WriteLine($"\t\t\t\tExperiencia:\t\t{Experiencia} px");
        Console.WriteLine($"\t\t\t\tBatallas ganadas:\t{BatallasGanadas}");
    }
}
