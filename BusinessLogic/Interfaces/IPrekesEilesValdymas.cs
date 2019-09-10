using System.Collections.Generic;
using BusinessObjects.PrekesInfo;

namespace BusinessLogic.Interfacai
{
    public interface IPrekesEilesValdymas
    {
        Preke GautiPreke(int prekesId); // Gauti preke pagal ivesta Id
        IEnumerable<Preke> GautiPrekesPagalKategorija(int kategorijosId); // Gauti prekes pagal ivesta kategorijos id
        IEnumerable<Preke> GautiVisasPrekes();
    }
}