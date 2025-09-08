using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorExpenseTracker2.Data.Data
{
    public class SQLConfiguration
    {
        //se almacena el conection string que configuramos en el appsetting -> es de solo lectura y no sera actualizado desde afuera
        public SQLConfiguration(string conectionString) => ConectionString = conectionString;
        public string ConectionString { get; }
    }
}
