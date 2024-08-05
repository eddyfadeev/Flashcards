using Flashcards.Models.Entity;

namespace Flashcards.Interfaces.Database;

public interface IDatabaseManager
{
    internal int InsertEntity<TEntity>(string query, TEntity entity);
    internal IEnumerable<TEntity> GetAllEntities<TEntity>(string query);
    internal IEnumerable<TEntity> GetAllEntities<TEntity>(string query, object parameters);
    internal void BulkInsertRecords(List<Stack> stacks, List<Flashcard> flashcards);
    internal void DeleteTables();
}