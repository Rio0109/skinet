using System.Security.Cryptography.X509Certificates;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
  public ProductSpecification(ProductSpecParmas specParmas) : base(x => 
    (specParmas.Brands.Count == 0 || specParmas.Brands.Contains(x.Brand)) &&
    (specParmas.Types.Count == 0 || specParmas.Types.Contains(x.Type))
  )
  {
    ApplyPaging(specParmas.PageSize * (specParmas.PageIndex -1), specParmas.PageSize);

    switch (specParmas.Sort)
    {
      case "priceAsc":
        AddOrderBy(x => x.Price);
        break;
        case "priceDesc":
          AddOrderByDescending(x => x.Price);
          break;
      default:
        AddOrderBy(x => x.Name);
        break;
    }
  }  
}