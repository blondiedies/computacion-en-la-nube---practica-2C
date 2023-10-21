namespace computación_en_la_nube___práctica_2C.Models
{
    public class ObjectListModel
    {
        public int count { get; set; } //Total de objetos
        public string next { get;set; } //enlace a siguiente
        public string previous { get;set; } //enlace a anterior
        public List<ObjectModel> list { get; set; }
    }
}
