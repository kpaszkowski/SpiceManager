using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiceManager.Messages
{
    public static class ValidationMessages
    {
        public const string NieWybranoElementuDoUsuniecia = "Nie wybrano elementu do usunięcia.";
        public const string NieWybranoElementuDoEdycji = "Nie wybrano elementu do edycji.";
        public const string ZleParametry = "Podano niewłaśniwe dane.";
        public const string NazwaPrzyprawyNieJestUnikalna = "Istnieje już taka przyprawa.";
        public const string NazwaProduktuNieJestUnikalna = "Istnieje już taki produkt.";
        public const string NazwaPartiiNieJestUnikalna = "Istnieje już taka partia.";
        public const string BrakPrzyprawy = "Brakuje {0} gram przyprawy \"{1}\".\n\tNajpierw dodaj wymaganą ilość do magazynu.";
        public const string NiePodanoNazwy= "Nie podano nazwy.";
        public const string NieMoznaUsunacPrzyprawy = "Nie można usunąć przyprawy.\n\tNajpierw zmodyfikuj skład produktu \"{0}\".";
        public const string NieMoznaUsunacPrzyprawyZnajdujeSieWMagazynie = "Nie można usunąć przyprawy.\n\tNajpierw usuń ją z magazynu.";
        public const string NieWybranoPrzyprawy = "Nie wybrano przyprawy.";
        public const string NieWybranoProduktu = "Nie wybrano produktu.";
        public const string PrzyprawaJużIstenieWPrzepisie = "Przyprawa już istenie w przepisie.\nWybierz inną.";
    }
}
