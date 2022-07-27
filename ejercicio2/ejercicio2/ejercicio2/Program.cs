using System.Text.Json;
using System.Net;
namespace TP102;

class Program
{
    static int Main(string[] args)
    {

        Console.WriteLine("======= ESTRUCTURAS AoE2 =======");
        Estructuras listaEstructuras = ObtenerEstructuras();

        if (listaEstructuras.lEstructuras.Any())
        {
            Console.WriteLine("Hola");
            AdministradorEstructuras(listaEstructuras);
        }

        return 0;

    }

    public static Estructuras ObtenerEstructuras()
    {

        Estructuras listaEstructuras = new Estructuras();

        string url = $"https://age-of-empires-2-api.herokuapp.com/api/v1/structures";
        var request = (HttpWebRequest)WebRequest.Create(url);

        request.Method = "GET";
        request.Accept = "application/json";
        request.ContentType = "application/json";

        try
        {
            using (WebResponse respuesta = request.GetResponse())
            {
                using (Stream reader = respuesta.GetResponseStream())
                {
                    if (reader != null)
                    {

                        using (StreamReader objectReader = new StreamReader(reader))
                        {
                            string lecturaJson = objectReader.ReadToEnd();
                            listaEstructuras = JsonSerializer.Deserialize<Estructuras>(lecturaJson);
                        }
                    }
                }
            }
        }
        catch (WebException excepcion)
        {
            Console.WriteLine("Hubo un error al conectar con el servicio web.");
        }

        return listaEstructuras;

    }

    public static void AdministradorEstructuras(Estructuras listaEstructuras)
    {

        Random rand = new Random();
        Jugador J1 = new Jugador(rand.Next(1000), rand.Next(1500), rand.Next(1000));

        int operacion = 0;

        do
        {
            Console.WriteLine("== ADMINISTRADOR DE ESTRUCTURAS: ");

            do
            {
                Console.WriteLine(" 1) Ver recursos ");
                Console.WriteLine(" 2) Ver estructuras ");
                Console.WriteLine(" 3) Comprar estructura ");
                Console.WriteLine(" 4) Salir ");
                int.TryParse(Console.ReadLine(), out operacion);

            } while (operacion < 1 || operacion > 4);

            switch (operacion)
            {
                case 1:
                    J1.mostrarRecursos();
                    break;
                case 2:
                    J1.mostrarMisEstructuras();
                    break;
                case 3:
                    ComprarEstructuras(J1, listaEstructuras);
                    break;
            }

        } while (operacion != 4);
    }

    public static void ComprarEstructuras(Jugador J, Estructuras listaEstructuras)
    {

        int i = 1;
        foreach (var estructura in listaEstructuras.lEstructuras)
        {

            string infoEstructura = $"{i}) {estructura.Name} - Costo: ";

            if (estructura.Cost.Gold != null) infoEstructura += $"{estructura.Cost.Gold} oro, ";
            if (estructura.Cost.Wood != null) infoEstructura += $"{estructura.Cost.Wood} madera, ";
            if (estructura.Cost.Stone != null) infoEstructura += $"{estructura.Cost.Stone} piedra.";

            Console.WriteLine(infoEstructura);

            i++;
        }

        int estructuraElegida = 0;

        do
        {
            Console.WriteLine("¿Cuál estructura desea comprar?");
            int.TryParse(Console.ReadLine(), out estructuraElegida);

        } while (estructuraElegida < 1 || estructuraElegida >= i);

        int cantidadOroEstructuraElegida = listaEstructuras.lEstructuras[estructuraElegida - 1].Cost.Gold ?? 0;
        int cantidadMaderaEstructuraElegida = listaEstructuras.lEstructuras[estructuraElegida - 1].Cost.Wood;
        int cantidadPiedraEstructuraElegida = listaEstructuras.lEstructuras[estructuraElegida - 1].Cost.Stone ?? 0;

        if (cantidadOroEstructuraElegida <= J.cantidadOro && cantidadMaderaEstructuraElegida <= J.cantidadMadera && cantidadPiedraEstructuraElegida <= J.cantidadPiedra)
        {
            J.agregarEstructura(listaEstructuras.lEstructuras[estructuraElegida - 1]);
        }
        else
        {
            Console.WriteLine(" -> Recursos insuficientes :(");
        }

    }
}