using Core.Entities.Abstract;

namespace Entities.Concrete.Products
{

    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; } // Url için gerekli 
        public int? ParentCategoryId { get; set; } // en üstekinin parentı olmayabilir.
        public int Level { get; set; } // 1: Ana, 2: Alt, 3: En Alt

    }
}

