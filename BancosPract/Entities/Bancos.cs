
namespace BancosPract.Entities
{
    public class Bancos
    {
        public Bancos()
        {
        }
         
         public Guid Id { get; set; }
         public string Name { get; set; }
         public int NoCuenta { get; set; }
         public string Address { get; set;}
         public decimal Cash{ get; set; }
         public List<Servicio> Servicios{ get; set; }
         
        





    }
}