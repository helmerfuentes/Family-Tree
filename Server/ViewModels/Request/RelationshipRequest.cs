namespace Server.ViewModels.Request
{
    public class RelationshipRequest
    {
        public int MotherId { get; set; }
        public int ParentId { get; set; }
        public required int ChildId { get; set; }
        public int IsBiologic { get; set; } = 1; //todo: cambios en la segunda version para mandar este dato, se tendra en cuenta que los hiujos son bilogicos por defecto
    }
}
