using ServiceStack;
using System.Collections.Generic;

namespace SSRQ.ServiceInterface.DTO
{
    [Route("/clients")]
    [Route("/clients/{id}/{name}")]
    public class Client : IReturn<ClientsResponse>
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class ClientsResponse
    {
        public HashSet<Client> Clients { get; set; }
        public string Response { get; set; }
    }
}
