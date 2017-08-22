using System;

namespace TDDBank
{
    /// <summary>
    /// - Kontostand abfragen
    /// - Betrag einzahlen (nicht negativ)
    /// - Betrag auszahlen (nicht negativ)
    ///     - Kontostand darf nicht unter 0 fallen
    /// - Neues Konto hat 0 als Kontostand
    /// </summary>
    public class Konto
    {
        public decimal Kontostand { get; private set; }

        public void Einzahlen(decimal betrag)
        {
            if (betrag < 0)
                throw new ArgumentException("Betrag darf nicht negativ sein!");

            Kontostand += betrag;
        }

        public void Auszahlen(decimal betrag)
        {
            if (betrag < 0)
                throw new ArgumentException("Betrag darf nicht negativ sein!");

            if (betrag > Kontostand)
                throw new InvalidOperationException("Auszahlungsbetrag darf nicht größer als der Kontostand sein!");

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday ||
                DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                throw new InvalidOperationException("Am Wochenende keine Auszahlung möglich!");

            Kontostand -= betrag;
        }
    }
}
