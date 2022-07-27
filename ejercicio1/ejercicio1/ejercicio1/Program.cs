using System.Net;
using System.Text.Json;



class Program
{

    static int Main(string[] args)
    {
        int id = 0;

        ListaCivilizaciones listaCivilizaciones = ObtenerListaCivilizaciones();
        MostrarCivilizaciones(listaCivilizaciones);
        Console.WriteLine("===============================");
        Console.WriteLine("Ingrese un Id");
        id = Convert.ToInt32(Console.ReadLine());
        MostrarInformacionCivilizacion(listaCivilizaciones, id);
        return 0;
    }

    public static ListaCivilizaciones ObtenerListaCivilizaciones()
    {

        ListaCivilizaciones listaCivilizaciones = new ListaCivilizaciones();

        string url = $"https://age-of-empires-2-api.herokuapp.com/api/v1/civilizations";
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
                            listaCivilizaciones = JsonSerializer.Deserialize<ListaCivilizaciones>(lecturaJson);
                        }
                    }
                }
            }
        }
        catch (WebException excepcion)
        {
            Console.WriteLine("Hubo un error al conectar con el servicio web.");
        }

        return listaCivilizaciones;

    }

    public static void MostrarCivilizaciones(ListaCivilizaciones listaCivilizaciones)
    {

        if (listaCivilizaciones.civilizations.Any())
        {

            Console.WriteLine("=== LISTA DE CIVILIZACIONES ===");

            foreach (var civilizacion in listaCivilizaciones.civilizations)
            {
                Console.WriteLine($"ID: {civilizacion.id} - Nombre: {civilizacion.name} - Expansión: {civilizacion.expansion} ");
            }

        }
        else
        {
            Console.WriteLine("No se cargaron civilizaciones.");
        }

    }

    public static void MostrarInformacionCivilizacion(ListaCivilizaciones listaCivilizaciones, int idCivilizacion)
    {

        var civilizacionBuscada = listaCivilizaciones.civilizations.Find(civilizacion => civilizacion.id == idCivilizacion);

        if (civilizacionBuscada != null)
        {

            Console.WriteLine($"==> Civilización: {civilizacionBuscada.name}, ID: {civilizacionBuscada.id}");
            Console.WriteLine($"- Expansión: {civilizacionBuscada.expansion}");
            Console.WriteLine($"- Tipo de armada: {civilizacionBuscada.army_type}");
            Console.WriteLine($"- Bonus de equipo: {civilizacionBuscada.team_bonus}");

        }
        else
        {
            Console.WriteLine("No se encontró la civilización buscada.");
        }


    }
}
