using System;
using System.Collections.Generic;

/// <summary>
/// Вспомогательный класс расширений для работы с LinkedList<T>.
/// </summary>
public static class LinkedListExtensions
{
    /// <summary>
    /// Удаляет узлы, удовлетворяющие условию.
    /// </summary>
    /// <param name="list">Исходный двусвязный список.</param>
    /// <param name="removePredicate">Предикат удаления.</param>
    /// <param name="onRemoveAction">Обработчик события удаления элемента.</param>
    public static void RemoveWhere<T>(
        this LinkedList<T> list, 
        Func<T, bool> removePredicate,
        Action<T> onRemoveAction)
    {
        var currentNode = list.First;

        while (currentNode != null)
        {
            var value = currentNode.Value;
            
            if (removePredicate(value))
            {
                var toRemove = currentNode;
                currentNode = currentNode.Next;
                list.Remove(toRemove);

                onRemoveAction(value);
            }
            else
            {
                currentNode = currentNode.Next;
            }
        }
    }
}
