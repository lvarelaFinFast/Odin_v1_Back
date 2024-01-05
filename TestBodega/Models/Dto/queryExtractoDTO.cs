namespace TestBodega.Models.Dto
{
    public class queryExtractoDTO
    {
        public Extracto Extracto { get; set; }
        public Municipios Origen { get; set;}
        public Municipios Destino { get; set; }
        public Contrato Contrato { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public Conductores Conductores1 { get; set; }
        public Conductores conductores2 { get; set; }
    }
}
