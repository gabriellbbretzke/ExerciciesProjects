namespace Project.Domain.Shared.Entities;

public abstract class Entity(Guid id) : IEquatable<Guid>
{
    #region Properties
    public Guid Id { get; } = id;
    #endregion

    #region Equatable Implmentation

    public bool Equals(Guid id) => Id == id;
    
    public override int GetHashCode() => Id.GetHashCode();

    #endregion
}