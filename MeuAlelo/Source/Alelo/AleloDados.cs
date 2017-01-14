using System;
using System.Collections;
using System.Collections.Generic;
namespace MeuAlelo.Source.Alelo
{
    public class AleloDados
    {
        public string CartaoNumero { get; set; }
        public string Ticket { get; set; }
    }

    public class aleloDadosCollection:Dictionary<string,AleloDados>
    {
        public void Read()
        {
            if (System.IO.File.Exists("list.json"))
            {
                var value = System.IO.File.ReadAllText("list.json");
                if(!string.IsNullOrEmpty(value))
                {
                    var list= Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, AleloDados>>(value);
                    foreach (var item in list)
                    {
                        this.Add(item.Key, item.Value);
                    }
                }
            }
        }
        public void Salvar()
        {
            var value = Newtonsoft.Json.JsonConvert.SerializeObject(this);
           System.IO.File.WriteAllText("list.json", value);
        }
    }
}