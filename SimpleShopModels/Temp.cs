using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace SimpleShopModels
{
    class Temp
    {
        public int tempId { get; private set; }
        public float TempC { get; set; }
        public float TempF { get; set; }
        public string lokation { get; set; }

        [JsonConstructor]
        public Temp(float tempC, float tempF, string location)
        {
            TempC = tempC;
            TempF = tempF;
            lokation = location;
        }

        public void SetId(int id)
        {
            tempId = id;
        }
    }
}
