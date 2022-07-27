using System.Text.Json.Serialization;
namespace TP102;

public class Cost
{
    [JsonPropertyName("Wood")]
    public int Wood { get; set; }

    [JsonPropertyName("Stone")]
    public int? Stone { get; set; }

    [JsonPropertyName("Gold")]
    public int? Gold { get; set; }
}

public class Estructuras
{
    [JsonPropertyName("structures")]
    public List<Structure> lEstructuras { get; set; }
}

public class Structure
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("expansion")]
    public string Expansion { get; set; }

    [JsonPropertyName("age")]
    public string Age { get; set; }

    [JsonPropertyName("cost")]
    public Cost Cost { get; set; }

    [JsonPropertyName("build_time")]
    public int BuildTime { get; set; }

    [JsonPropertyName("hit_points")]
    public int HitPoints { get; set; }

    [JsonPropertyName("line_of_sight")]
    public int LineOfSight { get; set; }

    [JsonPropertyName("armor")]
    public string Armor { get; set; }

    [JsonPropertyName("special")]
    public List<string> Special { get; set; }

    [JsonPropertyName("reload_time")]
    public double? ReloadTime { get; set; }

    [JsonPropertyName("range")]
    public object Range { get; set; }

    [JsonPropertyName("attack")]
    public int? Attack { get; set; }
}

public class Jugador
{

    public int cantidadMadera;
    public int cantidadOro;
    public int cantidadPiedra;
    public List<Structure> misEstructuras;

    public Jugador(int cantidadM, int cantidadO, int cantidadP)
    {
        this.cantidadMadera = cantidadM;
        this.cantidadOro = cantidadO;
        this.cantidadPiedra = cantidadP;
        misEstructuras = new List<Structure>();
    }

    public void agregarEstructura(Structure estructura)
    {
        misEstructuras.Add(estructura);
        this.cantidadMadera -= estructura.Cost.Wood;
        this.cantidadOro -= estructura.Cost.Gold ?? 0;
        this.cantidadPiedra -= estructura.Cost.Stone ?? 0;
    }

    public void mostrarMisEstructuras()
    {
        if (misEstructuras.Any())
        {
            Console.WriteLine("--> Tus estructuras: ");
            foreach (var estructura in misEstructuras)
            {
                Console.WriteLine("  - " + estructura.Name);
            }
        }
        else
        {
            Console.WriteLine("No tienes estructuras.");
        }
    }

    public void mostrarRecursos()
    {
        Console.WriteLine("--> Tus recursos: ");
        Console.WriteLine(" - ORO: " + this.cantidadOro);
        Console.WriteLine(" - MADERA: " + this.cantidadMadera);
        Console.WriteLine(" - PIEDRA: " + this.cantidadPiedra);
    }

}
