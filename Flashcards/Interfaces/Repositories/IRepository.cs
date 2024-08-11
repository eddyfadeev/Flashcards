﻿using Flashcards.Interfaces.Models;

namespace Flashcards.Interfaces.Repositories;

internal interface IRepository<TEntity>
{
    internal TEntity? ChosenEntry { get; set; }
    internal int Insert(IDbEntity<TEntity> entity);
    internal void Delete(int id);
}