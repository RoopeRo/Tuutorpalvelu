using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Karkausvuosi
    {
        public static int PaivienLkm(string kuukausi, int vuosi)
        {
            if (new List<string> { "Tammikuu", "Maaliskuu", "Toukokuu", "Heinäkuu", "Elokuu", "Lokakuu", "Joulukuu" }.Contains(kuukausi))
            {
                return 31;
            }
            else if (kuukausi == "Helmikuu")
            {
                if (vuosi % 4 == 0)
                {
                    if (vuosi % 100 == 0 && vuosi % 400 == 0)
                    {
                        return 28;
                    }
                    else
                    {
                        return 29;
                    }
                }
                else
                {
                    return 28;
                }
            }
            else
            {
                return 30;
            }
        }
    }
}
