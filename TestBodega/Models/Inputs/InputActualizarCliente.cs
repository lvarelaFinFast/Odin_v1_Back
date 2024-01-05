namespace TestBodega.Models.Inputs
{
    public class InputActualizarCliente
    {
        public int Id { get; set; }
        public int IdTipoPersona { get; set; }
        public string NmCliente { get; set; }
        public int IdTipoDocumento { get; set; }
        public string NoDocumento { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
    }
}
