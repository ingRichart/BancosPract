namespace BancosPract.Entities
{
    public class Servicio
    {
        public Servicio()
        {
        }

         public Guid Id { get; set; }
         public string Tipo { get; set; }
         public int NoCuentaS { get; set; }
         public string  NombreS{ get; set;}
         public decimal Costo { get; set; }
         public Guid? BancosId { get; set; }
         public Bancos? Bancos{ get; set; }





    }



}