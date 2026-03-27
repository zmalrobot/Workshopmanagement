using MongoDB.Bson.Serialization.Attributes;

namespace Workshopmanagement.DatabaseManagment.DataObject
{
    public class SearcherWorkflowTask
    {
        public string? Id { get; set; }
        public string? WorkflowId { get; set; }
        public string? Title { get; set; }
    }

    public class WorkflowTask
    {
        [BsonId]
        public string Id { get; set; } = null!;
        public string WorkflowId { get; set; } = null!;
        public int TaskOrder { get; set; }
        public string Title { get; set; } = null!;
        public string Subtitle { get; set; } = null!;
        public string? TaskSpecification { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }

        //TaskCondfiguration
        public bool IsSignTask { get; set; }               //Task di firma
        public bool IsStartingTask { get; set; }           //Task di partenza
        public bool UserMustUpdateVeichleKm { get; set; }  //Chiede all'utente di inserire i km per il veicolo
        public bool UserMustUpdateFuelLevel { get; set; }  //Chiede all'utente di inserire il carburante per il veicolo
        public bool AllowImageUpload { get; set; }         //Permette di caricare le immagini
        public bool AllowLoop { get; set; }                //Permette di ripetere lo stesso task N volte
        public bool IsComponentRequired { get; set; }      //Permette di inserire un elenco di componenti e il relativo prezzo
        public bool CanBeSkipped { get; set; }             //Permette di skippare il task
        public bool HasOutcome { get; set; }               //Permette di inserire un outcome
        public bool IsFinalTask { get; set; }              //Task di fine
    }
}
