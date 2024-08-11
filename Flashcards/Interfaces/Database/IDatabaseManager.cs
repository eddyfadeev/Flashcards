using Flashcards.Models.Entity;

namespace Flashcards.Interfaces.Database;

public interface IDatabaseManager
{
    internal int InsertEntity(string query, object parameters);
    internal IEnumerable<TEntity> GetAllEntities<TEntity>(string query);
    internal IEnumerable<TEntity> GetAllEntities<TEntity>(string query, object parameters);
    internal int DeleteEntry(string query, object parameters);
    internal int UpdateEntry(string query, object parameters);
    internal void BulkInsertRecords(List<Stack> stacks, List<Flashcard> flashcards);
    internal void DeleteTables();
}