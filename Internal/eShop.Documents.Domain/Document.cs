namespace eShop.Documents.Domain;

public sealed class Document
{
    public long DocumentId { get; set; }

    public string Name { get; set; }

    public DateTime Date { get; set; }

    public int OwnerId { get; set; }


    public int Version { get; set; }

    public int UpdaterId { get; set; }


    public int DocumentType { get; set; }

    public int OperationType { get; set; }

    public int Status { get; set; }

    
}
