namespace Entidades
{
    public class DetalleTicket
    {
        public int Id { get; set; }
        public int IdTicket { get; set; }
        public string TipoSoporte { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }

        public DetalleTicket()
        {
        }

        public DetalleTicket(int id, int idTicket, string tipoSoporte, decimal precio, decimal total)
        {
            Id = id;
            IdTicket = idTicket;
            TipoSoporte = tipoSoporte;
            Precio = precio;
            Total = total;
        }
    }
}
