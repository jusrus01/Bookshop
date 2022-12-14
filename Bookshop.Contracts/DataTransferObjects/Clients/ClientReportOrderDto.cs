namespace Bookshop.Contracts.DataTransferObjects.Clients
{
    public class ClientReportOrderDto
    {
        public DateTime Created { get; set; }

        public DateTime Completed { get; set; }

        public List<ClientReportBookDto> Books { get; set; }
    }
}
