namespace TestBodega.Models.Dto
{
    public class LlaveValorObjectDTO<T>
    {
        public int Llave { get; set; }
        public string Valor { get; set; }
        public T data { get; set; }

    }
}
