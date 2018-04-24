using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiceManager.Messages
{
    public class OtherMessages
    {
        public const string WyprodukowanoKG = "Wyprodukowano {0} kg produktu \"{1}\".";
        public const string ProdukcjaZakonczonaSuksesem = "Pomyślnie zakończono produkcję.";
        public const string ZapisZakonczonySuksesem = "Pomyślnie zapisano stan przypraw i produktów.";
        public const string EksportZakonczonySuksesem = "Pomyślnie wyeksportowano dane do arkusza excel o nazwie \"{0}\".";
        public const string PomyslnieDodanoElement = "Pomyślnie dodano element.";
        public const string PomyslnieEdytowanoElement = "Pomyślnie edytowano element.";
        public const string PomyslnieUsunietoElement = "Pomyślnie usunięto element.";
        public const string PomyslnieWyczyszczonoMagazyn = "Pomyślnie wyczyszczono magazyn.";
        public const string PomyslnieWyczyszczonoHistorie = "Pomyślnie usiuniete produkcje starsze niż {1}. Usunieto {0} elementów.";
    }
}
