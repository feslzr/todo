using Kobold.TodoApp.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kobold.TodoApp.Api.Services
{
    public class CollectionService
    {
        private static int _nextCollectionId = 1;
        private static int _nextTodoId = 1;
        private static readonly List<Collection> _collections = new List<Collection>();

        public IEnumerable<Collection> Get()
        {
            return _collections;
        }

        public Collection Get(int id)
        {
            var collection = _collections.SingleOrDefault(c => c.Id == id);

            if (collection == null)
                throw new InvalidOperationException("Coleção não encontrada.");

            return collection;
        }

        public Collection Create(CollectionViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Name))
                throw new ArgumentException("Necessário informar o nome da Coleção.", nameof(model.Name));

            var collection = new Collection
            {
                Id = _nextCollectionId++,
                Name = model.Name,
                Todos = new List<Todo>()
            };

            _collections.Add(collection);

            return collection;
        }

        public Todo AddTodo(int collectionId, TodoViewModel model)
        {
            var collection = Get(collectionId);

            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Description))
                throw new ArgumentException("Necessário informar a descrição do Todo.", nameof(model.Description));

            var todo = new Todo
            {
                Id = _nextTodoId++,
                DataCriacao = DateTime.Now,
                Description = model.Description,
                Done = model.Done
            };

            collection.Todos.Add(todo);

            return todo;
        }

        public IEnumerable<Todo> GetTodos(int collectionId)
        {
            var collection = Get(collectionId);
            return collection.Todos;
        }

        public Todo GetTodo(int collectionId, int todoId)
        {
            var collection = Get(collectionId);

            var todo = collection.Todos.SingleOrDefault(t => t.Id == todoId);

            if (todo == null)
                throw new InvalidOperationException("Todo não encontrado.");

            return todo;
        }

        public Todo UpdateTodo(int collectionId, int todoId, TodoViewModel model)
        {
            var todo = GetTodo(collectionId, todoId);

            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.Description))
                throw new ArgumentException("Necessário informar a descrição do Todo.", nameof(model.Description));

            todo.Description = model.Description;
            todo.Done = model.Done;

            return todo;
        }

        public void RemoveTodo(int collectionId, int todoId)
        {
            var collection = Get(collectionId);

            var todo = collection.Todos.SingleOrDefault(t => t.Id == todoId);

            if (todo == null)
                throw new InvalidOperationException("Todo não encontrado.");

            collection.Todos.Remove(todo);
        }

        public void Remove(int id)
        {
            var collection = _collections.SingleOrDefault(c => c.Id == id);

            if (collection == null)
                throw new InvalidOperationException("Coleção não encontrada.");

            _collections.Remove(collection);
        }
    }
}