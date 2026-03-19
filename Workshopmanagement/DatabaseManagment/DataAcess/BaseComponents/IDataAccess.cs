using MongoDB.Driver;
using System.Collections;

namespace Workshopmanagement.DatabaseManagment.DataAcess.BaseComponents
{
    public interface IDataAccess<Table, Searcher> where Table : class where Searcher : class
    {
        Task InsertAsync(Table obj);
        Task InsertAsync(List<Table> objs);
        Task UpdateAsync(Table obj);
        Task UpsertAsync(Table obj, Searcher searcher);
        Task DeleteAsync(string id);
        Task<Table?> FindByIdAsync(string Id);
        Task<List<Table>> FindAsync(Searcher searcher);
        Task<(List<Table> Results, bool IsLastPage)> FindPagedAsync(Searcher searcher, int page);
        string GetIdValue(Table table);
        FilterDefinition<Table> FilterBuilder(Searcher searcher);
        FilterDefinition<Table> FilterBuilder(string ID);
        bool CanIncrement();
        UpdateDefinition<Table> GetIncrementFilter(bool isIncrement);
        Table GetDefaultIncrementObject(Searcher searcher);
        Task IncrementCounterValueAsync(Searcher searcher, bool isIncrement, int retry = 0);
    }
}
