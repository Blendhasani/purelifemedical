namespace PurelifeMedical.Models
{
    public class AnalizaLloji
    {
        public int Id { get; set; }
        public int AnalizaId { get; set; } 
        public Analiza Analiza { get; set; } 

        public int LlojiId { get; set; } 
        public Lloji Lloji { get; set; } 
    }
}
