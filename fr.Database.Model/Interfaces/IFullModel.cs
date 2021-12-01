using System;

namespace fr.Database.Model.Interfaces
{
    public interface IFullModel : IKeyModel, ICreatedModel, IUpdatedModel, IDeletedModel
    {
    }
}
