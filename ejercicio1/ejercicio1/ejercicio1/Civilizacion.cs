using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Civilizacion
{
    public int id { get; set; }
    public string name { get; set; }
    public string expansion { get; set; }
    public string army_type { get; set; }
    public List<string> unique_unit { get; set; }
    public List<string> unique_tech { get; set; }
    public string team_bonus { get; set; }
    public List<string> civilization_bonus { get; set; }
    public Civilizacion() { }

}

