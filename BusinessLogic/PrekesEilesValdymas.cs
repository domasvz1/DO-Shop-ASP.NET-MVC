using System.Collections.Generic;
using BusinessLogic.Interfacai;
using BusinessObjects.PrekesInfo;
using BusinessObjects.KategorijosInfo;
using Data_Accesss.Interfacai;
using Mehdime.Entity;
using System;

namespace BusinessLogic
{
    public class PrekesEilesValdymas : IPrekesEilesValdymas
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IPrekesRepositorija _prekesRepositorija;
        private readonly IKategorijosRepositorija _kategorijosRepositorija;

        public PrekesEilesValdymas(IDbContextScopeFactory dbContextScopeFactory, IPrekesRepositorija prekesRepositorija, IKategorijosRepositorija kategorijosRepositorija)
        {
            _dbContextScopeFactory = dbContextScopeFactory ?? throw new ArgumentNullException("dbContextScopeFactory");
            _prekesRepositorija = prekesRepositorija ?? throw new ArgumentNullException("Prekes repositorija");
            _kategorijosRepositorija = kategorijosRepositorija ?? throw new ArgumentNullException("Kategorijos repositorija");
        }

        public Preke GautiPreke(int prekesId) // Gauti preke pagal ivesta Id
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                Preke preke = _prekesRepositorija.RastiPrekePagalId(prekesId);

                if (preke == null)
                    throw new ArgumentException("Preke su paduotu Id nebuvo rasta");

                return preke;
            }
        }

        public IEnumerable<Preke> GautiPrekesPagalKategorija(int kategorijosId) // Gauti prekes pagal ivesta kategorijos id
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                Kategorija kategorija = _kategorijosRepositorija.RastiKategorijaPagalId(kategorijosId);

                if (kategorija == null)
                    throw new ArgumentException("Prekiu su tokia kategorija nera");

                return kategorija.Prekess;
            }
        }

        public IEnumerable<Preke> GautiVisasPrekes()
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                return _prekesRepositorija.GautiVisasPrekes();
            }
        }
    }
}
