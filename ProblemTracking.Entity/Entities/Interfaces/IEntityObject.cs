namespace ProblemTracking.Entity.Entities.Interfaces
{
	public interface IEntityObject : IEntityObject<int>
	{

	}

	public interface IEntityObject<IdType> : IEntityObjectBase
    {
		/// <summary>
		/// 
		/// </summary>
		public IdType Id { get; set; }
	}
}