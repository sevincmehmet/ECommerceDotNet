using Business.DTOs.Product;
using Entities.Concrete.Products;

namespace Business.Extensions
{
    public static class CategoryExtensions
    {
        public static List<CategoriyDto> ToTree(this List<CategoriyDto> allNodes, Func<CategoriyDto, bool> rootFilter)
        {
            // Hızlı erişim için Dictionary kullanıyoruz
            var nodeDict = allNodes.ToDictionary(n => n.Id);
            var rootNodes = new List<CategoriyDto>();

            foreach (var node in allNodes)
            {
                // Kök eleman mı? (Level 1 mi?)
                if (rootFilter(node))
                {
                    rootNodes.Add(node);
                }

                // Babasını bul ve onun Childrens listesine kendini ekle
                if (node.ParentCategoryId.HasValue && nodeDict.TryGetValue(node.ParentCategoryId.Value, out var parent))
                {
                    parent.Childrens.Add(node);
                }
            }
            return rootNodes;
        }

        // Tek bir kategori için veritabanındaki tüm listeyi kullanarak slug üretir
        public static string CreateSlugPath(this Category current, List<Category> allCategories)
        {
            var names = new List<string>();
            var temp = current;

            while (temp != null)
            {
                names.Insert(0, temp.Name.ToLower().Trim());

                if (temp.ParentCategoryId.HasValue)
                    // Babayı veritabanından çekilen listeden buluyoruz
                    temp = allCategories.FirstOrDefault(x => x.Id == temp.ParentCategoryId.Value);
                else
                    temp = null;
            }

            var fullPath = string.Join("-", names);
            return ToSeoUrl(fullPath);
        }

        private static string ToSeoUrl(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";

            text = text.Replace("ı", "i").Replace("ğ", "g").Replace("ü", "u")
                       .Replace("ş", "s").Replace("ö", "o").Replace("ç", "c");

            // Harf ve rakam dışındaki karakterleri temizle, boşlukları - yap
            return System.Text.RegularExpressions.Regex.Replace(text, @"[^a-z0-9\s-]", "")
                         .Replace(" ", "-")
                         .Replace("--", "-");
        }
        public static List<int> GetSelfAndChildIds(this List<CategoriyDto> allCategories, int parentId)
        {
            var ids = new List<int> { parentId };
            var children = allCategories.Where(c => c.ParentCategoryId == parentId).ToList();

            foreach (var child in children)
            {
                ids.AddRange(GetSelfAndChildIds(allCategories, child.Id));
            }
            return ids;
        }
    }
}