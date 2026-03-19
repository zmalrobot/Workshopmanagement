**Bit Bites S.r.l.s**

**Relazione sul funzionamento dell’applicazione**

L’applicazione in questione, commissionata dall’azienda Venice Camper Service, sarà un **ERP** (Enterprise Resource Planning) che servirà a gestire i processi lavorativi all’interno dell’officina.

Questo gestionale sarà sviluppato come Web App, quindi un software utilizzabile attraverso qualsiasi browser Internet attualmente supportato (Google Chrome, Safari ecc.) così da permettere la miglior compatibilità con qualsiasi dispositivo utilizzato.

Il software gestirà le anagrafiche dei clienti ed i veicoli a loro associati e le lavorazioni che verranno eseguite (Check Up, Tagliandi e Manutenzioni) con il loro relativo storico.
Il software inoltre si interfaccerà con il Magazzino e con la parte di Fatturazione (che verrà gestita esternamente dal programma di Fatturazione Elettronica del cliente).

*Di seguito verrà inserito lo schema del funzionamento principale dell’app che verrà successivamente descritto nel dettaglio.*


**Descrizione dettagliata del funzionamento principale dell’App**

L’applicazione, principalmente, servirà per gestire il workflow degli interventi che si andranno ad effettuare sui veicoli.

1. Il primo passaggio sarà il **Check In** del veicolo. In questo passaggio si verificherà se il cliente è già presente nell’anagrafica aziendale oppure no.
   In caso negativo, si andranno ad inserire i dati del nuovo cliente, con i relativi dati del veicolo in suo possesso.
   In caso affermativo, si caricherà l’anagrafica cliente già presente e si procederà allo step successivo.
1. Il secondo passaggio riguarda **l’Ispezione visiva esterna ed interna del veicolo.**
   Verrà sempre effettuata, prima di ogni tipo di intervento, un’ispezione visiva del veicolo per segnalare eventuali danni, crepe, urti ecc. presenti.
   Verrà segnato, inoltre, il Chilometraggio del veicolo ed il livello del carburante durante questa fase di consegna in officina del mezzo.
1. Il terzo passaggio riguarda la **scelta dell’intervento** da effettuare al mezzo in questione.
   Gli interventi saranno divisi in due tipologie: **Manutenzione** e **Check Up** (Controllo utenze, Controllo componenti meccanici e Controllo infiltrazioni).
   Con **Check Up** si intendono tutti quei controlli effettuati di routine, che sono a loro volta divisi in tre tipologie:
   1. **Controllo Utenze:** controllo Riscaldamento, impianto luci interne, prese 220V e 12V, frigorifero, condizionatore ecc.
   1. **Controllo Componenti Meccanici:** controllo pressione gomme, battistrada pneumatici, livello olio motore, fanaleria anteriore e posteriore ecc.
   1. **Controllo Infiltrazioni:** controllo guarnizioni finestre, sportelli gavoni, porta ingresso, pavimento ecc.

Con **Manutenzioni** si intendono tutte quelle lavorazioni che non fanno parte della tipologia **Check Up**, ad esempio la sostituzione di un qualche componente meccanico, un cambio gomme, il montaggio di un componente aggiuntivo ecc.

1. Il quarto ed ultimo processo è il **Check Out** del veicolo.
   In questo passaggio verrà chiusa la scheda di lavorazione in cui verrà segnata la data di termine dei lavori, la data di consegna del veicolo, il resoconto delle lavorazioni eseguite ed il riferimento al pagamento.
   Il resoconto delle lavorazioni verrà poi archiviato in un database che terrà lo storico di tutti gli interventi eseguiti per ogni mezzo presente nell’anagrafica cliente.
   Ogni resoconto potrà essere esportato e stampato così da fornire una copia digitale e/o cartacea al cliente.

**Ulteriori funzionalità dell’Applicazione**

Oltre alla funzionalità principale appena descritta, l’applicazione possiederà queste ulteriori funzioni:

- **Anagrafica Cliente**
  Database che conterrà i dati di tutti i clienti dell’officina.
- **Anagrafica Veicolo**
  Database che conterrà i dati di tutti i veicoli associati ai clienti dell’officina.
- **Interventi in corso**
  Elenco degli interventi attualmente in corso.
- **Storico degli interventi**
  Elenco contenente i report di tutti gli interventi svolti, suddivisi per cliente e per veicolo.
- **Calendario appuntamenti**
  Calendario condiviso tra l’Amministrazione e l’Officina per la gestione degli appuntamenti.

