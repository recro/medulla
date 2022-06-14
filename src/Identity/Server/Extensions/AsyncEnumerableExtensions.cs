// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Identity.Server.Extensions;

/// <summary>
/// Extension methods for <see cref="IAsyncEnumerable{T}"/>.
/// </summary>
public static class AsyncEnumerableExtensions
{
    /// <summary>
    /// Creates a <see cref="List{T}"/> from an <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <param name="source">The <see cref="IAsyncEnumerable{T}"/> to create a <see cref="List{T}"/> from.</param>
    /// <returns>A <see cref="List{T}"/> that contains the elements from the input sequence.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
    public static Task<List<TSource>> ToListAsync<TSource>(this IAsyncEnumerable<TSource> source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return ExecuteAsync();

        async Task<List<TSource>> ExecuteAsync()
        {
            var list = new List<TSource>();

            await foreach (var element in source)
            {
                list.Add(element);
            }

            return list;
        }
    }
}
