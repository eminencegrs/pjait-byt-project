using System.Collections.Immutable;

namespace App;

public class Catalog
{
    public string Description { get; init; }

    public int Year { get; init; }

    public IImmutableDictionary<string, Category> Categories { get; private set; } =
        ImmutableDictionary<string, Category>.Empty;

    public Task<bool> AddCategory(Category newCategory)
    {
        newCategory = newCategory ?? throw new ArgumentNullException(nameof(newCategory));

        // TODO: check if category/categories exist(s).
        this.Categories = this.Categories.Add(newCategory.Id, newCategory);

        return Task.FromResult(true);
    }

    public Task<bool> AddCategories(ICollection<Category> newCategories)
    {
        newCategories = newCategories ?? throw new ArgumentNullException(nameof(newCategories));
        if (!newCategories.Any())
        {
            throw new ArgumentException(nameof(newCategories));
        }

        // TODO: check if category/categories exist(s).
        this.Categories = this.Categories.AddRange(
            newCategories.Select(x => new KeyValuePair<string, Category>(x.Id, x)));

        return Task.FromResult(true);
    }

    public Task<bool> RemoveCategory(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("The category id must not be null, empty or white-space.", nameof(id));
        }

        // TODO: check if category does not exist.
        this.Categories = this.Categories.Remove(id);

        return Task.FromResult(true);
    }
}
